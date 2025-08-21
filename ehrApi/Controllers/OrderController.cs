using Microsoft.AspNetCore.Mvc;
using ehrApi.Contracts.Order;
using ehrApi.Models;
using ehrApi.Services.Orders;
using ehrApi.Contracts.Test;

namespace ehrApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost()]
    public IActionResult CreateOrder(CreateOrderRequest request)
    {
        Order order = new(
            Guid.NewGuid(),
            request.PatientId,
            "0123456789", // implement logic to generare order number
            request.OrderType,
            DateTime.UtcNow,
            DateTime.UtcNow,
            request.Notes
        );

        _orderService.CreateOrder(order);

        OrderResponse response = new(
            order.Id,
            order.PatientId,
            order.OrderNumber,
            order.OrderType,
            order.DateTimeCreated,
            order.LastUpdated,
            order.Notes,
            null // TODO: Add test response
        );

        return CreatedAtAction(
            actionName: nameof(CreateOrder),
            routeValues: new { id = order.Id },
            value: response);
    }

    [HttpGet()]
    public IActionResult GetAllOrders([FromQuery] int limit = 20) // here I could include more parameters
    {
        return Ok("List of all orders, default limit is 20");
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetOrder(Guid id)
    {

        Order order = _orderService.GetOrder(id);

        OrderResponse response = new(
            order.Id,
            order.PatientId,
            order.OrderNumber,
            order.OrderType,
            order.DateTimeCreated,
            order.LastUpdated,
            order.Notes,
            null // TODO: Add test response
        );

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertOrder(UpsertOrderRequest request)
    {
        Order order = new(
            Guid.NewGuid(),
            request.PatientId,
            "0123456789", // implement logic to generare order number
            request.OrderType,
            DateTime.UtcNow,
            DateTime.UtcNow,
            request.Notes
        );

        _orderService.UpsertOrder(order);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteOrder(Guid id)
    {
        _orderService.DeleteOrder(id);
        // TODO: implement logic to delete all associated tests
        return NoContent();
    }
}