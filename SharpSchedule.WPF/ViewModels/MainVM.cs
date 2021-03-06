using System.Windows.Input;
using SharpSchedule.Commands;
using SharpSchedule.Services.Interfaces;
using SharpSchedule.State.Navigators;
using SharpSchedule.ViewModels.Factories;

namespace SharpSchedule.ViewModels
{
  /// <summary>
  /// Parent VM for all VMs in the Single Page Application
  /// </summary>
  public class MainVM : ViewModelBase
  {
    public INavigator Navigator { get; set; }
    public IAuthService AuthService { get; }
    public ICommand UpdateCurrentVM { get; }
    public ICommand LogoutCommand { get; }

    public MainVM(INavigator navigator,
      IAuthService authService,
      IRootVMFactory vmFactory)
    {
      Navigator = navigator;
      AuthService = authService;

      UpdateCurrentVM = new UpdateVMCommand(navigator, vmFactory);
      LogoutCommand = new LogoutCommand(authService, navigator, vmFactory);

      UpdateCurrentVM.Execute(ViewType.Login);
    }
  }
}
