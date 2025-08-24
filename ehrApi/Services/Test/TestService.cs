using ehrApi.Data;
using ehrApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ehrApi.Services.Tests;

public class TestService : ITestService
{
    private readonly EhrApiContext _context;

    public TestService(EhrApiContext context)
    {
        _context = context;
    }

    public async Task CreateTest(Test test)
    {
        _context.Tests.Add(test);
        await _context.SaveChangesAsync();
    }

    public async Task<Test?> GetTest(Guid id)
    {
        return await _context.Tests.FirstOrDefaultAsync(test => test.Id == id);
    }

    public async Task<List<Test>> GetAllTests()
    {
        return await _context.Tests
        .OrderByDescending(test => test.DateTimeCreated)
        .ToListAsync();
    }

    public async Task<(Test, bool, bool)> UpsertTest(Test test)
    {
        bool wasCreated;
        bool invalidMatch = false;

        Test? existingTest = await _context.Tests.FindAsync(test.Id);
        if (existingTest == null)
        {
            await CreateTest(test);
            wasCreated = true;
        }
        else
        {
            if (existingTest.OrderId != test.OrderId)
            {
                wasCreated = false;
                invalidMatch = true;
                return (test, wasCreated, invalidMatch);
            }
            existingTest.Result = test.Result;
            existingTest.LastUpdated = DateTime.UtcNow;
            existingTest.Order = test.Order;
            await _context.SaveChangesAsync();

            wasCreated = false;
        }
        return (existingTest ?? test, wasCreated, invalidMatch);
    }

    public async Task<bool> DeleteTest(Guid id)
    {
        Test? test = await _context.Tests.FindAsync(id);
        if (test != null)
        {
            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}