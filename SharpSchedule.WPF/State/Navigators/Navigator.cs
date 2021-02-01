using System.Windows.Input;
using SharpSchedule.Commands;
using SharpSchedule.ViewModels;

namespace SharpSchedule.State.Navigators
{
  public class Navigator : INavigator
  {
    public ViewModelBase CurrentVM { get; set; }

    public ICommand UpdateCurrentVM => new UpdateVMCommand(this);
  }
}
