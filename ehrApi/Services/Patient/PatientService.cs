using ehrApi.Data;
using ehrApi.Models;
using ehrApi.Services.Orders;
using ehrApi.Services.Tests;
using ehrApi.Services.Generators;
using Microsoft.EntityFrameworkCore;
using ehrApi.Contracts.Test;

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

    // This was a service function to create orders inside the body of a create patient request.
    // I commented it out since the contracts/models aren't properly set up to perform this. Also
    // there is an error in this function anyway, an always blank patient.Orders is being examined
    // here but really this function should receive the request.Orders separately and use that.

    // public async Task CreatePatientWithOrders(Patient patient)
    // {
    //     //TODO: Make transactional so errors mid way are caught
    //     _context.Patients.Add(patient);
    //     foreach (var orderReq in patient.Orders)
    //     {
    //         Order order = new(
    //             Guid.NewGuid(),
    //             patient.Id,
    //             await _orderNumberGenerator.GenerateOrderNumber(),
    //             orderReq.OrderType,
    //             DateTime.UtcNow,
    //             DateTime.UtcNow,
    //             orderReq.Notes
    //             );

    //         await _orderService.CreateOrder(order, false);
    //     }
    //     ;
    //     await _context.SaveChangesAsync();
    // }

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

    public async Task<Patient?> SubmitTest(SubmitTestRequest request)
    {
        var (mrn, orderNumber, orderType, result, notes) = request;

        Patient? patient = await GetPatientByMrn(mrn);
        if (patient == null) return null;

        // I originally had logic in here to create the order if it didnt exist however my implementation
        // allowed users to specify their own order number which could cause problems with how I am 
        // currently generating order numbers. Also if the order number didn't exist I will assume this
        // was a mistake by the user and not create a new order with a generated (different) order number.

        Order? order = await _orderService.GetOrderByOrderNumber(orderNumber);

        // return null if order number doesnt exist or belongs to patient with different MRN
        if (order == null) return null;
        if (order.PatientId != patient.Id) return null;

        order = new(
                order.Id,
                patient.Id,
                order.OrderNumber,
                orderType,
                order.DateTimeCreated,
                DateTime.UtcNow,
                notes
            );

        (Order newOrder, bool wasCreated) = await _orderService.UpsertOrder(order);

        Test test = new(
            Guid.NewGuid(),
            newOrder.Id,
            result,
            DateTime.UtcNow,
            DateTime.UtcNow
        );

        await _testService.CreateTest(test);
        return patient;
    }
}
