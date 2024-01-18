﻿using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.API.Controllers.V1;

[Authorize]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
//[Authorize(Policy = "CanRead")]
public class ApiController : ControllerBase
{
}