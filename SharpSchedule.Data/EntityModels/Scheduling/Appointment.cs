using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharpSchedule.Data.Validation;

namespace SharpSchedule.Data.EntityModels.Scheduling
{
  /// <summary>
  /// Appointment that the user Schedules with Customers
  /// </summary>
  [Table("appointment")]
  public class Appointment : BaseModel
  {
    /// <summary>
    /// Database Unique Identifier - System Id
    /// </summary>
    [Key]
    [Column("appointmentId")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Information about the Appointment
    /// </summary>
    [Column("title")]
    [Required]
    [MaxLength(255, ErrorMessage = "Max Length 255 Characters")]
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
    [Column("description")]
    [Required]
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
    [Column("location")]
    [Required]
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
    [Column("contact")]
    [Required]
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
    [Column("type")]
    [Required]
    public string Type { get; set; }

    /// <summary>
    /// Hotlink to info on the appointment or it's attendees
    /// </summary>
    [Column("url")]
    [Required]
    [MaxLength(255, ErrorMessage = "Max Length 255 Characters")]
    public string URL { get; set; }

    /// <summary>
    /// Start Time of the Appointment
    /// </summary>
    [Column("start")]
    [StartDate(nameof(Start), nameof(End))]
    public DateTime Start { get; set; }

    /// <summary>
    /// End Time of the Appointment
    /// </summary>
    [Column("end")]
    [EndDate(nameof(End), nameof(Start))]
    public DateTime End { get; set; }

    /// <summary>
    /// Customer that this Appointment is with
    /// </summary>
    public Customer Customer { get; set; }
    /// <summary>
    /// FKey to the Customer that this Appointment is with
    /// </summary>
    [Column("customerId")]
    public int CustomerId { get; set; }

    /// <summary>
    /// User that made the Appointment with the Customer
    /// </summary>
    public User User { get; set; }
    /// <summary>
    /// FKey to the User that made 
    /// the Appointment with the Customer
    /// </summary>
    [Column("userId")]
    public int UserId { get; set; }
  }
}
