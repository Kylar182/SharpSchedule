using System.ComponentModel.DataAnnotations;

namespace SharpSchedule.Data.EntityModels.Location
{
  public class Country : BaseModel
  {
    /// <summary>
    /// Name of the Country
    /// </summary>
    [Required]
    [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
    public string Name { get; set; }
  }
}
