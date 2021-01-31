using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpSchedule.Data.EntityModels.Locations
{
  /// <summary>
  /// Country with Cities, for Addresses
  /// </summary>
  [Table("country")]
  public class Country : BaseModel
  {
    /// <summary>
    /// Database Unique Identifier - System Id
    /// </summary>
    [Key]
    [Column("countryId")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Name of the Country
    /// </summary>
    [Column("country")]
    [Required]
    [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
    public string Name { get; set; }

    /// <summary>
    /// List of Cities in this Country
    /// </summary>
    public List<City> Cities { get; set; }
  }
}
