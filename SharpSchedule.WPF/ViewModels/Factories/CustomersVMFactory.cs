using SharpSchedule.Data.Repositories.Location;
using SharpSchedule.Data.Repositories.Scheduling;
using SharpSchedule.Services.Interfaces;

namespace SharpSchedule.ViewModels.Factories
{
  public class CustomersVMFactory : IVMFactory<CustomersVM>
  {
    private readonly ICustomerRepository _repository;
    private readonly IAddressRepository _addressRepository;
    private readonly IAuthService _authService;

    public CustomersVMFactory(ICustomerRepository repository, 
      IAddressRepository addressRepository, 
      IAuthService authService)
    {
      _repository = repository;
      _addressRepository = addressRepository;
      _authService = authService;
    }

    public CustomersVM CreateVM()
    {
      return new CustomersVM(_repository, _addressRepository, _authService.GetCurrent());
    }
  }
}
