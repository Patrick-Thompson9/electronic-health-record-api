using Microsoft.EntityFrameworkCore;

using ehrApi.Services.Patients;
using ehrApi.Services.Orders;
using ehrApi.Services.Tests;
using ehrApi.Services.Generators;
using ehrApi.Data;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<EhrApiContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddControllers();
    builder.Services.AddScoped<IPatientService, PatientService>(); // Registering interfaces for DI container
    builder.Services.AddScoped<IOrderService, OrderService>();
    builder.Services.AddScoped<ITestService, TestService>();
    builder.Services.AddScoped<IMrnGenerator, MrnGenerator>();
    builder.Services.AddScoped<IOrderNumberGenerator, OrderNumberGenerator>();
}

var app = builder.Build();
{
    //app.UseExceptionHandler("/error"); TODO: uncomment this when done debugging
    app.UseHttpsRedirection();
    app.UseRouting();
    app.MapControllers();
    app.Run();
}
