using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharpSchedule.Data.EntityModels.Scheduling;

namespace SharpSchedule.Data.EntityModels.Location
{
  /// <summary>
  /// Address of a Location, in a City which is in a Country
  /// </summary>
  public class Address : BaseModel
  {
    /// <summary>
    /// Address line 1, Street Address
    /// </summary>
    [Required]
    [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
    public string StreetAddress { get; set; }

    /// <summary>
    /// Address line 2, PO/Apt Number
    /// </summary>
    [Required]
    [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
    public string Address2 { get; set; }

    /// <summary>
    /// Zip/Postal Code as a string
    /// </summary>
    [Required]
    [MaxLength(10, ErrorMessage = "Max Length 10 Characters")]
    public string PostalCode { get; set; }

    /// <summary>
    /// Phone Number as a string
    /// </summary>
    [Required]
    [MaxLength(20, ErrorMessage = "Max Length 10 Characters")]
    public string Phone { get; set; }

    /// <summary>
    /// City that this Address is in
    /// </summary>
    public City City { get; set; }
    /// <summary>
    /// FKey to the City this Address belongs too
    /// </summary>
    public int CityId { get; set; }

    /// <summary>
    /// List of Customers at this address
    /// </summary>
    /// <remarks>
    /// I realize this doesn't make since 
    /// but this is a course requirement
    /// </remarks>
    public List<Customer> Customers { get; set; }
  }
}
