using Microsoft.AspNetCore.Mvc;
using ehrApi.Contracts.Patient;
using ehrApi.Contracts.Order;

using ehrApi.Models;

namespace ehrApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    [HttpPost()]
    public IActionResult CreatePatient(CreatePatientRequest request)
    {
        Patient patient = new Patient(
            Guid.NewGuid(),
            "request.MRN", // TODO: implement MRN generation
            request.FirstName,
            request.LastName,
            DateTime.UtcNow,
            DateTime.UtcNow,
            null // TODO: either create orders as we go or null
        );

        // TODO: Add service to handle db interaction

        PatientResponse patientResponse = new(
            Id: patient.Id,
            MRN: patient.MRN,
            FirstName: patient.FirstName,
            LastName: patient.LastName,
            DateTimeCreated: patient.DateTimeCreated,
            LastUpdated: patient.LastUpdated,
            Orders: new List<OrderResponse>() // TODO: actually implement orders
        );


        // TODO: Implement the logic to create a patient
        return Ok(patientResponse);
    }

    [HttpGet()]
    public IActionResult GetAllPatients([FromQuery] int limit = 20) // here I could include more parameters
    {
        // TODO: Implement the logic to get all patient
        return Ok("List of all patients, default limit is 20");
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetPatient(Guid id)
    {
        // TODO: Implement the logic to get a patient
        return Ok(id);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertPatient(UpsertPatientRequest request)
    {
        // TODO: Implement the logic to upsert a patient
        return Ok(request);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeletePatient(Guid id)
    {
        // TODO: Implement the logic to delete a patient
        // this must also delete all orders and tests tied to said patient
        return Ok(id);
    }
}