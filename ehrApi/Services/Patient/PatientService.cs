using ehrApi.Data;
using ehrApi.Models;
using ehrApi.Services.Orders;
using Microsoft.EntityFrameworkCore;

namespace ehrApi.Services.Patients;

public class PatientService : IPatientService
{
    private readonly EhrApiContext _context;
    private readonly IOrderService _orderService;

    public PatientService(EhrApiContext context, IOrderService orderService)
    {
        _context = context;
        _orderService = orderService;
    }

    public async Task CreatePatient(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
    }

    public async Task CreatePatientWithOrders(Patient patient)
    {
        //TODO: Make transactional so errors mid way are caught
        _context.Patients.Add(patient);
        foreach (var orderReq in patient.Orders)
        {
            Order order = new(
                Guid.NewGuid(),
                patient.Id,
                "0123456789", // implement logic to generare order number
                orderReq.OrderType,
                DateTime.UtcNow,
                DateTime.UtcNow,
                orderReq.Notes
                );

            await _orderService.CreateOrder(order, false);
        }
        ;
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