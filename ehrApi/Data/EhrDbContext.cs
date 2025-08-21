using ehrApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ehrApi.Data;

public class EhrApiContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Test> Tests { get; set; }

    public EhrApiContext(DbContextOptions<EhrApiContext> options) : base(options)
    {
    }
}