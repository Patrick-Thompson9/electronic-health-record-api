using ehrApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ehrApi.Data;

public class EhrApiContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Test> Tests { get; set; }

    public EhrApiContext(DbContextOptions<EhrApiContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>()
            .HasMany(patient => patient.Orders)
            .WithOne(order => order.Patient)
            .HasForeignKey(o => o.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .HasOne(order => order.Test)
            .WithOne(test => test.Order)
            .HasForeignKey<Test>(test => test.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}