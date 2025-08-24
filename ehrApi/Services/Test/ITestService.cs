using ehrApi.Models;

namespace ehrApi.Services.Tests;

public interface ITestService
{
    Task CreateTest(Test test);
    Task<Test?> GetTest(Guid id);
    Task<List<Test>> GetAllTests();
    Task<(Test, bool, bool)> UpsertTest(Test test);
    Task<bool> DeleteTest(Guid id);
}