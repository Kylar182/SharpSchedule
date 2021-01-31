using System;
using System.ComponentModel.DataAnnotations;

namespace SharpSchedule.Data.EntityModels.Scheduling
{
  /// <summary>
  /// Appointment that the user Schedules with Customers
  /// </summary>
  public class Appointment : BaseModel
  {
    /// <summary>
    /// Information about the Appointment
    /// </summary>
    [Required]
    [MaxLength(255, ErrorMessage = "Max Length 50 Characters")]
    public string Title { get; set; }

    /// <summary>
    /// Description of the Appointment
    /// </summary>
    /// <remarks>
    /// I would like to reiterate that variable choices
    /// and restrictions (or lack there-of) are specific 
    /// to the course and should not be considered as a
    /// reflections of my skill in database architecture.
    /// </remarks>
    /// [MaxLength(500)]
    public string Description { get; set; }

    /// <summary>
    /// Location information of the Appointment
    /// </summary>
    /// <remarks>
    /// I would like to reiterate that variable choices
    /// and restrictions (or lack there-of) are specific 
    /// to the course and should not be considered as a
    /// reflections of my skill in database architecture.
    /// </remarks>
    /// [MaxLength(150)]
    public string Location { get; set; }

    /// <summary>
    /// Point of Contact for the Appointment
    /// </summary>
    /// <remarks>
    /// I would like to reiterate that variable choices
    /// and restrictions (or lack there-of) are specific 
    /// to the course and should not be considered as a
    /// reflections of my skill in database architecture.
    /// </remarks>
    /// [MaxLength(50)]
    public string Contact { get; set; }

    /// <summary>
    /// Type of the Appointment
    /// </summary>
    /// <remarks>
    /// I would like to reiterate that variable choices
    /// and restrictions (or lack there-of) are specific 
    /// to the course and should not be considered as a
    /// reflections of my skill in database architecture.
    /// </remarks>
    /// [MaxLength(50)]
    public string Type { get; set; }

    /// <summary>
    /// Hotlink to info on the appointment or it's attendees
    /// </summary>
    [MaxLength(255, ErrorMessage = "Max Length 255 Characters")]
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
  }
}
