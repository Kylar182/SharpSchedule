using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.Data.Repositories;

namespace SharpSchedule.Persistence.Repositories.Location
{
  public class CityRepository : Repository<City>, ICityRepository
  {
    public CityRepository(DbContextFactory contextFactory) : base(contextFactory)
    {
      _contextFactory = contextFactory;
    }

    public override async Task<List<City>> GetAll()
    {
      using (SchedulingContext _context = _contextFactory.CreateDbContext())
      {
        return await _context.Cities.Include(pr => pr.Country).ToListAsync();
      }
    }
  }
}
