using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.Data.Repositories;

namespace SharpSchedule.Persistence.Repositories
{
  public class AddressRepository : Repository<Address>, IAddressRepository
  {
    public AddressRepository(DbContextFactory contextFactory) : base(contextFactory)
    {
      _contextFactory = contextFactory;
    }

    public override async Task<List<Address>> GetAll()
    {
      using (SchedulingContext _context = _contextFactory.CreateDbContext())
      {
        return await _context.Addresses.Include(pr => pr.City)
                        .ThenInclude(pr => pr.Country).ToListAsync();
      }
    }
  }
}
