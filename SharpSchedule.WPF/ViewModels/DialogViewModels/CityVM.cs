﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using SharpSchedule.Commands.AddressVMCommands;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.Data.Repositories.Location;
using SharpSchedule.Models;
using SharpSchedule.ViewModels.Validation;

namespace SharpSchedule.ViewModels.DialogViewModels
{
  /// <summary>
  /// View Model for City CRUD in the City Dialog
  /// </summary>
  public class CityVM : ValidationBase
  {
    private readonly ICityRepository _repository;
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

    /// <summary>
    /// Filtered Countries
    /// </summary>
    public ObservableCollection<Country> Countries { get; set; } = new ObservableCollection<Country>();

    /// <summary>
    /// All Countries currently in the System
    /// </summary>
    public List<Country> AllCountries { get; set; } = new List<Country>();

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
          City.CountryId = value.Id;
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

    public ICommand SearchCountries { get; }

    /// <summary>
    /// The City this VM is performing CRUD ops on
    /// </summary>
    public City City { get; set; }

    public CityVM(ICityRepository cityRepository, List<Country> allCountries,
                    CUD cud, Action action, User user, City city = null)
    {
      _repository = cityRepository;

      AllCountries = allCountries;

      foreach (Country country in AllCountries)
        Countries.Add(country);

      _cud = cud;
      Enabled = cud != CUD.Delete;
      CUDString = cud.ToString();
      CloseAction = action;

      if (city != null)
      {
        City = city;
        City.LastUpdatedBy = user.Username;
        Name = City.Name;
        CountrySelected = AllCountries.Where(pr => pr.Id == city.CountryId).First();
      }
      else
      {
        City = new City
        {
          CreatedBy = user.Username,
          CreateDate = DateTime.UtcNow,
          LastUpdatedBy = user.Username
        };

        Name = string.Empty;
        CountrySelected = null;
      }

      CRUDCommand = new CityCRUDCommand(this);
      SearchCountries = new SearchCountriesCommand(this);
    }

    public async Task DBUpdate()
    {
      City.LastUpdate = DateTime.UtcNow;
      City.Country = null;

      switch (_cud)
      {
        case CUD.Create:
          await _repository.Create(City).ConfigureAwait(true);
          Close();
          break;
        case CUD.Update:
          await _repository.Update(City).ConfigureAwait(true);
          Close();
          break;
        case CUD.Delete:
          await _repository.Delete(City.Id).ConfigureAwait(true);
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
