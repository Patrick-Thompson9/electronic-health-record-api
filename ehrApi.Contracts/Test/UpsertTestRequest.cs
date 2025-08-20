namespace ehrApi.Contracts.Test;

public record UpsertTestRequest(
    Guid OrderId,
    DateTime DateTimeOrdered,
    string Result
);