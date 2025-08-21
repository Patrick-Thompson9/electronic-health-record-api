using ehrApi.Contracts.Test;

namespace ehrApi.Contracts.Order;

public record CreateOrderRequest(
    Guid PatientId,
    string OrderType,
    string Notes,
    CreateTestRequest? Test
);

