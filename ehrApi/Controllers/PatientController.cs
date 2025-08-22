using Microsoft.AspNetCore.Mvc;
using ehrApi.Contracts.Patient;
using ehrApi.Contracts.Order;
using ehrApi.Services.Patients;

using ehrApi.Models;

namespace ehrApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;
    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpPost()]
    public async Task<ActionResult> CreatePatient(CreatePatientRequest request)

    {
        Patient patient = new(
            Guid.NewGuid(),
            "request.MRN", // TODO: implement MRN generation
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            DateTime.UtcNow,
            DateTime.UtcNow // TODO: actually create orders map order creation service?
        );

        await _patientService.CreatePatient(patient);

        PatientResponse response = new(
            Id: patient.Id,
            Mrn: patient.Mrn,
            FirstName: patient.FirstName,
            LastName: patient.LastName,
            DateOfBirth: patient.DateOfBirth,
            DateTimeCreated: patient.DateTimeCreated,
            LastUpdated: patient.LastUpdated,
            Orders: new List<OrderResponse>() // TODO: actually implement orders
        );


        // TODO: Implement the logic to create a patient
        return CreatedAtAction(
            actionName: nameof(CreatePatient),
            routeValues: new { id = patient.Id },
            value: response);
    }

    // [HttpGet()]
    // public async Task<ActionResult> GetAllPatients([FromQuery] int limit = 20) // here I could include more parameters
    // {
    //     // TODO: Implement the logic to get all patient
    //     return Ok($"List of all patients, default limit is {limit}");
    // }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetPatient(Guid id)
    {
        Patient? patient = await _patientService.GetPatient(id);
        if (patient == null)
        {
            return NotFound();
        }

        PatientResponse response = new(
            patient.Id,
            patient.Mrn,
            patient.FirstName,
            patient.LastName,
            patient.DateOfBirth,
            patient.DateTimeCreated,
            patient.LastUpdated,
            new List<OrderResponse>()
        );

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpsertPatient(Guid id, UpsertPatientRequest request)
    {
        Patient patient = new(
            id,
            "request.MRN", // TODO: implement MRN generation
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            DateTime.UtcNow,
            DateTime.UtcNow // TODO: actually create orders map order creation service?
        );

        await _patientService.UpsertPatient(patient);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeletePatient(Guid id)
    {
        bool deleted = await _patientService.DeletePatient(id);
        return deleted ? NoContent() : NotFound();
    }
}