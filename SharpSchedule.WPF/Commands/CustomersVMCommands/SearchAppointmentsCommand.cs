using System;
using System.Linq;
using System.Windows.Input;
using SharpSchedule.Data.EntityModels.Scheduling;
using SharpSchedule.ViewModels;

namespace SharpSchedule.Commands.CustomersVMCommands
{
  public class SearchAppointmentsCommand : ICommand
  {
    private readonly AppointmentsVM _appointmentVM;

    public SearchAppointmentsCommand(AppointmentsVM appointmentVM)
    {
      _appointmentVM = appointmentVM;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter)
    {
      if (parameter is string name)
      {
        _appointmentVM.Appointments.Clear();

        foreach (Appointment appointment in _appointmentVM.AllAppointments.Where(pr => pr.Title.Contains(name)))
        {
          appointment.Start = appointment.Start.ToLocalTime();
          appointment.End = appointment.End.ToLocalTime();
          _appointmentVM.Appointments.Add(appointment);
        }
      }
    }
  }
}
