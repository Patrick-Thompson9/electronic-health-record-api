using ehrApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ehrApi.Services.Generators;

public class MrnGenerator : IMrnGenerator
{
    private readonly EhrApiContext _context;
    public MrnGenerator(EhrApiContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateMrn()
    {
        var lastPatient = await _context.Patients
            .OrderByDescending(patient => patient.Mrn)
            .FirstOrDefaultAsync();

        if (lastPatient == null)
            return "0000000001";

        var lastMrn = int.Parse(lastPatient.Mrn);
        var nextMrn = lastMrn + 1;

        return nextMrn.ToString("D10");
    }
}