using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpSchedule.Data.EntityModels
{
  /// <summary>
  /// Base Model for inheritance
  /// </summary>
  public abstract class BaseModel
  {
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
    [Column("createdBy")]
    [Required]
    [MaxLength(40)]
    public string CreatedBy { get; set; }

    /// <summary>
    /// Date / Time the Model was Created
    /// </summary>
    /// <remarks>
    /// The Variable names are also a requirement
    /// </remarks>
    [Column("createDate")]
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
    [Column("lastUpdateBy")]
    [Required]
    [MaxLength(40)]
    public string LastUpdatedBy { get; set; }

    /// <summary>
    /// Date / Time the Model was last Updated
    /// </summary>
    /// <remarks>
    /// The Variable names are also a requirement
    /// </remarks>
    [Column("lastUpdate")]
    public DateTime LastUpdate { get; set; }
  }
}
