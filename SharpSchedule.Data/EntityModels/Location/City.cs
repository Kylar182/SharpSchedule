using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpSchedule.Data.EntityModels.Location
{
  /// <summary>
  /// City, in a Country, used for Addresses
  /// </summary>
  [Table("cities")]
  public class City : BaseModel
  {
    /// <summary>
    /// Database Unique Identifier - System Id
    /// </summary>
    [Key]
    [Column("cityId")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Name of the City
    /// </summary>
    [Column("city")]
    [Required]
    [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
    public string Name { get; set; }

    /// <summary>
    /// Country that this City is in
    /// </summary>
    public Country Country { get; set; }
    /// <summary>
    /// FKey to the Country this City Belongs too
    /// </summary>
    [Column("countryId")]
    public int CountryId { get; set; }

    /// <summary>
    /// Addresses in this City
    /// </summary>
    public List<Address> Addresses { get; set; }
  }
}
