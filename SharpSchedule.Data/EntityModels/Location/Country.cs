using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharpSchedule.Data.EntityModels.Location
{
  /// <summary>
  /// Country with Cities, for Addresses
  /// </summary>
  public class Country : BaseModel
  {
    /// <summary>
    /// Name of the Country
    /// </summary>
    [Required]
    [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
    public string Name { get; set; }

    /// <summary>
    /// List of Cities in this Country
    /// </summary>
    public List<City> Cities { get; set; }
  }
}
