using Microsoft.AspNetCore.Mvc;
using ehrApi.Contracts.Patient;
using ehrApi.Contracts.Order;

namespace ehrApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    [HttpPost()]
    public IActionResult CreatePatient(CreatePatientRequest request)
    {
        // TODO: Implement the logic to create a patient
        return Ok(request);
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
}