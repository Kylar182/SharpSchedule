using SharpSchedule.Services.Interfaces;

namespace SharpSchedule.ViewModels.Factories
{
  public class LoginVMFactory : IVMFactory<LoginVM>
  {
    private readonly IAuthService _authService;

    public LoginVMFactory(IAuthService authService)
    {
      _authService = authService;
    }

    public LoginVM CreateVM()
    {
      return new LoginVM(_authService);
    }
  }
}
