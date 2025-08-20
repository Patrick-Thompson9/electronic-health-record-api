namespace ehrApi.Models;

public class Patient
{
    public Guid Id { get; }
    public string MRN { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public DateTime DateTimeCreated { get; }
    public DateTime LastUpdated { get; }
    public List<Order>? Orders { get; }
    public Patient(
        Guid id,
        string mrn,
        string firstName,
        string lastName,
        DateTime dateTimeCreated,
        DateTime lastUpdated,
        List<Order>? orders
    )
    {
        Id = id;
        MRN = mrn;
        FirstName = firstName;
        LastName = lastName;
        DateTimeCreated = dateTimeCreated;
        LastUpdated = lastUpdated;
        Orders = orders ?? new();
    }
}