using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpSchedule.Data.EntityModels.Location
{
  /// <summary>
  /// City, in a Country, in an Address
  /// </summary>
  public class City : BaseModel
  {
    /// <summary>
    /// Name of the City
    /// </summary>
    [Required]
    [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
    public string Name { get; set; }

  }
}
