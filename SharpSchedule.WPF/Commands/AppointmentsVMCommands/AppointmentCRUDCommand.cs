using System;
using System.ComponentModel;
using System.Windows.Input;
using SharpSchedule.ViewModels.DialogViewModels;

namespace SharpSchedule.Commands.AppointmentsVMCommands
{
  public class AppointmentCRUDCommand : ICommand
  {
    private readonly AppointmentVM _appointmentVM;

    public AppointmentCRUDCommand(AppointmentVM appointmentVM)
    {
      _appointmentVM = appointmentVM;

      _appointmentVM.PropertyChanged += ErrorsChanged;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return !_appointmentVM.HasErrors;
    }

    public async void Execute(object parameter)
    {
      if (!_appointmentVM.HasErrors)
      {
        try
        {
          await _appointmentVM.DBUpdate().ConfigureAwait(true);
        }
        catch (Exception)
        {
          return;
        }
      }
    }

    private void ErrorsChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(_appointmentVM.HasErrors))
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
  }
}
