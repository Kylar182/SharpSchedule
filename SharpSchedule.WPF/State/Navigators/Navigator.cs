using System.Windows.Input;
using SharpSchedule.Commands;
using SharpSchedule.Models;
using SharpSchedule.ViewModels;
using SharpSchedule.ViewModels.Factories;

namespace SharpSchedule.State.Navigators
{
  public class Navigator : ObservableObject, INavigator
  {
    private ViewModelBase _currentVM;

    public ViewModelBase CurrentVM
    {
      get
      {
        return _currentVM;
      }
      set
      {
        _currentVM = value;
        OnPropChanged(nameof(CurrentVM));
      }
    }

    public ICommand UpdateCurrentVM { get; set; }

    public Navigator(IVMAbstractFactory vmFactory)
    {
      UpdateCurrentVM = new UpdateVMCommand(this, vmFactory);
    }
  }
}
