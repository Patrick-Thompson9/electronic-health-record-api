using ehrApi.Models;

namespace ehrApi.Services.Patients;

public interface IPatientService
{
    Task CreatePatient(Patient patient);
    Task CreatePatientWithOrders(Patient Patient);
    Task<Patient?> GetPatient(Guid id);
    Task<Patient?> GetPatientByMrn(string mrn);
    Task<List<Order>?> GetOrdersByMrn(string mrn);
    Task<List<Patient>> GetAllPatients();
    Task<(Patient, bool)> UpsertPatient(Patient patient);
    Task<bool> DeletePatient(Guid id);
}