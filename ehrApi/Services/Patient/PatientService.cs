using ehrApi.Data;
using ehrApi.Models;
using ehrApi.Services.Orders;
using ehrApi.Services.Tests;
using ehrApi.Services.Generators;
using Microsoft.EntityFrameworkCore;

namespace ehrApi.Services.Patients;

public class PatientService : IPatientService
{
    private readonly EhrApiContext _context;
    private readonly IOrderService _orderService;
    private readonly ITestService _testService;
    private readonly IOrderNumberGenerator _orderNumberGenerator;

    public PatientService(
        EhrApiContext context,
        IOrderService orderService,
        IOrderNumberGenerator orderNumberGenerator,
        ITestService testService)
    {
        _context = context;
        _orderService = orderService;
        _orderNumberGenerator = orderNumberGenerator;
        _testService = testService;
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
                await _orderNumberGenerator.GenerateOrderNumber(),
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
        .Include(patient => patient.Orders.OrderByDescending(order => order.OrderNumber))
        .ThenInclude(order => order.Test)
        .FirstOrDefaultAsync(patient => patient.Id == id);
    }

    public async Task<Patient?> GetPatientByMrn(string mrn)
    {
        return await _context.Patients
        .Include(patient => patient.Orders.OrderByDescending(order => order.OrderNumber))
        .ThenInclude(order => order.Test)
        .FirstOrDefaultAsync(patient => patient.Mrn == mrn);
    }

    public async Task<List<Order>?> GetOrdersByMrn(string mrn)
    {
        Patient? patient = await _context.Patients
        .Include(patient => patient.Orders.OrderByDescending(order => order.OrderNumber))
        .ThenInclude(order => order.Test)
        .FirstOrDefaultAsync(patient => patient.Mrn == mrn);

        if (patient == null) return null;

        return patient.Orders.ToList();
    }

    public async Task<List<Patient>> GetAllPatients()
    {
        return await _context.Patients
        .Include(patient => patient.Orders.OrderByDescending(order => order.OrderNumber))
        .ThenInclude(order => order.Test)
        .OrderByDescending(patient => patient.Mrn)
        .ToListAsync();
    }

    public async Task<(Patient, bool)> UpsertPatient(Patient patient)
    {
        bool wasCreated;

        Patient? existingPatient = await _context.Patients
        .Include(patient => patient.Orders.OrderByDescending(order => order.OrderNumber))
        .ThenInclude(order => order.Test)
        .FirstOrDefaultAsync(p => p.Id == patient.Id);

        if (existingPatient == null)
        {
            await CreatePatient(patient);
            wasCreated = true;
        }
        else
        {
            // not a great solution for something with many properties
            // Currently cant edit MRN or Orders with this update function
            existingPatient.FirstName = patient.FirstName;
            existingPatient.LastName = patient.LastName;
            existingPatient.DateOfBirth = patient.DateOfBirth;
            existingPatient.LastUpdated = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            wasCreated = false;
        }
        return (existingPatient ?? patient, wasCreated);
    }

    public async Task<bool> DeletePatient(Guid id)
    {
        Patient? patient = await _context.Patients.FindAsync(id);
        if (patient != null)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<Patient?> SubmitTest(string mrn, string orderNumber, string result)
    {
        Patient? patient = await GetPatientByMrn(mrn);
        if (patient == null) return null;

        Order? order = patient.Orders.FirstOrDefault(order => order.OrderNumber == orderNumber);
        if (order == null) return null;

        Test test = new(
            Guid.NewGuid(),
            order.Id,
            result,
            DateTime.UtcNow,
            DateTime.UtcNow
        );

        await _testService.CreateTest(test);
        return patient;
    }
}