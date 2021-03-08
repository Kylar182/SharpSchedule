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

    private string customerName;
    /// <summary>
    /// Customer's Name that the Appointment is with
    /// </summary>
    public string CustomerName
    {
      get => customerName;
      set
      {
        customerName = value;
        OnPropChanged(nameof(Contact));
      }
    }

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

    /// <summary>
    /// DTO for the Appointment this VM showing an Alarm For
    /// </summary>
    public AppointmentDTO DTO { get; set; }

    public AlarmVM(AppointmentDTO dto)
    {
      DTO = dto;

      Title = DTO.Title;
      CustomerName = "Customer: " + DTO.Customer.Name;
      Location = "Location: " + DTO.Location;
      Contact = "Point of Contact: " + DTO.Contact;
      Type = "Type: " + DTO.Type;
    }
  }
}
