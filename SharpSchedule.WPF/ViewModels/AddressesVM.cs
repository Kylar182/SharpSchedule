using System.Collections.ObjectModel;
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
    /// All addresses currently in the System
    /// </summary>
    public ObservableCollection<Address> Addresses { get; set; }

    /// <summary>
    /// All cities currently in the System
    /// </summary>
    public ObservableCollection<City> Cities { get; set; }

    public AddressesVM(
      IAddressRepository repository,
      ICityRepository cityRepository)
    {
      _repository = repository;
      _cityRepository = cityRepository;
      Addresses = new ObservableCollection<Address>();
      Cities = new ObservableCollection<City>();
      Load();
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
          Addresses.Clear();
          foreach (Address address in a.Result)
            Addresses.Add(address);
        }
      });

      _cityRepository.GetAll().ContinueWith(a =>
      {
        if (a.Exception == null)
        {
          Cities.Clear();
          foreach (City city in a.Result)
            Cities.Add(city);
        }
      });
    }
  }
}
