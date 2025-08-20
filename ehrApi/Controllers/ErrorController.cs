using Microsoft.AspNetCore.Mvc;

namespace ehrApi.Controllers;

[ApiController]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult HandleError()
    {
        return Problem(
            detail: "An unexpected error occurred.",
            statusCode: 500,
            title: "Internal Server Error"
            );
    }
}