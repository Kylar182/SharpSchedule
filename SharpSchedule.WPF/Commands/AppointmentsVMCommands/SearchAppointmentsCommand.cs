using System;
using System.Linq;
using System.Windows.Input;
using SharpSchedule.Data.DTOs;
using SharpSchedule.ViewModels;

namespace SharpSchedule.Commands.AppointmentsVMCommands
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

        foreach (AppointmentDTO dto in _appointmentVM.AllAppointments.Where(pr => pr.Title.Contains(name)))
        {
          AppointmentDTO transfer = dto;
          transfer.Start = transfer.Start.ToLocalTime();
          transfer.End = transfer.End.ToLocalTime();
          _appointmentVM.Appointments.Add(transfer);
        }
      }
    }
  }
}
