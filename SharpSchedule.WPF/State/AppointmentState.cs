using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using SharpSchedule.Commands;
using SharpSchedule.Data.DTOs;

namespace SharpSchedule.State
{
  /// <summary>
  /// Singleton to Hold State of upcoming Appointments
  /// </summary>
  public class AppointmentState : IStateManager<AppointmentDTO>
  {
    public AppointmentState()
    {
      DispatcherTimer timer = new DispatcherTimer();
      timer.Interval = new TimeSpan(0, 1, 0);
      timer.Tick += (s, e) => AppointmentCheck();
      timer.Start();
    }

    public List<AppointmentDTO> Appointments { get; protected set; } = new List<AppointmentDTO>();

    public List<AppointmentDTO> GetState() => Appointments;

    public void SetState(List<AppointmentDTO> state) => Appointments = state;

    private void AppointmentCheck()
    {
      if (Appointments != null && Appointments.Any())
      {
        AppointmentDTO dto = Appointments.Where
                  (pr => pr.Start.AddMinutes(-15).Minute == DateTime.Now.Minute).FirstOrDefault();
        if (dto != null)
        {
          ShowAlarmCommand command = new ShowAlarmCommand(dto);
          command.Execute(string.Empty);
        }
      }
    }
  }
}
