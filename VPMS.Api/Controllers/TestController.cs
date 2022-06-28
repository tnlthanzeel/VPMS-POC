using Microsoft.AspNetCore.Mvc;
using VPMS.Application.Responses;

namespace VPMS.Api.Controllers;


[Route("api/test")]
public class TestController : AppControllerBase
{
    public TestController() { }

    /// <summary>
    /// This is a test controller
    /// </summary>
    /// <returns></returns>
    /// 
    [HttpGet]
    [ProducesResponseType(typeof(ResponseResult<string>), StatusCodes.Status200OK)]
    public IActionResult GetTest()
    {
        var response = new ResponseResult<string>(value: "Test API return 200 ok");
        return Ok(response);
    }


    [HttpGet("throw-internal-server-error")]
    [ProducesResponseType(typeof(ResponseResult<string>), StatusCodes.Status200OK)]
    public IActionResult GetException()
    {
        throw new Exception("This exception is intentional");
        var response = new ResponseResult<string>(value: "Test API return 200 ok");
        return Ok(response);
    }
}
