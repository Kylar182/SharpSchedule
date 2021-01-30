using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpSchedule.Data.EntityModels
{
  /// <summary>
  /// Base Model for inheritance
  /// </summary>
  public abstract class BaseModel
  {
    /// <summary>
    /// Database Unique Identifier - System Id
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// User name of the user that created the Model
    /// </summary>
    /// <remarks>
    /// I realize it's absolutley stupid to record this 
    /// as a string and not an int FKey to the User 
    /// model but this is a specific course requirement. 
    /// I also understand it's bonkers to have different
    /// length requirements than the username as well.
    /// </remarks>
    [Required]
    [MaxLength(40)]
    public string CreatedBy { get; set; }

    /// <summary>
    /// Date / Time the Model was Created
    /// </summary>
    /// <remarks>
    /// The Variable names are also a requirement
    /// </remarks>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// User name of the user that most recently updated the Model
    /// </summary>
    /// <remarks>
    /// I realize it's absolutley stupid to record this 
    /// as a string and not an int FKey to the User 
    /// model but this is a specific course requirement. 
    /// I also understand it's bonkers to have different
    /// length requirements than the username as well.
    /// </remarks>
    [Required]
    [MaxLength(40)]
    public string LastUpdatedBy { get; set; }

    /// <summary>
    /// Date / Time the Model was last Updated
    /// </summary>
    /// <remarks>
    /// The Variable names are also a requirement
    /// </remarks>
    public DateTime LastUpdate { get; set; }
  }
}
