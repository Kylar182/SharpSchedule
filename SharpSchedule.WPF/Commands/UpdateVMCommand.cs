using System;
using System.Windows.Input;
using SharpSchedule.State.Navigators;
using SharpSchedule.ViewModels.Factories;

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
    private readonly IVMAbstractFactory _vmFactory;

    public UpdateVMCommand(INavigator navigator, IVMAbstractFactory vmFactory)
    {
      _navigator = navigator;
      _vmFactory = vmFactory;
    }

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      if (parameter is ViewType type)
      {
        _navigator.CurrentVM = _vmFactory.CreateVM(type);
      }
    }
  }
}
