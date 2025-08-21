using ehrApi.Models;

namespace ehrApi.Services.Patients;

public interface IPatientService
{
    Task CreatePatient(Patient patient);
    Task<Patient?> GetPatient(Guid id);
    Task<Patient> UpsertPatient(Patient patient);
    Task DeletePatient(Guid id);
}