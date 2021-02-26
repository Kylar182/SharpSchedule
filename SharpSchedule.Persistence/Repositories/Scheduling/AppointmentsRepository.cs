using System.Collections.Generic;
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

    public override async Task<List<Appointment>> GetAll()
    {
      using SchedulingContext _context = _contextFactory.CreateDbContext();
      return await _context.Appointments.Include(pr => pr.Customer)
                      .ThenInclude(pr => pr.Address)
                      .ThenInclude(pr => pr.City)
                      .ThenInclude(pr => pr.Country)
                      .Include(pr => pr.User).ToListAsync();
    }
  }
}
