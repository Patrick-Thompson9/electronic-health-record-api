using ehrApi.Contracts.Patient;
using ehrApi.Contracts.Order;
using ehrApi.Contracts.Test;
using ehrApi.Models;

namespace ehrApi.Extensions;

public static class MappingResponseExtensions
{
    public static PatientResponse ToResponse(this Patient patient)
    {
        return new PatientResponse(
            patient.Id,
            patient.Mrn,
            patient.FirstName,
            patient.LastName,
            patient.DateOfBirth,
            patient.DateTimeCreated,
            patient.LastUpdated,
            patient.Orders.Select(order => order.ToResponse()).ToList()
        );
    }

    public static OrderResponse ToResponse(this Order order)
    {
        return new OrderResponse(
            order.Id,
            order.PatientId,
            order.OrderNumber,
            order.OrderType,
            order.DateTimeCreated,
            order.LastUpdated,
            order.Notes,
            order.Test != null ? order.Test.ToResponse() : null
        );
    }

    public static TestResponse ToResponse(this Test test)
    {
        return new TestResponse(
            test.Id,
            test.OrderId,
            test.TestType,
            test.Result,
            test.DateTimeCreated,
            test.LastUpdated
        );
    }
}