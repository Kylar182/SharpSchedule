using Microsoft.EntityFrameworkCore;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.Data.EntityModels.Scheduling;

namespace SharpSchedule.Persistence
{
  /// <summary>
  /// Database Context for EF Models
  /// </summary>
  public class SchedulingContext : DbContext
  {
    /// <summary>
    /// DbSet / Table for Addresses
    /// </summary>
    public DbSet<Address> Addresses { get; set; }

    /// <summary>
    /// DbSet / Table for Cities
    /// </summary>
    public DbSet<City> Cities { get; set; }

    /// <summary>
    /// DbSet / Table for Countries
    /// </summary>
    public DbSet<Country> Countries { get; set; }

    /// <summary>
    /// DbSet / Table for Appointments
    /// </summary>
    public DbSet<Appointment> Appointments { get; set; }

    /// <summary>
    /// DbSet / Table for Customers
    /// </summary>
    public DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// DbSet / Table for Users
    /// </summary>
    public DbSet<User> Users { get; set; }

    public SchedulingContext(DbContextOptions options) : base(options) { }
  }
}
