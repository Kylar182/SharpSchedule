using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
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
    /// <summary>
    /// Currently Selected City in City Grid
    /// </summary>
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

    private Address address;
    /// <summary>
    /// Currently Selected Address in Address Grid
    /// </summary>
    [Required(ErrorMessage = "Address is Required")]
    public Address AddressSelected
    {
      get => address;
      set
      {
        address = value;
        OnPropChanged(nameof(AddressSelected));
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
      CityUpdate().ConfigureAwait(true);
      AddressUpdate().ConfigureAwait(true);
      CitySelected = null;
      AddressSelected = null;
      SearchCities = new SearchCitiesCommand(this);
      SearchAddresses = new SearchAddressesCommand(this);
      NewCity = new NewCityCommand(this, _cityRepository, _countryRepository, _user);
      UpdateCity = new UpdateCityCommand(this, _cityRepository, _countryRepository, _user);
      DeleteCity = new DeleteCityCommand(this, _cityRepository, _countryRepository, _user);
      NewAddress = new NewAddressCommand(this, _repository, _cityRepository, _user);
      UpdateAddress = new UpdateAddressCommand(this, _repository, _cityRepository, _user);
      DeleteAddress = new DeleteAddressCommand(this, _repository, _cityRepository, _user);
    }

    public async Task CityUpdate()
    {
      AllCities = await GetAllCities().ConfigureAwait(true);

      Cities.Clear();

      foreach (City city in AllCities)
        Cities.Add(city);
    }

    public async Task AddressUpdate()
    {
      AllAddresses = await GetAllAddresses().ConfigureAwait(true);

      Addresses.Clear();

      foreach (Address address in AllAddresses)
        Addresses.Add(address);
    }

    /// <summary>
    /// Gets All Cities from the DB
    /// </summary>
    public async Task<List<City>> GetAllCities()
    {
      List<City> transfer = new List<City>();

      await _cityRepository.GetAll().ContinueWith(t =>
      {
        if (t.Exception == null)
        {
          transfer = t.Result;
        }
      }).ConfigureAwait(true);

      return transfer;
    }

    /// <summary>
    /// Gets All Addresses from the DB
    /// </summary>
    public async Task<List<Address>> GetAllAddresses()
    {
      List<Address> transfer = new List<Address>();

      await _repository.GetAll().ContinueWith(t =>
      {
        if (t.Exception == null)
        {
          transfer = t.Result;
        }
      }).ConfigureAwait(true);

      return transfer;
    }

  }
}
