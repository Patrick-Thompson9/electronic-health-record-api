using ehrApi.Data;
using ehrApi.Models;
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
        return await _context.Tests.ToListAsync();
    }

    public async Task<Test> UpsertTest(Test test)
    {
        Test? existingTest = await _context.Tests.FindAsync(test.Id);
        if (existingTest == null)
        {

            await CreateTest(test);
        }
        else
        {
            existingTest.Result = test.Result;
            existingTest.OrderId = test.OrderId;
            existingTest.LastUpdated = DateTime.UtcNow;
            existingTest.Order = test.Order;
            await _context.SaveChangesAsync();
        }
        return existingTest ?? test;
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