using ehrApi.Data;
using ehrApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ehrApi.Services.Patients;

public class PatientService : IPatientService
{
    private readonly EhrApiContext _context;

    public PatientService(EhrApiContext context)
    {
        _context = context;
    }

    public async Task CreatePatient(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
    }

    public async Task<Patient?> GetPatient(Guid id)
    {
        return await _context.Patients
        .Include(patient => patient.Orders)
        .ThenInclude(order => order.Test)
        .FirstOrDefaultAsync(patient => patient.Id == id);
    }

    public async Task<List<Patient>> GetAllPatients()
    {
        return await _context.Patients
        .Include(patient => patient.Orders)
        .ThenInclude(order => order.Test)
        .ToListAsync();
    }

    public async Task<Patient> UpsertPatient(Patient patient)
    {
        var existingPatient = await _context.Patients.FindAsync(patient.Id);
        if (existingPatient == null)
        {

            await CreatePatient(patient);
        }
        else
        {
            // not a great solution for something with many properties but good enough for now.
            existingPatient.Mrn = patient.Mrn;
            existingPatient.FirstName = patient.FirstName;
            existingPatient.DateOfBirth = patient.DateOfBirth;
            existingPatient.LastUpdated = DateTime.UtcNow;
            existingPatient.Orders = patient.Orders;
            await _context.SaveChangesAsync();
        }
        return existingPatient ?? patient;
    }

    public async Task<bool> DeletePatient(Guid id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient != null)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}