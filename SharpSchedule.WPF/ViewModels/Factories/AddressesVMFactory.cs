using SharpSchedule.Data.Repositories.Location;

namespace SharpSchedule.ViewModels.Factories
{
  public class AddressesVMFactory : IVMFactory<AddressesVM>
  {
    private readonly IAddressRepository _repository;

    public AddressesVMFactory(IAddressRepository repository)
    {
      _repository = repository;
    }

    public AddressesVM CreateVM()
    {

      return new AddressesVM(_repository);
    }
  }
}
