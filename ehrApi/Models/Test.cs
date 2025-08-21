namespace ehrApi.Models;

public class Test
{
    public Guid Id { get; set; }
    public DateTime DateTimeOrdered { get; set; }
    public DateTime DateTimeCreated { get; set; }
    public string Result { get; set; }

    public Guid OrderId { get; set; }
    public Order? Order { get; set; }

    public Test(
        Guid id,
        Guid orderId,
        DateTime dateTimeOrdered,
        DateTime dateTimeCreated,
        string result
    )
    {
        Id = id;
        OrderId = orderId;
        DateTimeOrdered = dateTimeOrdered;
        DateTimeCreated = dateTimeCreated;
        Result = result;
    }
}