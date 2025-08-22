using ehrApi.Contracts.Order;
using ehrApi.Validation;

namespace ehrApi.Contracts.Patient;

public record UpsertPatientRequest(
    string FirstName,
    string LastName,
    [DateOfBirthValidator]
    DateOnly DateOfBirth,
    List<UpsertOrderRequest>? Orders
);