using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

    /// <summary>
    /// Applys all the configurations to setup all tables, columns, indexes, etc.
    /// </summary>
    /// <param name="builder">builder used to apply configurations</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      #region Set the UTC flag on dates
      var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
          v => v,
          v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

      var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
          v => v,
          v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

      foreach (var entityType in builder.Model.GetEntityTypes())
      {
        foreach (var property in entityType.GetProperties())
        {
          if (property.ClrType == typeof(DateTime))
          {
            property.SetValueConverter(dateTimeConverter);
          }
          else if (property.ClrType == typeof(DateTime?))
          {
            property.SetValueConverter(nullableDateTimeConverter);
          }
        }
      }
      #endregion
    }

    #region Set the DateTimes on Save
    /// <summary>
    /// Override to allow the add of Audit fields
    /// </summary>
    public override int SaveChanges()
    {
      AddAuditInfo();
      return base.SaveChanges();
    }

    /// <summary>
    /// Override to allow the add of Audit field when do async saves
    /// </summary>
    public async Task SaveChangesAsync()
    {
      AddAuditInfo();
      await base.SaveChangesAsync();
    }

    /// <summary>
    /// Sets the FirstUploadedAt and LastUploadedAt of BaseModel Inheritence
    /// </summary>
    private void AddAuditInfo()
    {
      var entries = ChangeTracker.Entries().Where(x => (x.Entity is BaseModel)
                                                    && (x.State == EntityState.Added));

      foreach (var entry in entries)
        ((BaseModel)entry.Entity).CreateDate = DateTime.UtcNow;
    }
    #endregion
  }
}
