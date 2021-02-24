using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using SharpSchedule.Commands.AddressVMCommands;
using SharpSchedule.Data.EntityModels;
using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.Data.Repositories;
using SharpSchedule.Data.Repositories.Location;

namespace SharpSchedule.ViewModels
{
  /// <summary>
  /// View Model for Address CRUD Ops on the Addresses View
  /// </summary>
  public class AddressesVM : ViewModelBase
  {
    private readonly IAddressRepository _repository;
    private readonly ICityRepository _cityRepository;
    private readonly IRepository<Country> _countryRepository;
    private readonly User _user;

    private City city;
    [Required(ErrorMessage = "City is Required")]
    public City CitySelected
    {
      get => city;
      set
      {
        city = value;
        OnPropChanged(nameof(CitySelected));
      }
    }

    /// <summary>
    /// Filtered Addresses
    /// </summary>
    public ObservableCollection<Address> Addresses { get; set; } = new ObservableCollection<Address>();

    /// <summary>
    /// All Addresses currently in the System
    /// </summary>
    public List<Address> AllAddresses { get; set; } = new List<Address>();

    /// <summary>
    /// Filtered Cities
    /// </summary>
    public ObservableCollection<City> Cities { get; set; } = new ObservableCollection<City>();

    /// <summary>
    /// All Cities currently in the System
    /// </summary>
    public List<City> AllCities { get; set; } = new List<City>();

    public ICommand SearchCities { get; }

    public ICommand SearchAddresses { get; }

    public ICommand NewCity { get; }

    public ICommand UpdateCity { get; }

    public ICommand DeleteCity { get; }

    public ICommand NewAddress { get; }

    public ICommand UpdateAddress { get; }

    public ICommand DeleteAddress { get; }

    public AddressesVM(
      IAddressRepository repository,
      ICityRepository cityRepository,
      IRepository<Country> countryRepository,
      User user)
    {
      _repository = repository;
      _cityRepository = cityRepository;
      _countryRepository = countryRepository;
      _user = user;
      CityUpdate();
      AddressUpdate();
      CitySelected = null;
      SearchCities = new SearchCitiesCommand(this);
      SearchAddresses = new SearchAddressesCommand(this);
      NewCity = new NewCityCommand(this, _cityRepository, _countryRepository, _user);
      UpdateCity = new UpdateCityCommand(this, _cityRepository, _countryRepository, _user);
    }

    public void CityUpdate()
    {
      ObservableCollection<City> cities = Cities;

      _cityRepository.GetAll().ContinueWith(t =>
      {
        if (t.Exception == null)
        {
          AllCities = t.Result;

          cities.Clear();
          foreach (City city in AllCities)
            cities.Add(city);
        }
      }).ConfigureAwait(true);

      Cities = cities;
    }

    public void AddressUpdate()
    {
      ObservableCollection<Address> addresses = Addresses;

      _repository.GetAll().ContinueWith(t =>
      {
        if (t.Exception == null)
        {
          AllAddresses = t.Result;

          addresses.Clear();
          foreach (Address address in AllAddresses)
            addresses.Add(address);
        }
      }).ConfigureAwait(true);

      Addresses = addresses;
    }
  }
}
