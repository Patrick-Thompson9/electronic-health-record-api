namespace ehrApi.Models;

public class Test
{
    public Guid Id { get; }
    public Guid OrderId { get; }
    public DateTime DateTimeOrdered { get; }
    public DateTime DateTimeCreated { get; }
    public string Result { get; }

    public Test(
        Guid id,
        Guid orderId,
        DateTime dateTimeOrdered,
        DateTime lastUpdated,
        string result
    )
    {
        Id = id;
        OrderId = orderId;
        DateTimeOrdered = dateTimeOrdered;
        DateTimeCreated = lastUpdated;
        Result = result;
    }
}