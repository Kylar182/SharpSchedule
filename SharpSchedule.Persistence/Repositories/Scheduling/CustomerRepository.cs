using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharpSchedule.Data.EntityModels.Scheduling;
using SharpSchedule.Data.Repositories.Scheduling;

namespace SharpSchedule.Persistence.Repositories.Scheduling
{
  public class CustomerRepository : Repository<Customer>, ICustomerRepository
  {
    public CustomerRepository(DbContextFactory contextFactory) : base(contextFactory)
    {
      _contextFactory = contextFactory;
    }

    public override async Task<List<Customer>> GetAll()
    {
      using SchedulingContext _context = _contextFactory.CreateDbContext();
      return await _context.Customers.Include(pr => pr.Address)
                                      .ThenInclude(pr => pr.City)
                                        .ThenInclude(pr => pr.Country).ToListAsync();
    }
  }
}
