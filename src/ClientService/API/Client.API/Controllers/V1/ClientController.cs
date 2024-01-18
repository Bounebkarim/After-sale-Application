using Client.API.Helpers;
using Client.Application.Contracts.Specs;
using Client.Application.Features.ClientFeatures.Commands.CreateClient;
using Client.Application.Features.ClientFeatures.Commands.DeleteClient;
using Client.Application.Features.ClientFeatures.Commands.UpdateClient;
using Client.Application.Features.ClientFeatures.Queries.GetClientDetails;
using Client.Application.Features.ClientFeatures.Queries.GetClients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.API.Controllers.V1;

public class ClientController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IUriService _uriService;

    public ClientController(IMediator mediator, IUriService uriService)
    {
        _mediator = mediator;
        _uriService = uriService;
    }

    [HttpGet]
    [Authorize(Policy = "CanReadClients")]
    public async Task<Pagination<ClientDto>> Get([FromQuery] SpecParams specParams)
    {
        var data = await _mediator.Send(new GetClientQuery(specParams));
        var route = Request.Path.Value;
        return PaginationHelper.CreatePagedReponse<ClientDto>(data, _uriService, route);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "CanReadClients")]
    public async Task<ActionResult<ClientDetailDto>> Get(Guid id)
    {
        var client = await _mediator.Send(new GetClientDetailQuery(id));
        return Ok(client);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "CanWriteClients")]
    public async Task<ActionResult> Post(CreateClientCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Authorize(Policy = "CanWriteClients")]
    public async Task<ActionResult> Put(UpdateClientCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Authorize(Policy = "CanWriteClients")]
    public async Task<ActionResult> Delete(DeleteClientCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}