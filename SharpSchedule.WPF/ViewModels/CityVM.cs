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

namespace SharpSchedule.ViewModels
{
  /// <summary>
  /// View Model for City CRUD in the City Dialog
  /// </summary>
  public class CityVM : ValidationBase
  {
    private readonly ICityRepository _repository;
    private readonly IRepository<Country> _countryRepository;
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

    /// <summary>
    /// Filtered Countries
    /// </summary>
    public ObservableCollection<Country> Countries { get; set; }

    /// <summary>
    /// All Countries currently in the System
    /// </summary>
    public List<Country> AllCountries { get; set; }

    public ICommand SearchCountries { get; }

    public string WindowLabel => cudString + " City";

    private Country country;
    [Required(ErrorMessage = "Country is Required")]
    public Country CountrySelected
    {
      get => country;
      set
      {
        ValidateProp(value);
        country = value;

        OnPropChanged(nameof(CountrySelected));

        if (!PropHasErrors(nameof(CountrySelected)))
        {
          City.CountryId = value.Id;
          City.Country = value;
        }
      }
    }

    private string name;
    /// <summary>
    /// Name of the City
    /// </summary>
    [Required(ErrorMessage = "City's Name is required")]
    [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
    public string Name
    {
      get => name;
      set
      {
        ValidateProp(value);
        name = value;
        OnPropChanged(nameof(Name));
        OnPropChanged(nameof(NameText));
        OnPropChanged(nameof(NameValid));

        if (NameValid)
          City.Name = value;
      }
    }

    /// <summary>
    /// Helpertext for the city's Name
    /// </summary>
    /// <remarks>
    /// If Name is invalid, returns the first Error message, 
    /// otherwise it returns the designated Helpertext
    /// </remarks>
    public string NameText => PropHasErrors(nameof(Name)) ?
      GetErrors(nameof(Name)).OfType<string>().First() : "Input City's Name";

    /// <summary>
    /// Bool to determine if Name is Valid or not
    /// </summary>
    public bool NameValid => !PropHasErrors(nameof(Name));

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

    /// <summary>
    /// The City this VM is performing CRUD ops on
    /// </summary>
    public City City { get; set; }

    public CityVM(ICityRepository cityRepository, IRepository<Country> countryRepository,
                    CUD cud, Action action, User user, City city = null)
    {
      _repository = cityRepository;
      _countryRepository = countryRepository;
      _cud = cud;
      Enabled = cud != CUD.Delete;
      CUDString = cud.ToString();
      CloseAction = action;
      Countries = new ObservableCollection<Country>();
      Load();

      if (city != null)
      {
        City = city;
        City.LastUpdatedBy = user.Username;
        Name = City.Name;
        CountrySelected = AllCountries.Where(pr => pr.Id == City.CountryId).First();
      }
      else
      {
        City = new City
        {
          CreatedBy = user.Username,
          CreateDate = DateTime.UtcNow,
          LastUpdatedBy = user.Username,
          LastUpdate = DateTime.UtcNow
        };

        Name = string.Empty;
        CountrySelected = null;
      }

      CRUDCommand = new CityCRUDCommand(this);
    }

    /// <summary>
    /// Loads DB Data for Dialog
    /// </summary>
    private void Load()
    {
      _countryRepository.GetAll().ContinueWith(t =>
      {
        if (t.Exception == null)
        {
          AllCountries = t.Result;
          foreach (Country country in AllCountries)
            Countries.Add(country);
        }
      });
    }

    public async Task DBUpdate()
    {
      City.LastUpdate = DateTime.UtcNow;

      switch (_cud)
      {
        case CUD.Create:
          await _repository.Create(City);
          Close();
          break;
        case CUD.Update:
          await _repository.Update(City);
          Close();
          break;
        case CUD.Delete:
          await _repository.Delete(City.Id);
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
