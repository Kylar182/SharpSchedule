using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SharpSchedule.Data.Extensions;

namespace SharpSchedule.Persistence
{
  public class DbContextFactory : IDesignTimeDbContextFactory<SchedulingContext>
  {
    public SchedulingContext CreateDbContext(string[] args = null)
    {
      DbContextOptionsBuilder options = new DbContextOptionsBuilder<SchedulingContext>();
      string conn = Environment.GetEnvironmentVariable("CONN");
      if (conn.IsEmpty())
        conn = "server=wgudb.ucertify.com;port=3306;database=U06aOV;user=U06aOV;password=53688711604";
      options.UseMySQL(conn);

      return new SchedulingContext(options.Options);
    }
  }
}
