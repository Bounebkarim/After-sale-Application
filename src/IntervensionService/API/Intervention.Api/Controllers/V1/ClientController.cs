using Client.API.Controllers.V1;
using Intervention.Api.Helpers;
using Intervention.Application.Contracts.Specs;
using Intervention.Application.Features.InterventionFeature.Commands.CreateIntervention;
using Intervention.Application.Features.InterventionFeature.Commands.DeleteIntervention;
using Intervention.Application.Features.InterventionFeature.Commands.UpdateIntervention;
using Intervention.Application.Features.InterventionFeature.Queries.GetInterventionDetails;
using Intervention.Application.Features.InterventionFeature.Queries.GetInterventions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Intervention.Api.Controllers.V1;

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
    public async Task<Pagination<InterventionDto>> Get([FromQuery] SpecParams specParams)
    {
        var data = await _mediator.Send(new GetInterventionsQuery(specParams));
        string? route = Request.Path.Value;
        return PaginationHelper.CreatePagedReponse<InterventionDto>(data, _uriService, route);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InterventionDetailsDto>> Get(Guid id)
    {
        InterventionDetailsDto client = await _mediator.Send(new GetInterventionDetailsQuery(id));
        return Ok(client);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateInterventionCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateInterventionCommand command)
    {
        await _mediator.Send((command));
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(DeleteInterventionCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
