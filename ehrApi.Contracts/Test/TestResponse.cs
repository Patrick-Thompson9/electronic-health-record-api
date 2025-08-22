namespace ehrApi.Contracts.Test;

public record TestResponse(
    Guid Id,
    Guid OrderId,
    string TestType,
    string Result,
    DateTime DateTimeCreated,
    DateTime LastUpdated
);