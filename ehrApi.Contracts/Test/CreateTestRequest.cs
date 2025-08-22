namespace ehrApi.Contracts.Test;

public record CreateTestRequest(
    Guid OrderId,
    string Result
);