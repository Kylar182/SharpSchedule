using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharpSchedule.Data.DTOs;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.Repositories;

namespace SharpSchedule.Persistence.Repositories
{
  public class UserRepository : Repository<User>, IUserRepository
  {
    public UserRepository(DbContextFactory contextFactory) : base(contextFactory)
    {
      _contextFactory = contextFactory;
    }

    public async Task<User> Login(LoginDTO dto)
    {
      using (SchedulingContext _context = _contextFactory.CreateDbContext())
      {
        return await _context.Users.Where(q => q.Username == dto.Username 
                                && q.Password == dto.Password).FirstOrDefaultAsync();
      }
    }

    public void Dispose() => GC.SuppressFinalize(this);
  }
}
