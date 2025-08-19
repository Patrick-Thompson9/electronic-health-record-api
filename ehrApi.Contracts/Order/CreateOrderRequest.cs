using ehrApi.Contracts.Test;

namespace ehrApi.Contracts.Order;

public record CreateOrderRequest(
    Guid PatientId,
    string Notes,
    List<CreateTestRequest>? Tests
);

