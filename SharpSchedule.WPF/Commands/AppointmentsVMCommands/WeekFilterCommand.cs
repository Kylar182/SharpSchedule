using System;
using System.Windows.Input;
using SharpSchedule.ViewModels.DialogViewModels;

namespace SharpSchedule.Commands.AppointmentsVMCommands
{
  public class WeekFilterCommand : ICommand
  {
    private readonly AppointmentFilterVM _filterVM;

    public WeekFilterCommand(AppointmentFilterVM filterVM)
    {
      _filterVM = filterVM;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter)
    {
      _filterVM.Start = DateTime.Now.AddDays(-7);
      _filterVM.End = DateTime.Now;
    }
  }
}
