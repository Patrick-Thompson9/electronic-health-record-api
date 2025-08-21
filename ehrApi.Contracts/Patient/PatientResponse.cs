using ehrApi.Contracts.Order;

namespace ehrApi.Contracts.Patient;

public record PatientResponse(
    // TODO: Add date of birth
    Guid Id,
    string Mrn,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    DateTime DateTimeCreated,
    DateTime LastUpdated,
    List<OrderResponse>? Orders
);
