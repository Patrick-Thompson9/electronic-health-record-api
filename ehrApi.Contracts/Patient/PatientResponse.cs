using ehrApi.Contracts.Order;

namespace ehrApi.Contracts.Patient;

public record PatientResponse(
    Guid Id,
    string Mrn,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    DateTime DateTimeCreated,
    DateTime LastUpdated,
    List<OrderResponse>? Orders
);
