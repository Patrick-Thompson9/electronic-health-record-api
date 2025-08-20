using ehrApi.Services.Patients;
using ehrApi.Services.Orders;
using ehrApi.Services.Tests;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<IPatientService, PatientService>(); // Registering interfaces for DI container
    builder.Services.AddScoped<IOrderService, OrderService>();
    builder.Services.AddScoped<ITestService, TestService>();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseRouting();
    app.MapControllers();
    app.Run();
}
