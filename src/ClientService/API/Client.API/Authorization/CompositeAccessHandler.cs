using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace Client.Api.Authorization;

public class CompositeAccessHandler : IAuthorizationHandler
{
    private readonly GetAccessHandler _getAccessHandler;
    private readonly PostAccessHandler _postAccessHandler;

    public CompositeAccessHandler(GetAccessHandler getAccessHandler, PostAccessHandler postAccessHandler)
    {
        _getAccessHandler = getAccessHandler;
        _postAccessHandler = postAccessHandler;
    }

    public async Task HandleAsync(AuthorizationHandlerContext context)
    {
        await _getAccessHandler.HandleAsync(context);
        await _postAccessHandler.HandleAsync(context);
    }
}