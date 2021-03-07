using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using SharpSchedule.Commands.AppointmentsVMCommands;
using SharpSchedule.Data.DTOs;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.EntityModels.Scheduling;
using SharpSchedule.Data.Repositories.Scheduling;
using SharpSchedule.State;

namespace SharpSchedule.ViewModels
{
  /// <summary>
  /// View Model for Appointment CRUD Ops on the Appointments View
  /// </summary>
  public class AppointmentsVM : ViewModelBase
  {
    private readonly IAppointmentRepository _repository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IStateManager<AppointmentDTO> _state;
    private readonly User _user;

    private AppointmentDTO appointment;
    [Required(ErrorMessage = "Appointment is Required")]
    public AppointmentDTO AppointmentSelected
    {
      get => appointment;
      set
      {
        appointment = value;
        OnPropChanged(nameof(AppointmentSelected));
      }
    }

    /// <summary>
    /// Filtered Appiontments
    /// </summary>
    public ObservableCollection<AppointmentDTO> Appointments { get; set; } = new ObservableCollection<AppointmentDTO>();

    /// <summary>
    /// All Appointments currently in the System
    /// </summary>
    public List<AppointmentDTO> AllAppointments { get; set; } = new List<AppointmentDTO>();

    /// <summary>
    /// Opens the Filter Appointment Dialog
    /// </summary>
    public ICommand FilterAppointments { get; }
    /// <summary>
    /// Opens the New Appointment Dialog
    /// </summary>
    public ICommand NewAppointment { get; }
    /// <summary>
    /// Opens the Update Appointment Dialog
    /// </summary>
    public ICommand UpdateAppointment { get; }
    /// <summary>
    /// Opens the Delete Appointment Dialog
    /// </summary>
    public ICommand DeleteAppointment { get; }

    public AppointmentsVM(
      IAppointmentRepository repository,
      ICustomerRepository customerRepository,
      IStateManager<AppointmentDTO> state,
      User user)
    {
      _repository = repository;
      _customerRepository = customerRepository;
      _state = state;
      _user = user;

      Load().ConfigureAwait(true);

      AppointmentSelected = null;

      FilterAppointments = new FilterAppointmentsCommand(this, _customerRepository);
      NewAppointment = new NewAppointmentCommand(this, _repository, _customerRepository, _state, _user);
      UpdateAppointment = new UpdateAppointmentCommand(this, _repository, _customerRepository, _state, _user);
      DeleteAppointment = new DeleteAppointmentCommand(this, _repository, _customerRepository, _state, _user);
    }

    /// <summary>
    /// Loads All Appointments and required related data
    /// </summary>
    public async Task Load()
    {
      List<Appointment> transfer = await GetAll().ConfigureAwait(true);

      Appointments.Clear();

      foreach (Appointment appointment in transfer)
      {
        AllAppointments.Add(new AppointmentDTO(appointment));

        AppointmentDTO dto = new AppointmentDTO(appointment);
        dto.Start = dto.Start.ToLocalTime();
        dto.End = dto.End.ToLocalTime();

        Appointments.Add(dto);
      }

      _state.SetState(Appointments.Where(pr => pr.Start >= DateTime.Now).ToList());
    }

    public async Task<List<Appointment>> GetAll()
    {
      List<Appointment> transfer = new List<Appointment>();

      await _repository.GetAll().ContinueWith(t =>
      {
        if (t.Exception == null)
        {
          transfer = t.Result;
        }
      }).ConfigureAwait(true);

      return transfer;
    }
  }
}
