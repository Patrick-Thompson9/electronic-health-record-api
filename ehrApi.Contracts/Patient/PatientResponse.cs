using ehrApi.Contracts.Order;

namespace ehrApi.Contracts.Patient;

public record PatientResponse(
    Guid Id,
    string MRN,
    string FirstName,
    string LastName,
    DateTime DateTimeCreated,
    DateTime LastUpdated,
    List<OrderResponse>? Orders
);
