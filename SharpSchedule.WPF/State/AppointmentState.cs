using System.Collections.Generic;
using SharpSchedule.Data.DTOs;

namespace SharpSchedule.State
{
  /// <summary>
  /// Singleton to Hold State of upcoming Appointments
  /// </summary>
  public class AppointmentState : IStateManager<AppointmentDTO>
  {
    public List<AppointmentDTO> Appointments { get; protected set; } = new List<AppointmentDTO>();

    public List<AppointmentDTO> GetState() => Appointments;

    public void SetState(List<AppointmentDTO> state) => Appointments = state;
  }
}
