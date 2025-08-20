using Microsoft.AspNetCore.Mvc;
using ehrApi.Contracts.Patient;
using ehrApi.Contracts.Order;

namespace ehrApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    [HttpPost()]
    public IActionResult CreateOrder(CreateOrderRequest request)
    {
        // TODO: Implement the logic to create a order
        return Ok(request);
    }

    [HttpGet()]
    public IActionResult GetAllOrders([FromQuery] int limit = 20) // here I could include more parameters
    {
        // TODO: Implement the logic to get all order
        return Ok("List of all orders, default limit is 20");
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetOrder(Guid id)
    {
        // TODO: Implement the logic to get a order
        return Ok(id);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertOrder(UpsertOrderRequest request)
    {
        // TODO: Implement the logic to upsert a order
        return Ok(request);
    }
}