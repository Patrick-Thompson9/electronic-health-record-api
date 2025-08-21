namespace ehrApi.Models;

public class Test
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public DateTime DateTimeOrdered { get; set; }
    public DateTime DateTimeCreated { get; set; }
    public string Result { get; set; }

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