using System;
using System.Threading.Tasks;
using SharpSchedule.Data.DTOs;
using SharpSchedule.Data.EntityModels;

namespace SharpSchedule.Data.Repositories
{
  /// <summary>
  /// Interface for Repository Methods related 
  /// to Users as well as Login attempts
  /// </summary>
  public interface IUserRepository : IRepository<User>, IDisposable
  {
    /// <summary>
    /// Attempts to find a User in by matching 
    /// the DTO information to a user
    /// </summary>
    public Task<User> Login(LoginDTO dto);
  }
}
