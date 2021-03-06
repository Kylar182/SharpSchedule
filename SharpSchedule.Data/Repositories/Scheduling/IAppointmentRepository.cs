using System.Collections.Generic;
using System.Threading.Tasks;
using SharpSchedule.Data.EntityModels.Scheduling;

namespace SharpSchedule.Data.Repositories.Scheduling
{
  /// <summary>
  /// Interface for Repository Methods related to Appointments
  /// </summary>
  public interface IAppointmentRepository : IRepository<Appointment>
  {
    /// <summary>
    /// Get a List of Appointments who's Start 
    /// Time is less than the Current Time
    /// </summary>
    /// <returns></returns>
    Task<List<Appointment>> Current();
  }
}
