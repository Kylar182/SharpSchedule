using System;
using System.Threading.Tasks;
using SharpSchedule.Data.DTOs;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.Repositories;
using SharpSchedule.Models;
using SharpSchedule.Persistence;
using SharpSchedule.Persistence.Repositories;
using SharpSchedule.Services.Interfaces;

namespace SharpSchedule.Services
{
  public class AuthService : ObservableObject, IAuthService
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

    public async Task<bool?> Login(LoginDTO dto)
    {
      try
      {
        using (IUserRepository _repository = new UserRepository(_contextFactory))
        {
          User user = await _repository.Login(dto);
          if (user != null)
          {
            Current = user;
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

    private User _current;
    public User Current
    {
      get => _current;
      private set
      {
        _current = value;
        OnPropChanged(nameof(Current));
        OnPropChanged(nameof(IsLoggedIn));
      }
    }

    public User GetCurrent() => Current;

    public void Logout()
    {
      Current = null;
    }

    public bool IsLoggedIn => Current != null;
  }
}
