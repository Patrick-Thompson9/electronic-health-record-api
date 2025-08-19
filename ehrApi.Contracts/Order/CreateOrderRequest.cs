using TestModel = ehrApi.Contracts.Models.Test;

namespace ehrApi.Contracts.Order;

public record CreateOrderRequest(
    Guid PatientId,
    string Notes,
    List<TestModel>? Tests = null
);

