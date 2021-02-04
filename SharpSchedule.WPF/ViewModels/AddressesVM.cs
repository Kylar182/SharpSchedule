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

    /// <summary>
    /// All addresses currently in the System
    /// </summary>
    public ObservableCollection<Address> Addresses { get; set; }

    public AddressesVM(
      IAddressRepository repository)
    {
      _repository = repository;
      Addresses = new ObservableCollection<Address>();
      Load();
    }

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
    }
  }
}
