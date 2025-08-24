namespace ehrApi.Contracts.Test;

public record SubmitTestRequest(
    string MRN,
    string OrderNumber,
    string OrderType,
    string Result
);