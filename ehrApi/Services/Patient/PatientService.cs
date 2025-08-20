using ehrApi.Models;

namespace ehrApi.Services.Patients;

public class PatientService : IPatientService
{
    // TODO: Currently this only stores patient data in this dictionary, need to figure out how to store orders and tests
    private static readonly Dictionary<Guid, Patient> _patients = new();

    public void CreatePatient(Patient patient)
    {
        _patients.Add(patient.Id, patient);
    }

    public Patient GetPatient(Guid id)
    {

        return _patients[id];
    }

    public Patient UpsertPatient(Patient patient)
    {
        _patients[patient.Id] = patient;
        return patient;
    }

    public void DeletePatient(Guid id)
    {
        _patients.Remove(id);
    }
}