using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Reclamation.Api.Controllers.V1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
//[Authorize(Policy = "CanRead")]
public class ApiController : ControllerBase
{

}