using ehrApi.Contracts.Test;

namespace ehrApi.Contracts.Order;

public record OrderResponse(
    Guid Id,
    Guid PatientId,
    string OrderNumber,
    string OrderType,
    DateTime DateTimeCreated,
    DateTime LastUpdated,
    string? Notes,
    TestResponse? Test
);
