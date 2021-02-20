using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using SharpSchedule.Commands.AddressVMCommands;
using SharpSchedule.Data.EntityModels.Locations;
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

    /// <summary>
    /// Filtered Addresses
    /// </summary>
    public ObservableCollection<Address> Addresses { get; set; }

    /// <summary>
    /// All Addresses currently in the System
    /// </summary>
    public List<Address> AllAddresses { get; set; }

    /// <summary>
    /// Filtered Cities
    /// </summary>
    public ObservableCollection<City> Cities { get; set; }

    /// <summary>
    /// All Cities currently in the System
    /// </summary>
    public List<City> AllCities { get; set; }

    public ICommand SearchCities { get; }

    public ICommand SearchAddresses { get; }

    public AddressesVM(
      IAddressRepository repository,
      ICityRepository cityRepository)
    {
      _repository = repository;
      _cityRepository = cityRepository;
      Addresses = new ObservableCollection<Address>();
      Cities = new ObservableCollection<City>();
      Load();
      SearchCities = new SearchCitiesCommand(this);
      SearchAddresses = new SearchAddressesCommand(this);
    }

    /// <summary>
    /// Loads all Addresses and Cities
    /// </summary>
    private void Load()
    {
      _repository.GetAll().ContinueWith(a =>
      {
        if (a.Exception == null)
        {
          AllAddresses = a.Result;
          AllCities = AllAddresses.Select(pr => pr.City).Distinct().ToList();
          Addresses.Clear();

          Cities.Clear();
          foreach (City city in AllCities)
            Cities.Add(city);

          foreach (Address address in AllAddresses)
            Addresses.Add(address);
        }
      });
    }
  }
}
