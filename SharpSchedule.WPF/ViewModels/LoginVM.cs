using System.Globalization;
using System.Linq;
using System.Windows.Input;
using SharpSchedule.Commands;
using SharpSchedule.Data.Validation;
using SharpSchedule.Services.Interfaces;
using SharpSchedule.State.Navigators;
using SharpSchedule.ViewModels.Factories;
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
    [RequiredInfo(ErrorMessage = "Un nom d'utilisateur est requis")]
    [MaxInfo(50, ErrorMessage = "Longueur max 50 caractères")]
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
      GetErrors(nameof(Username)).OfType<string>().First() :
        Info == "en" ? "Input Username" : "Nom d'utilisateur d'entrée";

    /// <summary>
    /// Bool to determine if Username is Valid or not
    /// </summary>
    public bool UsernameValid => !PropHasErrors(nameof(Username));

    private string password;
    /// <summary>
    /// Password of the User
    /// </summary>
    [RequiredInfo(ErrorMessage = "Un mot de passe est requis")]
    [MaxInfo(50, ErrorMessage = "Longueur max 50 caractères")]
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
      GetErrors(nameof(Password)).OfType<string>().First() :
        Info == "en" ? "Input Password" : "Saisir mot de passe";

    /// <summary>
    /// Bool to determine if Password is Valid or not
    /// </summary>
    public bool PasswordValid => !PropHasErrors(nameof(Password));

    private string message;
    /// <summary>
    /// Message return on Login Attempt
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

    /// <summary>
    /// Culture Info
    /// </summary>
    public string Info { get; set; }

    /// <summary>
    /// Command to Attempt User Login
    /// </summary>
    public ICommand LoginCommand { get; }

    private readonly IAuthService _authService;
    private readonly INavigator _navigator;
    private readonly IVMFactory<AppointmentsVM> _appointmentVMFactory;

    public LoginVM
      (IAuthService authService,
      INavigator navigator,
      IVMFactory<AppointmentsVM> appointmentVMFactory)
    {
      _authService = authService;
      _navigator = navigator;
      _appointmentVMFactory = appointmentVMFactory;

      Info = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

      Username = string.Empty;
      Password = string.Empty;
      LoginCommand = new LoginCommand(this, _authService, _navigator, _appointmentVMFactory);

      Message = Info == "en" ? "Login to start Scheduling" : "Connectez-vous pour démarrer la planification";
    }
  }
}
