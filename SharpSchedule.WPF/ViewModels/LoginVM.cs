using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using SharpSchedule.Commands;
using SharpSchedule.Services.Interfaces;
using SharpSchedule.State.Navigators;
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
        OnPropChanged(nameof(UsernameValid));
      }
    }

    /// <summary>
    /// Bool to determine Username Validity
    /// </summary>
    public bool UsernameValid => PropertyHasErrors(nameof(Username));

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
        OnPropChanged(nameof(PasswordValid));
      }
    }

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

    /// <summary>
    /// Bool to determine Username Validity
    /// </summary>
    public bool PasswordValid => PropertyHasErrors(nameof(Password));

    private readonly IAuthService _authService;
    private readonly INavigator _navigator;
    public LoginVM(IAuthService authService, INavigator navigator)
    {
      _authService = authService;
      _navigator = navigator;
      LoginCommand = new LoginCommand(this, _authService, _navigator);
    }
  }
}
