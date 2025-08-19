var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    //builder.Services.AddScoped<IPatientServices,PatientServices>(); 
    //builder.Services.AddScoped<IOrderServices,OrderServices>(); 
    //builder.Services.AddScoped<ITestServices,TestServices>(); 
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseRouting();
    app.MapControllers();
    app.Run();
}
