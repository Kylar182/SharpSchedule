using SharpSchedule.Data.Repositories.Scheduling;

namespace SharpSchedule.ViewModels
{
  public class CustomersVM : ViewModelBase
  {
    private readonly ICustomerRepository _repository;

    public CustomersVM(ICustomerRepository repository)
    {
      _repository = repository;
    }
  }
}
