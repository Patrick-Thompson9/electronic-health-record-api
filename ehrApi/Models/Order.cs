namespace ehrApi.Models;

public class Order
{
    public Guid Id { get; }
    public Guid PatientId { get; }
    public string OrderNumber { get; }
    public DateTime DateTimeCreated { get; }
    public DateTime LastUpdated { get; }
    public string? Notes { get; }
    public List<Test>? Tests { get; }

    public Order(
        Guid id,
        Guid patientId,
        string orderNumber,
        DateTime dateTimeCreated,
        DateTime lastUpdated,
        string? notes,
        List<Test>? tests
    )
    {
        Id = id;
        PatientId = patientId;
        OrderNumber = orderNumber;
        DateTimeCreated = dateTimeCreated;
        LastUpdated = lastUpdated;
        Notes = notes;
        Tests = tests;
    }
}