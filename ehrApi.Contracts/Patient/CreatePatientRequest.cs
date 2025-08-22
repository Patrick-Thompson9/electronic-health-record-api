using ehrApi.Contracts.Order;
using ehrApi.Validation;

namespace ehrApi.Contracts.Patient;

public record CreatePatientRequest(
    string FirstName,
    string LastName,
    [DateOfBirthValidator]
    DateOnly DateOfBirth,
    List<CreateOrderRequest>? Orders
);