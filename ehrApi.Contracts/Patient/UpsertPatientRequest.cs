using ehrApi.Contracts.Order;

namespace ehrApi.Contracts.Patient;

public record UpsertPatientRequest(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    List<UpsertOrderRequest>? Orders
);