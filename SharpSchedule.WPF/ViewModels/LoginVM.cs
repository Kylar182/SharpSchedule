using System.ComponentModel.DataAnnotations;
using SharpSchedule.Services.Interfaces;
using SharpSchedule.ViewModels.Validation;

namespace SharpSchedule.ViewModels
{
  /// <summary>
  /// View Model for User Auth on the Login View
  /// </summary>
  public class LoginVM : ValidationBase
  {
    private string username;
    /// <summary>
    /// Username of the User
    /// </summary>
    [Required(ErrorMessage = "A Username is required")]
    [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
    public string Username
    {
      get => username;
      set
      {
        ValidateProp(value);
        username = value;
        OnPropChanged(nameof(Username));
        OnPropChanged(nameof(UsernameValidity));
      }
    }

    /// <summary>
    /// Bool to determine Username Validity
    /// </summary>
    public bool UsernameValidity => PropertyHasErrors(nameof(Username));

    private string password;
    /// <summary>
    /// Password of the User
    /// </summary>
    [Required(ErrorMessage = "A Password is required")]
    [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
    public string Password
    {
      get => password;
      set
      {
        ValidateProp(value);
        password = value;
        OnPropChanged(nameof(Password));
      }
    }

    /// <summary>
    /// Bool to determine Username Validity
    /// </summary>
    public bool PasswordValidity => PropertyHasErrors(nameof(Password));

    private readonly IAuthService _authService;
    public LoginVM(IAuthService authService)
    {
      _authService = authService;
    }
  }
}
