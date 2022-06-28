using Microsoft.AspNetCore.Mvc;
using System.Net;
using VPMS.Application.Responses;

namespace VPMS.Api.Controllers;

[ApiController]
public abstract class AppControllerBase : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    protected ObjectResult UnsuccessfullResponse<T>(ResponseResult<T> responseResult)
    {
        ErrorResponse errorResponse = new()
        {
            Errors = responseResult.Errors
        };

        if (responseResult.HttpStatusCode == HttpStatusCode.BadRequest)
            return BadRequest(errorResponse);

        else if (responseResult.HttpStatusCode == HttpStatusCode.NotFound)
            return NotFound(errorResponse);

        else
            return StatusCode(statusCode: (int)HttpStatusCode.InternalServerError, errorResponse);

    }

    [ApiExplorerSettings(IgnoreApi = true)]
    protected ObjectResult UnsuccessfullResponse(ResponseResult responseResult)
    {
        ErrorResponse errorResponse = new()
        {
            Errors = responseResult.Errors
        };

        if (responseResult.HttpStatusCode == HttpStatusCode.BadRequest)
            return BadRequest(errorResponse);

        else if (responseResult.HttpStatusCode == HttpStatusCode.NotFound)
            return NotFound(errorResponse);

        else
            return StatusCode(statusCode: (int)HttpStatusCode.InternalServerError, errorResponse);

    }
}
