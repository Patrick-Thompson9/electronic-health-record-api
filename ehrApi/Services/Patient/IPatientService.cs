using ehrApi.Models;

namespace ehrApi.Services.Patients;

public interface IPatientService
{
    Task CreatePatient(Patient patient);
    Task CreatePatientWithOrders(Patient Patient);
    Task<Patient?> GetPatient(Guid id);
    Task<List<Patient>> GetAllPatients();
    Task<Patient> UpsertPatient(Patient patient);
    Task<bool> DeletePatient(Guid id);
}