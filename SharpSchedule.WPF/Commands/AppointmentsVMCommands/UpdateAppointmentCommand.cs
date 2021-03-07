using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SharpSchedule.Data.DTOs;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.EntityModels.Scheduling;
using SharpSchedule.Data.Repositories.Scheduling;
using SharpSchedule.Models;
using SharpSchedule.State;
using SharpSchedule.ViewModels;
using SharpSchedule.ViewModels.DialogViewModels;
using SharpSchedule.Views.Dialogs;

namespace SharpSchedule.Commands.AppointmentsVMCommands
{
  public class UpdateAppointmentCommand : CommandBase
  {
    private readonly AppointmentsVM _appointmentsVM;
    private readonly IAppointmentRepository _repository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IStateManager<AppointmentDTO> _state;
    private readonly User _user;

    public UpdateAppointmentCommand(AppointmentsVM appointmentsVM,
      IAppointmentRepository repository,
      ICustomerRepository customerRepository,
      IStateManager<AppointmentDTO> state,
      User user)
    {
      _appointmentsVM = appointmentsVM;
      _repository = repository;
      _customerRepository = customerRepository;
      _state = state;
      _user = user;

      _appointmentsVM.PropertyChanged += AppointmentChanged;
    }

    public override event EventHandler CanExecuteChanged;

    public override bool CanExecute(object parameter)
    {
      return _appointmentsVM.AppointmentSelected != null;
    }

    protected override async Task ExecuteAsync(object parameter)
    {
      if (_appointmentsVM.AppointmentSelected != null)
      {
        AppointmentDialog dialog = new AppointmentDialog();
        AppointmentVM VM = new AppointmentVM(_repository, _customerRepository, CUD.Update,
                                new Action(() => dialog.Close()), _user, _appointmentsVM.AppointmentSelected);
        dialog.DataContext = VM;
        bool? result = dialog.ShowDialog();

        if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
        {
          List<Appointment> transfer = await _appointmentsVM.GetAll().ConfigureAwait(true);

          _appointmentsVM.Appointments.Clear();

          foreach (Appointment appointment in transfer)
          {
            _appointmentsVM.AllAppointments.Add(new AppointmentDTO(appointment));

            AppointmentDTO dto = new AppointmentDTO(appointment);
            dto.Start = dto.Start.ToLocalTime();
            dto.End = dto.End.ToLocalTime();

            _appointmentsVM.Appointments.Add(dto);
          }

          _state.SetState(_appointmentsVM.Appointments.Where(pr => pr.Start >= DateTime.Now).ToList());
        }
      }
    }

    private void AppointmentChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(_appointmentsVM.AppointmentSelected))
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
  }
}
