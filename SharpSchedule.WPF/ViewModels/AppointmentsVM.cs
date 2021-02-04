using System.Collections.ObjectModel;
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
    /// All appiontments currently in the System
    /// </summary>
    public ObservableCollection<Appointment> Appointments { get; set; }

    public AppointmentsVM(
      IAppointmentRepository repository)
    {
      _repository = repository;
      Appointments = new ObservableCollection<Appointment>();
      Load();
    }

    private void Load()
    {
      _repository.GetAll().ContinueWith(a =>
      {
        if (a.Exception == null)
        {
          Appointments.Clear();
          foreach (Appointment appointment in a.Result)
            Appointments.Add(appointment);
        }
      });
    }
  }
}
