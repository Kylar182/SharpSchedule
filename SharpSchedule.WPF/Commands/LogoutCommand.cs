using System;
using System.Windows.Input;
using SharpSchedule.Services.Interfaces;
using SharpSchedule.State.Navigators;
using SharpSchedule.ViewModels.Factories;

namespace SharpSchedule.Commands
{
  public class LogoutCommand : ICommand
  {
    private readonly IAuthService _authService;
    private readonly INavigator _navigator; 
    private readonly IRootVMFactory _vmFactory;

    public LogoutCommand(
      IAuthService authService,
      INavigator navigator,
      IRootVMFactory vmFactory)
    { 
      _authService = authService;
      _navigator = navigator;
      _vmFactory = vmFactory;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      if (_authService.IsLoggedIn)
      {
        _authService.Logout();
        _navigator.CurrentVM = _vmFactory.CreateVM(ViewType.Login);
      }
    }
  }
}
