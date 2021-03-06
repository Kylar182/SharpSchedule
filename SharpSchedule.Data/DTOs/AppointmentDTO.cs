using System;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.EntityModels.Scheduling;

namespace SharpSchedule.Data.DTOs
{
  /// <summary>
  /// DTO for an Appointment that a user Schedules with Customers
  /// </summary>
  public class AppointmentDTO : BaseModel
  {
    /// <summary>
    /// Database Unique Identifier - System Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Information about the Appointment
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Description of the Appointment
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Location information of the Appointment
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// Point of Contact for the Appointment
    /// </summary>
    public string Contact { get; set; }

    /// <summary>
    /// Type of the Appointment
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Hotlink to info on the appointment or it's attendees
    /// </summary>
    public string URL { get; set; }

    /// <summary>
    /// Start Time of the Appointment
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// End Time of the Appointment
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// Customer that this Appointment is with
    /// </summary>
    public Customer Customer { get; set; }
    /// <summary>
    /// FKey to the Customer that this Appointment is with
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// User that made the Appointment with the Customer
    /// </summary>
    public User User { get; set; }
    /// <summary>
    /// FKey to the User that made 
    /// the Appointment with the Customer
    /// </summary>
    public int UserId { get; set; }

    public AppointmentDTO() { }

    public AppointmentDTO(Appointment appointment)
    {
      CreatedBy = appointment.CreatedBy;
      CreateDate = appointment.CreateDate;
      LastUpdatedBy = appointment.LastUpdatedBy;
      LastUpdate = appointment.LastUpdate;
      Id = appointment.Id;
      Title = appointment.Title;
      Description = appointment.Description;
      Location = appointment.Location;
      Contact = appointment.Contact;
      Type = appointment.Type;
      URL = appointment.URL;
      Start = appointment.Start;
      End = appointment.End;
      Customer = appointment.Customer;
      CustomerId = appointment.CustomerId;
      User = appointment.User;
      UserId = appointment.UserId;
    }

    public Appointment ToAppointment()
    {
      return new Appointment()
      {
        CreatedBy = CreatedBy,
        CreateDate = CreateDate,
        LastUpdatedBy = LastUpdatedBy,
        LastUpdate = LastUpdate,
        Id = Id,
        Title = Title,
        Description = Description,
        Location = Location,
        Contact = Contact,
        Type = Type,
        URL = URL,
        Start = Start,
        End = End,
        CustomerId = CustomerId,
        UserId = UserId
      };
    }
  }
}
