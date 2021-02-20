using SharpSchedule.Services.Interfaces;
using SharpSchedule.State.Navigators;

namespace SharpSchedule.ViewModels.Factories
{
  public class LoginVMFactory : IVMFactory<LoginVM>
  {
    private readonly IAuthService _authService;
    private readonly INavigator _navigator;
    private readonly IVMFactory<HomeVM> _homeFactory;

    public LoginVMFactory(IAuthService authService, INavigator navigator, IVMFactory<HomeVM> homeFactory)
    {
      _authService = authService;
      _navigator = navigator;
      _homeFactory = homeFactory;
    }

    public LoginVM CreateVM()
    {
      return new LoginVM(_authService, _navigator, _homeFactory);
    }
  }
}
