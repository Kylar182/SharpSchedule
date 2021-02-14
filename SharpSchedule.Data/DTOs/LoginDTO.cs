using System.ComponentModel.DataAnnotations;

namespace SharpSchedule.Data.DTOs
{
  /// <summary>
  /// Login DTO for user
  /// </summary>
  public class LoginDTO
  {    /// <summary>
       /// Username of the User, used for Login, must be Unique
       /// </summary>
    [Required]
    [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
    public string Username { get; set; }

    /// <summary>
    /// Password of the User, used for Login, must be Unique
    /// </summary>
    [Required]
    [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
    public string Password { get; set; }

    public LoginDTO(string username, string password)
    {
      Username = username;
      Password = password;
    }
  }
}
