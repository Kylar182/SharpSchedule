using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharpSchedule.Data.EntityModels.Scheduling;
using SharpSchedule.Data.Repositories.Scheduling;

namespace SharpSchedule.Persistence.Repositories.Scheduling
{
  public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
  {
    public AppointmentRepository(DbContextFactory contextFactory) : base(contextFactory)
    {
      _contextFactory = contextFactory;
    }

    public async Task<List<Appointment>> Current()
    {
      using SchedulingContext _context = _contextFactory.CreateDbContext();
      return await _context.Appointments.Where(pr => pr.Start >= DateTime.UtcNow)
                                          .Include(pr => pr.Customer)
                                          .Include(pr => pr.User).ToListAsync();
    }

    public override async Task<List<Appointment>> GetAll()
    {
      using SchedulingContext _context = _contextFactory.CreateDbContext();
      return await _context.Appointments.Include(pr => pr.Customer)
                                        .Include(pr => pr.User).ToListAsync();
    }
  }
}
