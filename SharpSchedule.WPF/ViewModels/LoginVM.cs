using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Input;
using SharpSchedule.Commands;
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
        OnPropChanged(nameof(UsernameText));
        OnPropChanged(nameof(UsernameValid));
      }
    }

    /// <summary>
    /// Helpertext for the user's Username
    /// </summary>
    /// <remarks>
    /// If Username is invalid, returns the first Error message, 
    /// otherwise it returns the designated Helpertext
    /// </remarks>
    public string UsernameText => PropHasErrors(nameof(Username)) ?
      GetErrors(nameof(Username)).OfType<string>().First() : "Input Username";

    /// <summary>
    /// Bool to determine if Username is Valid or not
    /// </summary>
    public bool UsernameValid => !PropHasErrors(nameof(Username));

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
        OnPropChanged(nameof(PasswordText));
        OnPropChanged(nameof(PasswordValid));
      }
    }

    /// <summary>
    /// Helpertext for the user's Password
    /// </summary>
    /// <remarks>
    /// If Password is invalid, returns the first Error message, 
    /// otherwise it returns the designated Helpertext
    /// </remarks>
    public string PasswordText => PropHasErrors(nameof(Password)) ?
      GetErrors(nameof(Password)).OfType<string>().First() : "Input Password";

    /// <summary>
    /// Bool to determine if Password is Valid or not
    /// </summary>
    public bool PasswordValid => !PropHasErrors(nameof(Password));

    private string message;
    /// <summary>
    /// Password of the User
    /// </summary>
    public string Message
    {
      get => message;
      set
      {
        message = value;
        OnPropChanged(nameof(Message));
      }
    }

    public ICommand LoginCommand { get; }

    private readonly IAuthService _authService;
    public LoginVM(IAuthService authService)
    {
      _authService = authService;
      LoginCommand = new LoginCommand(this, _authService);
    }
  }
}
