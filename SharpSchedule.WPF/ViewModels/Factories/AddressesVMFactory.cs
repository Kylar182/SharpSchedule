using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.Data.Repositories;
using SharpSchedule.Data.Repositories.Location;

namespace SharpSchedule.ViewModels.Factories
{
  public class AddressesVMFactory : IVMFactory<AddressesVM>
  {
    private readonly IAddressRepository _repository;
    private readonly ICityRepository _cityRepository;
    private readonly IRepository<Country> _countryRepository;

    public AddressesVMFactory(
      IAddressRepository repository,
      ICityRepository cityRepository, 
      IRepository<Country> countryRepository)
    {
      _repository = repository;
      _cityRepository = cityRepository;
      _countryRepository = countryRepository;
    }

    public AddressesVM CreateVM()
    {
      return new AddressesVM(_repository, _cityRepository, _countryRepository);
    }
  }
}
