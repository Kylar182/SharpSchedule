using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using SharpSchedule.Data.EntityModels.Scheduling;
using SharpSchedule.Data.Extensions;
using SharpSchedule.Data.Repositories.Scheduling;
using SharpSchedule.ViewModels;
using SharpSchedule.ViewModels.DialogViewModels;
using SharpSchedule.Views.Dialogs;

namespace SharpSchedule.Commands.AppointmentsVMCommands
{
  public class FilterAppointmentsCommand : ICommand
  {
    private readonly AppointmentsVM _appointmentsVM;
    private readonly ICustomerRepository _customerRepository;

    public FilterAppointmentsCommand(AppointmentsVM appointmentsVM,
      ICustomerRepository customerRepository)
    {
      _appointmentsVM = appointmentsVM;
      _customerRepository = customerRepository;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      AppointmentFilterDialog dialog = new AppointmentFilterDialog();
      AppointmentFilterVM VM = new AppointmentFilterVM(_customerRepository,
                                                          new Action(() => dialog.Close()));
      dialog.DataContext = VM;
      bool? result = dialog.ShowDialog();

      if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
      {
        List<Appointment> Filtered = _appointmentsVM.AllAppointments;

        VM.Filter = VM.DBFilter();

        if (!VM.Filter.Title.IsEmpty())
          Filtered = Filtered.Where(pr => pr.Title.Contains(VM.Filter.Title)).ToList();

        if (!VM.Filter.Description.IsEmpty())
          Filtered = Filtered.Where(pr => pr.Description.Contains(VM.Filter.Description)).ToList();

        if (!VM.Filter.Location.IsEmpty())
          Filtered = Filtered.Where(pr => pr.Location.Contains(VM.Filter.Location)).ToList();

        if (!VM.Filter.Contact.IsEmpty())
          Filtered = Filtered.Where(pr => pr.Contact.Contains(VM.Filter.Contact)).ToList();

        if (!VM.Filter.Type.IsEmpty())
          Filtered = Filtered.Where(pr => pr.Type.Contains(VM.Filter.Type)).ToList();

        if (!VM.Filter.URL.IsEmpty())
          Filtered = Filtered.Where(pr => pr.URL.Contains(VM.Filter.URL)).ToList();

        if (VM.Filter.Start.HasValue)
          Filtered = Filtered.Where(pr => pr.Start >= VM.Filter.Start.Value).ToList();

        if (VM.Filter.End.HasValue)
          Filtered = Filtered.Where(pr => pr.End <= VM.Filter.End.Value).ToList();

        if (VM.Filter.CustomerId != null)
          Filtered = Filtered.Where(pr => pr.CustomerId == VM.Filter.CustomerId).ToList();

        _appointmentsVM.Appointments.Clear();

        foreach (Appointment appointment in Filtered)
        {
          appointment.Start = appointment.Start.ToLocalTime();
          appointment.End = appointment.End.ToLocalTime();
          _appointmentsVM.Appointments.Add(appointment);
        }
      }
    }
  }
}
