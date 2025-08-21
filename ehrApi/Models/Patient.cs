namespace ehrApi.Models;

public class Patient
{
    public Guid Id { get; set; }
    public string MRN { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public DateTime DateTimeCreated { get; set; }
    public DateTime LastUpdated { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public Patient(
        Guid id,
        string mrn,
        string firstName,
        string lastName,
        DateOnly dateOfBirth,
        DateTime dateTimeCreated,
        DateTime lastUpdated,
        ICollection<Order> orders
    )
    {
        Id = id;
        MRN = mrn;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        DateTimeCreated = dateTimeCreated;
        LastUpdated = lastUpdated;
        Orders = orders;
    }
}