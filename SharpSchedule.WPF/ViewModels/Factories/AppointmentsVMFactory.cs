using SharpSchedule.Data.Repositories.Scheduling;
using SharpSchedule.Services.Interfaces;

namespace SharpSchedule.ViewModels.Factories
{
  public class AppointmentsVMFactory : IVMFactory<AppointmentsVM>
  {
    private readonly IAppointmentRepository _repository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IAuthService _authService;

    public AppointmentsVMFactory(
      IAppointmentRepository repository,
      ICustomerRepository customerRepository,
      IAuthService authService)
    {
      _repository = repository;
      _customerRepository = customerRepository;
      _authService = authService;
    }

    public AppointmentsVM CreateVM()
    {
      return new AppointmentsVM(_repository, _customerRepository, _authService.GetCurrent());
    }
  }
}
