using ehrApi.Contracts.Test;

namespace ehrApi.Contracts.Order;

public record UpsertOrderRequest(
    Guid PatientId,
    string OrderType,
    string Notes,
    List<UpsertTestRequest>? Tests
);

