using Microsoft.AspNetCore.Mvc;
using ehrApi.Contracts.Order;
using ehrApi.Models;
using ehrApi.Services.Orders;
using ehrApi.Services.Patients;
using ehrApi.Extensions;

namespace ehrApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IPatientService _patientService;
    public OrdersController(IOrderService orderService, IPatientService patientService)
    {
        _orderService = orderService;
        _patientService = patientService;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        // first check if patient is real
        Patient? patient = await _patientService.GetPatient(request.PatientId);
        if (patient == null)
        {
            return BadRequest("Invalid PatientId");
        }
        Order order = new(
            Guid.NewGuid(),
            request.PatientId,
            "0123456789", // implement logic to generare order number
            request.OrderType,
            DateTime.UtcNow,
            DateTime.UtcNow,
            request.Notes
        );

        await _orderService.CreateOrder(order);

        OrderResponse response = order.ToResponse();

        return CreatedAtAction(
            actionName: nameof(CreateOrder),
            routeValues: new { id = order.Id },
            value: response);
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllOrders([FromQuery] int limit = 20) // here I could include more parameters
    {
        List<Order> orders = await _orderService.GetAllOrders();
        var limitedOrders = orders.Take(limit).ToList();

        var response = limitedOrders.Select(order => order.ToResponse()).ToList();

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {

        Order? order = await _orderService.GetOrder(id);
        if (order == null)
        {
            return NotFound();
        }


        OrderResponse response = order.ToResponse();
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpsertOrder(Guid id, UpsertOrderRequest request)
    {
        Order order = new(
            id,
            request.PatientId,
            "0123456789", // implement logic to generare order number
            request.OrderType,
            DateTime.UtcNow,
            DateTime.UtcNow,
            request.Notes
        );

        await _orderService.UpsertOrder(order);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        bool deleted = await _orderService.DeleteOrder(id);
        return deleted ? NoContent() : NotFound();
    }
}