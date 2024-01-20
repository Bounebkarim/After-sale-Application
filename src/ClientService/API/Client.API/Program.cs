using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using Asp.Versioning;
using Client.Api.Authorization;
using Client.API.MiddleWare;
using Client.Application;
using Client.Application.Contracts.Specs;
using Client.Application.Services;
using Client.Infrastructure;
using Client.Persistence;
using Client.Persistence.MigrationManager;
using Contracts.ServiceDiscovery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
const string SERVICE_NAME = "Client.Service";
// Add services to the container.
builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig.WriteTo.Console()
        .ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddHealthChecks();

builder.Services.AddConsul(builder.Configuration.GetServiceConfig(builder.Environment));
builder.Services.AddControllers().AddJsonOptions(config =>
{
    config.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.AllowTrailingCommas = true;
});
;
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUriService>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriService(uri);
});

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1);
    config.AssumeDefaultVersionWhenUnspecified = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Client.Api", Version = "1" });
        c.AddSecurityDefinition(
            "token",
            new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer",
                In = ParameterLocation.Header,
                Name = HeaderNames.Authorization
            }
        );
        c.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "token"
                        }
                    },
                    Array.Empty<string>()
                }
            }
        );
    }
);
var jwtSettingsConfiguration = builder.Configuration.GetSection("AccessTokenSettings");
builder.Services.Configure<AccessTokenSettings>(jwtSettingsConfiguration);
var jwtSettings = jwtSettingsConfiguration.Get<AccessTokenSettings>();

var rsa = RSA.Create();
rsa.ImportRSAPublicKey(
    Convert.FromBase64String(jwtSettings.PublicKey),
    out var _
);

var rsaKey = new RsaSecurityKey(rsa);

builder.Services.AddCors(options =>
{
    options.AddPolicy("all", builder => builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            RequireSignedTokens = true,
            RequireExpirationTime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = rsaKey,
            ClockSkew = TimeSpan.FromMinutes(0)
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanReadClients", policy =>
        policy.Requirements.Add(new GetClientRequirement()));
    options.AddPolicy("CanWriteClients", policy =>
        policy.Requirements.Add(new PostClientRequirement()));
});

// Singletons
builder.Services.AddSingleton<GetAccessHandler>();
builder.Services.AddSingleton<PostAccessHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, CompositeAccessHandler>();

var app = builder.Build().MigrateDatabase();
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseCors("all");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/healthcheck");
app.Run();