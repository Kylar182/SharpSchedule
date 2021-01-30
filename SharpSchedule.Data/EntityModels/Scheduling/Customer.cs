using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharpSchedule.Data.EntityModels.Location;

namespace SharpSchedule.Data.EntityModels.Scheduling
{
  /// <summary>
  /// Customer that the user Schedules Appointments with
  /// </summary>
  public class Customer : BaseModel
  {
    /// <summary>
    /// Name of the Customer
    /// </summary>
    [Required]
    [MaxLength(45, ErrorMessage = "Max Length 50 Characters")]
    public string Name { get; set; }

    /// <summary>
    /// Soft delete Boolean
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// Address of this Customer
    /// </summary>
    public Address Address { get; set; }
    /// <summary>
    /// Add of the Address of this Customer
    /// </summary>
    public int AddressId { get; set; }

    /// <summary>
    /// List of Appointments that scheduled with this Customer
    /// </summary>
    public List<Appointment> Appointments { get; set; }
  }
}
