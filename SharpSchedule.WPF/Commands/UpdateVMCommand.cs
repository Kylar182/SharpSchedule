using System;
using System.Windows.Input;
using SharpSchedule.State.Navigators;
using SharpSchedule.ViewModels;

namespace SharpSchedule.Commands
{
  /// <summary>
  /// Command run in Navigator to update 
  /// the Currently selected View Model
  /// </summary>
  public class UpdateVMCommand : ICommand
  {
    public event EventHandler CanExecuteChanged;

    private readonly INavigator _navigator;

    public UpdateVMCommand(INavigator navigator)
    {
      _navigator = navigator;
    }

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      if (parameter is ViewType type)
      {
        switch (type)
        {
          case ViewType.Home:
            _navigator.CurrentVM = new HomeVM();
            break;
          case ViewType.Customers:
            _navigator.CurrentVM = new CustomersVM();
            break;
          case ViewType.Appointments:
            _navigator.CurrentVM = new AppointmentsVM();
            break;
        }
      }
    }
  }
}
