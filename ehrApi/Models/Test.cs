namespace ehrApi.Models;

public class Test
{
    public Guid Id { get; set; }
    public string TestType { get; set; }
    public string Result { get; set; }
    public DateTime DateTimeCreated { get; set; }
    public DateTime LastUpdated { get; set; }

    public Guid OrderId { get; set; }
    public Order? Order { get; set; }

    public Test(
        Guid id,
        Guid orderId,
        string testType,
        string result,
        DateTime dateTimeCreated,
        DateTime lastUpdated
    )
    {
        Id = id;
        OrderId = orderId;
        TestType = testType;
        Result = result;
        DateTimeCreated = dateTimeCreated;
        LastUpdated = lastUpdated;
    }
}