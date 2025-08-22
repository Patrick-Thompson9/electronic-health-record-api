namespace ehrApi.Contracts.Test;

public record UpsertTestRequest(
    Guid OrderId,
    string TestType,
    string Result
);