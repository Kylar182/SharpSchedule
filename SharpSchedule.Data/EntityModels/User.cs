using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharpSchedule.Data.EntityModels.Scheduling;

namespace SharpSchedule.Data.EntityModels
{
  /// <summary>
  /// Person that Logs in and Makes Appointments
  /// </summary>
  public class User : BaseModel
  {
    /// <summary>
    /// Username of the User, used for Login, must be Unique
    /// </summary>
    [Column("userName")]
    [Required]
    [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
    public string Username { get; set; }

    /// <summary>
    /// Password of the User, used for Login, must be Unique
    /// </summary>
    /// <remarks>
    /// I realize saving Passwords with no encryption directly in a 
    /// database is asinine but again, this is a specific course requirement
    /// </remarks>
    [Column("password")]
    [Required]
    [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
    public string Password { get; set; }

    /// <summary>
    /// Soft delete Boolean
    /// </summary>
    [Column("active")]
    public bool Active { get; set; }

    /// <summary>
    /// List of Appointments for this User
    /// </summary>
    public List<Appointment> Appointments { get; set; }
  }
}
