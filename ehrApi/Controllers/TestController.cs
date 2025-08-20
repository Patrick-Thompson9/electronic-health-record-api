using Microsoft.AspNetCore.Mvc;
using ehrApi.Contracts.Test;
using ehrApi.Contracts.Order;

namespace ehrApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestsController : ControllerBase
{
    [HttpPost()]
    public IActionResult CreateTest(CreateTestRequest request)
    {
        // TODO: Implement the logic to create a test
        return Ok(request);
    }

    [HttpGet()]
    public IActionResult GetAllTests([FromQuery] int limit = 20) // here I could include more parameters
    {
        // TODO: Implement the logic to get all test
        return Ok("List of all tests, default limit is 20");
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetTest(Guid id)
    {
        // TODO: Implement the logic to get a test
        return Ok(id);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertTest(UpsertTestRequest request)
    {
        // TODO: Implement the logic to upsert a test
        return Ok(request);
    }
}