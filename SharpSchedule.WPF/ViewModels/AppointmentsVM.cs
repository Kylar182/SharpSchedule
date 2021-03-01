using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows.Input;
using SharpSchedule.Commands.CustomersVMCommands;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.EntityModels.Scheduling;
using SharpSchedule.Data.Repositories.Scheduling;

namespace SharpSchedule.ViewModels
{
  /// <summary>
  /// View Model for Appointment CRUD Ops on the Appointments View
  /// </summary>
  public class AppointmentsVM : ViewModelBase
  {
    private readonly IAppointmentRepository _repository;
    private readonly ICustomerRepository _customerRepository;
    private readonly User _user;

    private Appointment appointment;
    [Required(ErrorMessage = "Appointment is Required")]
    public Appointment AppointmentSelected
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
    public ObservableCollection<Appointment> Appointments { get; set; } = new ObservableCollection<Appointment>();

    /// <summary>
    /// All Appointments currently in the System
    /// </summary>
    public List<Appointment> AllAppointments { get; set; } = new List<Appointment>();

    public ICommand SearchAppointments { get; }

    public ICommand NewAppointment { get; }

    public ICommand UpdateAppointment { get; }

    public ICommand DeleteAppointment { get; }

    public AppointmentsVM(
      IAppointmentRepository repository, ICustomerRepository customerRepository, User user)
    {
      _repository = repository;
      _customerRepository = customerRepository;
      _user = user;

      Appointments = new ObservableCollection<Appointment>();
      Load().ConfigureAwait(true);

      AppointmentSelected = null;

      //SearchAppointments = new SearchAppointmentsCommand(this);
      NewAppointment = new NewAppointmentCommand(this, _repository, _customerRepository, _user);
      //UpdateAppointment = new UpdateAppointmentCommand(this, _repository, _customerRepository, _user);
      //DeleteAppointment = new DeleteAppointmentCommand(this, _repository, _customerRepository, _user);
    }

    public async Task Load()
    {
      await _repository.GetAll().ContinueWith(t =>
      {
        if (t.Exception == null)
        {
          AllAppointments = t.Result;

          Appointments.Clear();

          foreach (Appointment appointment in AllAppointments)
          {
            appointment.Start = appointment.Start.ToLocalTime();
            appointment.End = appointment.End.ToLocalTime();
            Appointments.Add(appointment);
          }
        }
      }).ConfigureAwait(true);
    }
  }
}
