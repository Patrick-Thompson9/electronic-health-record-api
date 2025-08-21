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
    public IActionResult CreatePatient(CreatePatientRequest request)
    // TODO: for scalability chnage to public async Task<ActionResult<PatientResponse>>  
    {
        Patient patient = new(
            Guid.NewGuid(),
            "request.MRN", // TODO: implement MRN generation
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            DateTime.UtcNow,
            DateTime.UtcNow,
            new List<Order>() // TODO: actually create orders map order creation service?
        );

        _patientService.CreatePatient(patient);

        PatientResponse response = new(
            Id: patient.Id,
            MRN: patient.MRN,
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

    [HttpGet()]
    public IActionResult GetAllPatients([FromQuery] int limit = 20) // here I could include more parameters
    {
        // TODO: Implement the logic to get all patient
        return Ok($"List of all patients, default limit is {limit}");
    }

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
            patient.MRN,
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
    public IActionResult UpsertPatient(UpsertPatientRequest request)
    {
        Patient patient = new(
            Guid.NewGuid(),
            "0123456789",
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            DateTime.UtcNow,
            DateTime.UtcNow,
            new List<Order>()
        );

        _patientService.UpsertPatient(patient);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeletePatient(Guid id)
    {
        _patientService.DeletePatient(id);
        // this must also delete all orders and tests tied to said patient
        return NoContent();
    }
}