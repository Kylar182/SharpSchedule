using System;
using System.Threading.Tasks;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.DTOs;
using SharpSchedule.Services.Interfaces;
using SharpSchedule.Data.Repositories;
using SharpSchedule.Persistence;
using SharpSchedule.Persistence.Repositories;

namespace SharpSchedule.Services
{
  public class AuthService : IAuthService
  {
    /// <summary>
    /// Since Auth Service needs to be a Singleton I 
    /// inject the Context Factory and create a new 
    /// Repository as needed using the injected Factory
    /// </summary>
    private readonly DbContextFactory _contextFactory;

    public AuthService(DbContextFactory contextFactory)
    {
      _contextFactory = contextFactory;
    }

    private User current { get; set; }

    public async Task<bool?> Login(LoginDTO dto)
    {
      try
      {
        using (IUserRepository _repository =  new UserRepository(_contextFactory))
        {
          User user = await _repository.Login(dto);
          if (user != null)
          {
            current = user;
            return true;
          }
          else
          {
            return false;
          }
        }
      }
      catch (Exception)
      {
        return null;
      }
    }

    public User GetCurrent() => current;

    public void Logout()
    {
      current = null;
    }
  }
}
