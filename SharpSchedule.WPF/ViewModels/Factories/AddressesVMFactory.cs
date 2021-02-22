using SharpSchedule.Data.EntityModels.Locations;
using SharpSchedule.Data.Repositories;
using SharpSchedule.Data.Repositories.Location;
using SharpSchedule.Services.Interfaces;

namespace SharpSchedule.ViewModels.Factories
{
  public class AddressesVMFactory : IVMFactory<AddressesVM>
  {
    private readonly IAddressRepository _repository;
    private readonly ICityRepository _cityRepository;
    private readonly IRepository<Country> _countryRepository;
    private readonly IAuthService _authService;

    public AddressesVMFactory(
      IAddressRepository repository,
      ICityRepository cityRepository,
      IRepository<Country> countryRepository,
      IAuthService authService)
    {
      _repository = repository;
      _cityRepository = cityRepository;
      _countryRepository = countryRepository;
      _authService = authService;
    }

    public AddressesVM CreateVM()
    {
      return new AddressesVM(_repository, _cityRepository, _countryRepository, _authService.GetCurrent());
    }
  }
}
