using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharpSchedule.Data.EntityModels.Location
{
  /// <summary>
  /// City, in a Country, used for Addresses
  /// </summary>
  public class City : BaseModel
  {
    /// <summary>
    /// Name of the City
    /// </summary>
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
    public int CountryId { get; set; }

    /// <summary>
    /// Addresses in this City
    /// </summary>
    public List<Address> Addresses { get; set; }
  }
}
