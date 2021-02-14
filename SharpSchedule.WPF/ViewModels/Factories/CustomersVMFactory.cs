using SharpSchedule.Data.Repositories.Scheduling;

namespace SharpSchedule.ViewModels.Factories
{
  public class CustomersVMFactory : IVMFactory<CustomersVM>
  {
    private readonly ICustomerRepository _repository;

    public CustomersVMFactory(ICustomerRepository repository)
    {
      _repository = repository;
    }

    public CustomersVM CreateVM()
    {
      return new CustomersVM(_repository);
    }
  }
}
