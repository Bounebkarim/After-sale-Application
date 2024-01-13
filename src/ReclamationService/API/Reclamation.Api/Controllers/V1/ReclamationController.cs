using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reclamation.Api.Helpers;
using Reclamation.Application.Contracts.Specs;
using Reclamation.Application.Features.ReclamationFeature.Commands.CreateReclamation;
using Reclamation.Application.Features.ReclamationFeature.Commands.DeleteReclamation;
using Reclamation.Application.Features.ReclamationFeature.Commands.UpdateReclamation;
using Reclamation.Application.Features.ReclamationFeature.Queries.GetReclamationDetails;
using Reclamation.Application.Features.ReclamationFeature.Queries.GetReclamations;

namespace Reclamation.Api.Controllers.V1;

public class ReclamationController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IUriService _uriService;

    public ReclamationController(IMediator mediator, IUriService uriService)
    {
        _mediator = mediator;
        _uriService = uriService;
    }

    [HttpGet]
    public async Task<Pagination<ReclamationDto>> Get([FromQuery] SpecParams specParams)
    {
        var data = await _mediator.Send(new GetReclamationsQuery(specParams));
        string? route = Request.Path.Value;
        return PaginationHelper.CreatePagedReponse<ReclamationDto>(data, _uriService, route);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReclamationDetailsDto>> Get(Guid id)
    {
        ReclamationDetailsDto client = await _mediator.Send(new GetReclamationDetailsQuery(id));
        return Ok(client);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateReclamationCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateReclamationCommand command)
    {
        await _mediator.Send((command));
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(DeleteReclamationCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
