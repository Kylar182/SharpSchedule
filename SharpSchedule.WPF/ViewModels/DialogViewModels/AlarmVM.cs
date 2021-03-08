using System;
using System.Windows.Threading;
using SharpSchedule.Data.DTOs;

namespace SharpSchedule.ViewModels.DialogViewModels
{
  /// <summary>
  /// View Model for City CRUD in the City Dialog
  /// </summary>
  public class AlarmVM : ViewModelBase
  {
    private string title;
    /// <summary>
    /// Information about the Appointment
    /// </summary>
    public string Title
    {
      get => title;
      set
      {
        title = value;
        OnPropChanged(nameof(Title));
        OnPropChanged(nameof(WindowLabel));
      }
    }

    public string WindowLabel => Title + " Reminder";

    private string location;
    /// <summary>
    /// Appointment's Location
    /// </summary>
    public string Location
    {
      get => location;
      set
      {
        location = value;
        OnPropChanged(nameof(Location));
      }
    }

    private string contact;
    /// <summary>
    /// Point of Contact for the Appointment
    /// </summary>
    public string Contact
    {
      get => contact;
      set
      {
        contact = value;
        OnPropChanged(nameof(Contact));
      }
    }

    private string type;
    /// <summary>
    /// Type of the Appointment
    /// </summary>
    public string Type
    {
      get => type;
      set
      {
        type = value;
        OnPropChanged(nameof(Type));
      }
    }

    private bool alarm;
    /// <summary>
    /// Type of the Appointment
    /// </summary>
    public bool AlarmOn
    {
      get => alarm;
      set
      {
        alarm = value;
        OnPropChanged(nameof(AlarmOn));
      }
    }

    /// <summary>
    /// DTO for the Appointment this VM showing an Alarm For
    /// </summary>
    public AppointmentDTO DTO { get; set; }

    public AlarmVM(AppointmentDTO dto)
    {
      DTO = dto;

      AppointmentCheck();

      DispatcherTimer timer = new DispatcherTimer();
      timer.Interval = new TimeSpan(0, 1, 0);
      timer.Tick += (s, e) => AppointmentCheck();
      timer.Start();

      Title = DTO.Title;
      Location = DTO.Location;
      Contact = DTO.Contact;
      Type = DTO.Type;
    }

    private void AppointmentCheck()
    {
      if (DateTime.Now <= DTO.Start)
        AlarmOn = true;
      else
        AlarmOn = false;
    }
  }
}
