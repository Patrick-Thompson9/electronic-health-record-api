using ehrApi.Models;

namespace ehrApi.Services.Tests;

public interface ITestService
{
    void CreateTest(Test test);
    Test GetTest(Guid id);
    Test UpsertTest(Test test);
    void DeleteTest(Guid id);
}