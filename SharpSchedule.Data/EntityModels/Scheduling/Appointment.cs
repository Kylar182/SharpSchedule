using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharpSchedule.Data.EntityModels.Scheduling
{
  /// <summary>
  /// Appointment that the user Schedules with Customers
  /// </summary>
  public class Appointment : BaseModel
  {
    /// <summary>
    /// Information about this Appointment
    /// </summary>
    [Required]
    [MaxLength(255, ErrorMessage = "Max Length 50 Characters")]
    public string Title { get; set; }
  }
}
