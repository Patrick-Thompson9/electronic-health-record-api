namespace ehrApi.Services.Generators;

public interface IMrnGenerator
{
    Task<string> GenerateMrn();
}