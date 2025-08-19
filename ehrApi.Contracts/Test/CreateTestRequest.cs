namespace ehrApi.Contracts.Test;

public record CreateTestRequest(
    Guid OrderId,
    DateTime DateTimeOrdered,
    string Result
);