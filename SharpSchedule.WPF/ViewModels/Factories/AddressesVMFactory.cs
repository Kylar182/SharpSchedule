using SharpSchedule.Data.Repositories.Location;

namespace SharpSchedule.ViewModels.Factories
{
  public class AddressesVMFactory : IVMFactory<AddressesVM>
  {
    private readonly IAddressRepository _repository;
    private readonly ICityRepository _cityRepository;

    public AddressesVMFactory(
      IAddressRepository repository, 
      ICityRepository cityRepository)
    {
      _repository = repository;
      _cityRepository = cityRepository;
    }

    public AddressesVM CreateVM()
    {
      return new AddressesVM(_repository, _cityRepository);
    }
  }
}
