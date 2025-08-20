using ehrApi.Models;

namespace ehrApi.Services.Patients;

public interface IPatientService
{
    void CreatePatient(Patient patient);
    Patient GetPatient(Guid id);
    Patient UpsertPatient(Patient patient);
    void DeletePatient(Guid id);
}