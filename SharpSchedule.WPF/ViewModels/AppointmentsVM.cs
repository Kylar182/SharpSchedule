using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

    /// <summary>
    /// Filtered Appiontments
    /// </summary>
    public ObservableCollection<Appointment> Appointments { get; set; } = new ObservableCollection<Appointment>();

    /// <summary>
    /// All Appointments currently in the System
    /// </summary>
    public List<Appointment> AllAppointments { get; set; } = new List<Appointment>();

    public AppointmentsVM(
      IAppointmentRepository repository)
    {
      _repository = repository;
      Appointments = new ObservableCollection<Appointment>();
      Load().ConfigureAwait(true);
    }

    private async Task Load()
    {
      await _repository.GetAll().ContinueWith(t =>
      {
        if (t.Exception == null)
        {
          AllAppointments = t.Result;

          Appointments.Clear();

          foreach (Appointment appointment in AllAppointments)
            Appointments.Add(appointment);
        }
      }).ConfigureAwait(true);
    }
  }
}
