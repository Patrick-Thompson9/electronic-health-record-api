using Microsoft.AspNetCore.Mvc;
using ehrApi.Contracts.Patient;
using ehrApi.Contracts.Order;
using ehrApi.Services.Patients;
using ehrApi.Services.Generators;
using ehrApi.Extensions;

using ehrApi.Models;
using ehrApi.Contracts.Test;

namespace ehrApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly IMrnGenerator _mrnGenerator;
    public PatientsController(IPatientService patientService, IMrnGenerator mrnGenerator)
    {
        _patientService = patientService;
        _mrnGenerator = mrnGenerator;
    }

    [HttpPost()]
    public async Task<ActionResult> CreatePatient(CreatePatientRequest request)

    {
        Patient patient = new(
            Guid.NewGuid(),
            await _mrnGenerator.GenerateMrn(),
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            DateTime.UtcNow,
            DateTime.UtcNow
        );

        // This was a section to create orders that were included in the body of a create patient request.
        // But to do this, the user forced to pass a dummy PatientId value. To properly implement this I
        // would have to make PatientId optional and make larger scale changes. 

        // if (request.Orders?.Count > 0)
        // {
        //     await _patientService.CreatePatientWithOrders(patient);
        // }
        // else
        // {
        //     await _patientService.CreatePatient(patient);
        // }

        await _patientService.CreatePatient(patient);

        PatientResponse response = patient.ToResponse();
        return CreatedAtAction(
            actionName: nameof(CreatePatient),
            routeValues: new { id = patient.Id },
            value: response);
    }

    [HttpGet()]
    public async Task<ActionResult> GetAllPatients([FromQuery] int limit = 20) // here I could include more parameters
    {
        List<Patient> patients = await _patientService.GetAllPatients();
        List<Patient> limitedPatients = patients.Take(limit).ToList();

        List<PatientResponse> response = limitedPatients.Select(patient => patient.ToResponse()).ToList();

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetPatient(Guid id)
    {
        Patient? patient = await _patientService.GetPatient(id);
        if (patient == null) return NotFound();

        PatientResponse response = patient.ToResponse();
        return Ok(response);
    }

    [HttpGet("mrn/{mrn}")]
    public async Task<ActionResult> GetPatientByMrn(string mrn)
    {
        Patient? patient = await _patientService.GetPatientByMrn(mrn);
        if (patient == null) return NotFound($"Did not find patient with MRN {mrn}");

        PatientResponse response = patient.ToResponse();
        return Ok(response);
    }

    [HttpGet("mrn/{mrn}/orders")]
    public async Task<ActionResult> GetOrdersByMrn(string mrn)
    {
        List<Order>? orders = await _patientService.GetOrdersByMrn(mrn);
        if (orders == null) return NotFound($"Did not find patient with MRN {mrn}");

        List<OrderResponse> response = orders.Select(order => order.ToResponse()).ToList();
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpsertPatient(Guid id, UpsertPatientRequest request)
    {
        Patient patient = new(
            id,
            await _mrnGenerator.GenerateMrn(), // wasteful call if not creating new patient
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            DateTime.UtcNow,
            DateTime.UtcNow // TODO: actually create orders map order creation service?
        );

        (Patient newPatient, bool wasCreated) = await _patientService.UpsertPatient(patient);
        PatientResponse response = newPatient.ToResponse();
        return wasCreated ?
        CreatedAtAction(
            actionName: nameof(UpsertPatient),
            routeValues: new { id = patient.Id },
            value: response) : Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeletePatient(Guid id)
    {
        bool deleted = await _patientService.DeletePatient(id);
        return deleted ? NoContent() : NotFound();
    }

    [HttpPost("submit-test")]
    public async Task<ActionResult> SubmitTest(SubmitTestRequest request)
    {
        Patient? patient = await _patientService.SubmitTest(request);

        if (patient == null) return NotFound($"Invalid MRN: {request.MRN} or Order Number: {request.OrderNumber}");

        PatientResponse response = patient.ToResponse();
        return CreatedAtAction(
            actionName: nameof(SubmitTest), value: response);
    }
}