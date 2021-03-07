using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharpSchedule.Data.DTOs;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.EntityModels.Scheduling;
using SharpSchedule.Data.Extensions;
using SharpSchedule.Data.Repositories;
using SharpSchedule.Data.Repositories.Scheduling;
using SharpSchedule.ViewModels;
using SharpSchedule.ViewModels.DialogViewModels;
using SharpSchedule.Views.Dialogs;

namespace SharpSchedule.Commands.AppointmentsVMCommands
{
  public class FilterAppointmentsCommand : CommandBase
  {
    private readonly AppointmentsVM _appointmentsVM;
    private readonly IUserRepository _userRepository;
    private readonly ICustomerRepository _customerRepository;

    public FilterAppointmentsCommand(
      AppointmentsVM appointmentsVM,
      IUserRepository userRepository,
      ICustomerRepository customerRepository)
    {
      _appointmentsVM = appointmentsVM;
      _userRepository = userRepository;
      _customerRepository = customerRepository;
    }

    protected override async Task ExecuteAsync(object parameter)
    {
      AppointmentFilterDialog dialog = new AppointmentFilterDialog();

      List<User> users = await _userRepository.GetAll();
      List<Customer> customers = await _customerRepository.GetAll();

      AppointmentFilterVM VM = new AppointmentFilterVM(users, customers,
                                                          new Action(() => dialog.Close()));
      dialog.DataContext = VM;
      bool? result = dialog.ShowDialog();

      if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
      {
        List<AppointmentDTO> Filtered = _appointmentsVM.AllAppointments;

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
          Filtered = Filtered.Where(pr => pr.Start >= VM.Filter.Start.Value.ToUniversalTime()).ToList();

        if (VM.Filter.End.HasValue)
          Filtered = Filtered.Where(pr => pr.End <= VM.Filter.End.Value.ToUniversalTime()).ToList();

        if (VM.Filter.CustomerId != null)
          Filtered = Filtered.Where(pr => pr.CustomerId == VM.Filter.CustomerId).ToList();

        if (VM.Filter.UserId != null)
          Filtered = Filtered.Where(pr => pr.UserId == VM.Filter.UserId).ToList();

        _appointmentsVM.Appointments.Clear();

        foreach (AppointmentDTO dto in Filtered)
        {
          AppointmentDTO transfer = dto;
          transfer.Start = transfer.Start.ToLocalTime();
          transfer.End = transfer.End.ToLocalTime();
          _appointmentsVM.Appointments.Add(transfer);
        }
      }
    }
  }
}
