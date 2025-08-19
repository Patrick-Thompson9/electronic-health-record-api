namespace ehrApi.Contracts.Test;

public record TestResponse(
    Guid Id,
    Guid OrderId,
    DateTime DateTimeOrdered,
    DateTime DateTimeCreated,
    string Result
);