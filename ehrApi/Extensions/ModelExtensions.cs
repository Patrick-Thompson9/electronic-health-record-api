using ehrApi.Contracts.Patient;
using ehrApi.Contracts.Order;
using ehrApi.Contracts.Test;
using ehrApi.Models;

namespace ehrApi.Extensions;

public static class MappingRequestExtensions
{
    public static Patient ToModel(this CreatePatientRequest request)
    {
        return new Patient(
            Guid.NewGuid(),
            "request.MRN", // TODO: implement MRN generation
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            DateTime.UtcNow,
            DateTime.UtcNow // TODO: actually create orders map order creation service?
                            //request.Orders != null ? request.Orders.Select(orderReq => _orderService.CreateOrder(orderReq.ToRequest()))
        );
    }

    public static Order ToModel(this CreateOrderRequest request)
    {
        return new Order(
            Guid.NewGuid(),
            request.PatientId,
            "0123456789", // implement logic to generare order number
            request.OrderType,
            DateTime.UtcNow,
            DateTime.UtcNow,
            request.Notes
        );
    }

    public static Test ToModel(this CreateTestRequest request)
    {
        return new Test(
            Guid.NewGuid(),
            request.OrderId,
            request.TestType,
            request.Result,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
    }
}