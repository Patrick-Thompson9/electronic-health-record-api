using ehrApi.Data;
using ehrApi.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace ehrApi.Services.Patients;

public class PatientService : IPatientService
{
    // TODO: Currently this only stores patient data in this dictionary, need to figure out how to store orders and tests
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

    public async Task<Patient> UpsertPatient(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task DeletePatient(Guid id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient != null)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
    }
}