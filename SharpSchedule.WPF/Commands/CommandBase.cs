using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharpSchedule.Commands
{
  public abstract class CommandBase : ICommand
  {
    public virtual event EventHandler CanExecuteChanged;

    public virtual bool CanExecute(object parameter)
    {
      return true;
    }

    public async void Execute(object parameter)
    {
      await ExecuteAsync(parameter);
    }

    protected abstract Task ExecuteAsync(object parameter);
  }
}
