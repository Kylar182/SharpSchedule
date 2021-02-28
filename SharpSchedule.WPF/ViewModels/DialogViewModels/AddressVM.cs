using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using SharpSchedule.Commands.AddressVMCommands;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.Data.Repositories;
using SharpSchedule.Data.Repositories.Location;
using SharpSchedule.Models;
using SharpSchedule.ViewModels.Validation;

namespace SharpSchedule.ViewModels.DialogViewModels
{
  /// <summary>
  /// View Model for City CRUD in the City Dialog
  /// </summary>
  public class AddressVM : ValidationBase
  {
    private readonly IAddressRepository _repository;
    private readonly IRepository<City> _cityRepository;
    private readonly CUD _cud;

    private bool enabled;
    public bool Enabled
    {
      get => enabled;
      set
      {
        enabled = value;
        OnPropChanged(nameof(Enabled));
      }
    }

    private string streetAddress;
    /// <summary>
    /// Street Address
    /// </summary>
    [Required(ErrorMessage = "Street Address is Required")]
    [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
    public string StreetAddress
    {
      get => streetAddress;
      set
      {
        ValidateProp(value);
        streetAddress = value;
        OnPropChanged(nameof(StreetAddress));
        OnPropChanged(nameof(StreetAddressText));
        OnPropChanged(nameof(StreetAddressValid));

        if (!PropHasErrors(nameof(StreetAddress)))
          Address.StreetAddress = value;
      }
    }

    /// <summary>
    /// Helpertext for the Street Address
    /// </summary>
    /// <remarks>
    /// If Street Address is invalid, returns the first Error message, 
    /// otherwise it returns the designated Helpertext
    /// </remarks>
    public string StreetAddressText => PropHasErrors(nameof(StreetAddress)) ?
      GetErrors(nameof(StreetAddress)).OfType<string>().First() : "Input Street Address";

    /// <summary>
    /// Bool to determine if StreetAddress is Valid or not
    /// </summary>
    public bool StreetAddressValid => !PropHasErrors(nameof(StreetAddress));

    private string address2;
    /// <summary>
    /// Address line 2, PO/Apt Number
    /// </summary>
    [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
    public string Address2
    {
      get => address2;
      set
      {
        ValidateProp(value);
        address2 = value;
        OnPropChanged(nameof(Address2));
        OnPropChanged(nameof(Address2Text));
        OnPropChanged(nameof(Address2Valid));

        if (!PropHasErrors(nameof(Address2)))
          Address.Address2 = value;
      }
    }

    /// <summary>
    /// Helpertext for Address Line 2
    /// </summary>
    /// <remarks>
    /// If Address Line 2 is invalid, returns the first Error message, 
    /// otherwise it returns the designated Helpertext
    /// </remarks>
    public string Address2Text => PropHasErrors(nameof(Address2)) ?
      GetErrors(nameof(Address2)).OfType<string>().First() : "Input Address line 2, PO/Apt Number";

    /// <summary>
    /// Bool to determine if Address2 is Valid or not
    /// </summary>
    public bool Address2Valid => !PropHasErrors(nameof(Address2));

    private string postalCode;
    /// <summary>
    /// Zip/Postal Code as a string
    /// </summary>
    [Required(ErrorMessage = "Postal Code is Required")]
    [MaxLength(10, ErrorMessage = "Max Length 10 Characters")]
    public string PostalCode
    {
      get => postalCode;
      set
      {
        ValidateProp(value);
        postalCode = value;
        OnPropChanged(nameof(PostalCode));
        OnPropChanged(nameof(PostalCodeText));
        OnPropChanged(nameof(PostalCodeValid));

        if (!PropHasErrors(nameof(PostalCode)))
          Address.PostalCode = value;
      }
    }

    /// <summary>
    /// Helpertext for the Postal Code
    /// </summary>
    /// <remarks>
    /// If Zip/Postal Code is invalid, returns the first Error message, 
    /// otherwise it returns the designated Helpertext
    /// </remarks>
    public string PostalCodeText => PropHasErrors(nameof(PostalCode)) ?
      GetErrors(nameof(PostalCode)).OfType<string>().First() : "Input Zip/Postal Code";

    /// <summary>
    /// Bool to determine if PostalCode is Valid or not
    /// </summary>
    public bool PostalCodeValid => !PropHasErrors(nameof(PostalCode));

    private string phone;
    /// <summary>
    /// Phone Number as a string
    /// </summary>
    [Required(ErrorMessage = "Phone Number is Required")]
    [MaxLength(20, ErrorMessage = "Max Length 10 Characters")]
    public string Phone
    {
      get => phone;
      set
      {
        ValidateProp(value);
        phone = value;
        OnPropChanged(nameof(Phone));
        OnPropChanged(nameof(PhoneText));
        OnPropChanged(nameof(PhoneValid));

        if (!PropHasErrors(nameof(Phone)))
          Address.Phone = value;
      }
    }

    /// <summary>
    /// Helpertext for the Phone Number
    /// </summary>
    /// <remarks>
    /// If Phone Number is invalid, returns the first Error message, 
    /// otherwise it returns the designated Helpertext
    /// </remarks>
    public string PhoneText => PropHasErrors(nameof(Phone)) ?
      GetErrors(nameof(Phone)).OfType<string>().First() : "Input Phone Number";

    /// <summary>
    /// Bool to determine if Phone is Valid or not
    /// </summary>
    public bool PhoneValid => !PropHasErrors(nameof(Phone));

    /// <summary>
    /// Filtered Cities
    /// </summary>
    public ObservableCollection<City> Cities { get; set; } = new ObservableCollection<City>();

    /// <summary>
    /// All Cities currently in the System
    /// </summary>
    public List<City> AllCities { get; set; } = new List<City>();

    public string WindowLabel => cudString + " Address";

    private City city;
    [Required(ErrorMessage = "City is Required")]
    public City CitySelected
    {
      get => city;
      set
      {
        ValidateProp(value);
        city = value;

        OnPropChanged(nameof(CitySelected));

        if (!PropHasErrors(nameof(CitySelected)))
          Address.CityId = value.Id;
      }
    }

    private string cudString;
    /// <summary>
    /// String of CUD Enum, used for Display
    /// </summary>
    public string CUDString
    {
      get => cudString;
      set
      {
        cudString = value;
        OnPropChanged(nameof(CUDString));
        OnPropChanged(nameof(WindowLabel));
      }
    }

    public Action CloseAction { get; set; }

    public ICommand CRUDCommand { get; }

    public ICommand SearchCities { get; }

    /// <summary>
    /// The City this VM is performing CRUD ops on
    /// </summary>
    public Address Address { get; set; }

    public AddressVM(IAddressRepository repository, ICityRepository cityRepository,
                    CUD cud, Action action, User user, Address address = null)
    {
      _repository = repository;
      _cityRepository = cityRepository;

      _cud = cud;
      Enabled = cud != CUD.Delete;
      CUDString = cud.ToString();
      CloseAction = action;

      Load().ConfigureAwait(true);

      if (address != null)
      {
        Address = address;
        StreetAddress = address.StreetAddress;
        Address2 = address.Address2;
        PostalCode = address.PostalCode;
        Phone = address.Phone;
        Address.LastUpdatedBy = user.Username;
        CitySelected = AllCities.Where(pr => pr.Id == address.CityId).First();
      }
      else
      {
        Address = new Address
        {
          CreatedBy = user.Username,
          CreateDate = DateTime.UtcNow,
          LastUpdatedBy = user.Username
        };

        StreetAddress = string.Empty;
        Address2 = string.Empty;
        PostalCode = string.Empty;
        Phone = string.Empty;

        CitySelected = null;
      }

      CRUDCommand = new AddressCRUDCommand(this);
      SearchCities = new SearchCitiesCommand(this);
    }

    /// <summary>
    /// Loads DB Data for Dialog
    /// </summary>
    private async Task Load()
    {
      await _cityRepository.GetAll().ContinueWith(t =>
      {
        if (t.Exception == null)
        {
          AllCities = t.Result;
          foreach (City city in AllCities)
            Cities.Add(city);
        }
      }).ConfigureAwait(true);
    }

    public async Task DBUpdate()
    {
      Address.LastUpdate = DateTime.UtcNow;
      Address.City = null;

      switch (_cud)
      {
        case CUD.Create:
          await _repository.Create(Address).ConfigureAwait(true);
          Close();
          break;
        case CUD.Update:
          await _repository.Update(Address).ConfigureAwait(true);
          Close();
          break;
        case CUD.Delete:
          await _repository.Delete(Address.Id).ConfigureAwait(true);
          Close();
          break;
      };
    }

    public void Close()
    {
      CloseAction();
    }
  }
}
