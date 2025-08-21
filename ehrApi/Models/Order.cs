namespace ehrApi.Models;

public class Order
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public string OrderNumber { get; set; }
    public string OrderType { get; set; }
    public DateTime DateTimeCreated { get; set; }
    public DateTime LastUpdated { get; set; }
    public string? Notes { get; set; }
    public List<Test>? Tests { get; set; }

    public Order(
        Guid id,
        Guid patientId,
        string orderNumber,
        string orderType,
        DateTime dateTimeCreated,
        DateTime lastUpdated,
        string? notes,
        List<Test>? tests
    )
    {
        Id = id;
        PatientId = patientId;
        OrderNumber = orderNumber;
        OrderType = orderType;
        DateTimeCreated = dateTimeCreated;
        LastUpdated = lastUpdated;
        Notes = notes;
        Tests = tests;
    }
}