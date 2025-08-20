using ehrApi.Models;

namespace ehrApi.Services.Tests;

public class TestService : ITestService
{
    private static readonly Dictionary<Guid, Test> _tests = new();

    public void CreateTest(Test test)
    {
        _tests.Add(test.Id, test);
    }

    public Test GetTest(Guid id)
    {
        return _tests[id];
    }

    public Test UpsertTest(Test test)
    {
        _tests[test.Id] = test;
        return test;
    }

    public void DeleteTest(Guid id)
    {
        _tests.Remove(id);
    }
}