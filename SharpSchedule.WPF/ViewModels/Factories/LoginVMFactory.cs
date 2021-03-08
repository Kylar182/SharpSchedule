using SharpSchedule.Services.Interfaces;
using SharpSchedule.State.Navigators;

namespace SharpSchedule.ViewModels.Factories
{
  public class LoginVMFactory : IVMFactory<LoginVM>
  {
    private readonly IAuthService _authService;
    private readonly INavigator _navigator;
    private readonly IVMFactory<AppointmentsVM> _appointmentVMFactory;

    public LoginVMFactory(
      IAuthService authService,
      INavigator navigator,
      IVMFactory<AppointmentsVM> appointmentVMFactory)
    {
      _authService = authService;
      _navigator = navigator;
      _appointmentVMFactory = appointmentVMFactory;
    }

    public LoginVM CreateVM()
    {
      return new LoginVM(_authService, _navigator, _appointmentVMFactory);
    }
  }
}
