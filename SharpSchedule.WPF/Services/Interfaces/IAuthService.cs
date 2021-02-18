using System.Threading.Tasks;
using SharpSchedule.Data.DTOs;
using SharpSchedule.Data.EntityModels;

namespace SharpSchedule.Services.Interfaces
{
  /// <summary>
  /// Service for Managing Auth and the Current User
  /// </summary>
  public interface IAuthService
  {
    /// <summary>
    /// Attempts to Log a User into the System via a DTO
    /// </summary>
    /// <returns>
    /// True if successful, False if user not found, Null if database unreachable
    /// </returns>
    public Task<bool?> Login(LoginDTO dto);

    /// <summary>
    /// Gets the Currently Logged in User from Memory
    /// </summary>
    /// <remarks>
    /// Auth Service is a Singleton to allow only 
    /// one user per instance of the application
    /// </remarks>
    public User GetCurrent();

    /// <summary>
    /// Sets Current User as Null 
    /// and redirects to Login Screen
    /// </summary>
    public void Logout();
  }
}
