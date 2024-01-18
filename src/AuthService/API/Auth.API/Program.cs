﻿using System.Security.Cryptography;
using System;
using Auth.Application.Ports.Repositories;
using Auth.Application.Ports.Services;
using Auth.Application.UseCases.CreateUser;
using Auth.Application.UseCases.Login;
using Auth.Application.UseCases.RefreshToken;
using Auth.Application.UseCases.SignOut;
using Auth.Infrastructure.Repositories.MongoDB;
using Auth.Infrastructure.Services.Cryptography;
using Auth.Infrastructure.Services.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Auth.Infrastructure.Repositories.MongoDB.Initialisation;

var builder = WebApplication.CreateBuilder(args);
// Register services
var jwtSettingsConfiguration = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsConfiguration);
var jwtSettings = jwtSettingsConfiguration.Get<JwtSettings>();

builder.Services.AddSingleton<IAuthTokenService, JwtService>();
builder.Services.AddSingleton<ICryptographyService, CryptographyService>();
builder.Services.AddSingleton(provider =>
{
    var rsa = RSA.Create();
    rsa.ImportRSAPrivateKey(Convert.FromBase64String(jwtSettings.AccessTokenSettings.PrivateKey), out var _);
    return new RsaSecurityKey(rsa);
});


// Register repositories
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDBSettings"));
MongoDbInit.InitializeMongoDB(builder.Configuration);
builder.Services.AddSingleton<IAuthRepository, AuthRepository>();

// Register use cases
builder.Services.AddSingleton<LoginUseCase>();
builder.Services.AddSingleton<RefreshTokenUseCase>();
builder.Services.AddSingleton<SignOutUseCase>();
builder.Services.AddSingleton<CreateUserUseCase>();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("all");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
