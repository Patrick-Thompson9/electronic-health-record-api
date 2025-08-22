namespace ehrApi.Services.Generators;

public interface IOrderNumberGenerator
{
    Task<string> GenerateOrderNumber();
}