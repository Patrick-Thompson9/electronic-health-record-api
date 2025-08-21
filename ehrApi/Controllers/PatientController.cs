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
            DateTime.UtcNow,
            DateTime.UtcNow,
            null // TODO: either create orders as we go or null
        );

        _patientService.CreatePatient(patient);

        PatientResponse response = new(
            Id: patient.Id,
            MRN: patient.MRN,
            FirstName: patient.FirstName,
            LastName: patient.LastName,
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
    public IActionResult GetPatient(Guid id)
    {
        Patient patient = _patientService.GetPatient(id);

        PatientResponse response = new(
            patient.Id,
            patient.MRN,
            patient.FirstName,
            patient.LastName,
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
            DateTime.UtcNow,
            DateTime.UtcNow,
            null
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