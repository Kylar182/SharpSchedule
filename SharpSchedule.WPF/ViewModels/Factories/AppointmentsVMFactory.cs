using SharpSchedule.Data.DTOs;
using SharpSchedule.Data.Repositories;
using SharpSchedule.Data.Repositories.Scheduling;
using SharpSchedule.Services.Interfaces;
using SharpSchedule.State;

namespace SharpSchedule.ViewModels.Factories
{
  public class AppointmentsVMFactory : IVMFactory<AppointmentsVM>
  {
    private readonly IAppointmentRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IStateManager<AppointmentDTO> _state;
    private readonly IAuthService _authService;

    public AppointmentsVMFactory(
      IAppointmentRepository repository,
      IUserRepository userRepository,
      ICustomerRepository customerRepository,
      IStateManager<AppointmentDTO> state,
      IAuthService authService)
    {
      _repository = repository;
      _userRepository = userRepository;
      _customerRepository = customerRepository;
      _state = state;
      _authService = authService;
    }

    public AppointmentsVM CreateVM()
    {
      return new AppointmentsVM(_repository, _userRepository, _customerRepository,
                                                    _state, _authService.GetCurrent());
    }
  }
}
