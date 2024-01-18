using Microsoft.AspNetCore.Authorization;

namespace Client.Api.Authorization;

public class PostAccessHandler : AuthorizationHandler<PostClientRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PostClientRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Type == "scope" && c.Value.Contains("can_change_clients")))
            context.Succeed(requirement);

        // Scopes are not mandatory and if they are not enough, then we could go to the database of this microservice and fetch permissions with the user's ID 

        return Task.CompletedTask;
    }
}