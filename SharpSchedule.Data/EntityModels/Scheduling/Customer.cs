using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharpSchedule.Data.EntityModels.Locations;

namespace SharpSchedule.Data.EntityModels.Scheduling
{
  /// <summary>
  /// Customer that the user Schedules Appointments with
  /// </summary>
  [Table("customer")]
  public class Customer : BaseModel
  {
    /// <summary>
    /// Database Unique Identifier - System Id
    /// </summary>
    [Key]
    [Column("customerId")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Name of the Customer
    /// </summary>
    [Column("customer")]
    [Required]
    [MaxLength(45, ErrorMessage = "Max Length 50 Characters")]
    public string Name { get; set; }

    /// <summary>
    /// Soft delete Boolean
    /// </summary>
    [Column("active")]
    public bool Active { get; set; }

    /// <summary>
    /// Address of this Customer
    /// </summary>
    public Address Address { get; set; }
    /// <summary>
    /// Add of the Address of this Customer
    /// </summary>
    [Column("addressId")]
    public int AddressId { get; set; }

    /// <summary>
    /// List of Appointments that 
    /// scheduled with this Customer
    /// </summary>
    public List<Appointment> Appointments { get; set; }
  }
}
