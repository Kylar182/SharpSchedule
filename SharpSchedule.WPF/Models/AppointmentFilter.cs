using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharpSchedule.Data.Validation;

namespace SharpSchedule.Models
{
  public class AppointmentFilter
  {
    /// <summary>
    /// Information about the Appointment
    /// </summary>
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
    [Required]
    public string Type { get; set; }

    /// <summary>
    /// Hotlink to info on the appointment or it's attendees
    /// </summary>
    [MaxLength(255, ErrorMessage = "Max Length 255 Characters")]
    public string URL { get; set; }

    /// <summary>
    /// Start Time of the Appointment
    /// </summary>
    [StartDate(nameof(Start), nameof(End))]
    public DateTime? Start { get; set; }

    /// <summary>
    /// End Time of the Appointment
    /// </summary>
    [EndDate(nameof(End), nameof(Start))]
    public DateTime? End { get; set; }

    /// <summary>
    /// FKey to the Customer that this Appointment is with
    /// </summary>
    public int? CustomerId { get; set; }
  }
}
