using SharpSchedule.Services.Interfaces;
using SharpSchedule.State.Navigators;

namespace SharpSchedule.ViewModels
{
  /// <summary>
  /// Parent VM for all VMs in the Single Page Application
  /// </summary>
  public class MainVM : ViewModelBase
  {
    public INavigator Navigator { get; set; }
    public IAuthService AuthService { get; }

    public MainVM(INavigator navigator, IAuthService authService)
    {
      Navigator = navigator;
      AuthService = authService;
      Navigator.UpdateCurrentVM.Execute(ViewType.Login);
    }
  }
}
