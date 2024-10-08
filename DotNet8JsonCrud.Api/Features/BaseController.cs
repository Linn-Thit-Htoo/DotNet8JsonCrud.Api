﻿namespace DotNet8JsonCrud.Api.Features;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected IActionResult Content(object obj)
    {
        return Content(obj.Serialize(), "application/json");
    }
}
