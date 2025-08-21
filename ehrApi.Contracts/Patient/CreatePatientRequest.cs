using ehrApi.Contracts.Order;

namespace ehrApi.Contracts.Patient;

public record CreatePatientRequest(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    List<CreateOrderRequest>? Orders
);