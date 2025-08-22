using Microsoft.AspNetCore.Mvc;
using ehrApi.Contracts.Test;
using ehrApi.Models;
using ehrApi.Services.Tests;
using ehrApi.Services.Orders;
using ehrApi.Extensions;

namespace ehrApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestsController : ControllerBase
{
    private readonly ITestService _testService;
    private readonly IOrderService _orderService;
    public TestsController(ITestService testService, IOrderService orderService)
    {
        _testService = testService;
        _orderService = orderService;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateTest(CreateTestRequest request)
    {
        // first check if order is real
        Order? order = await _orderService.GetOrder(request.OrderId);
        if (order == null)
        {
            return BadRequest("Invalid OrderId");
        }

        Test test = new(
            Guid.NewGuid(),
            request.OrderId,
            request.Result,
            DateTime.UtcNow,
            DateTime.UtcNow
        );

        await _testService.CreateTest(test);

        TestResponse response = test.ToResponse();

        return CreatedAtAction(
            actionName: nameof(CreateTest),
            routeValues: new { id = test.Id },
            value: response);
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllTests([FromQuery] int limit = 20) // here I could include more parameters
    {
        List<Test> tests = await _testService.GetAllTests();
        List<Test> limitedTests = tests.Take(limit).ToList();

        List<TestResponse> response = tests.Select(test => test.ToResponse()).ToList();

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTest(Guid id)
    {
        Test? test = await _testService.GetTest(id);
        if (test == null)
        {
            return NotFound();
        }

        TestResponse response = test.ToResponse();
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpsertTest(Guid id, UpsertTestRequest request)
    {
        Test test = new(
            id,
            request.OrderId,
            request.Result,
            DateTime.UtcNow,
            DateTime.UtcNow
        );

        await _testService.UpsertTest(test);

        return NoContent(); // TODO: Return updated value instead
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTest(Guid id)
    {
        bool deleted = await _testService.DeleteTest(id);
        return deleted ? NoContent() : NotFound();
    }
}