using SharpSchedule.Data.DTOs;
using SharpSchedule.State;

namespace SharpSchedule.ViewModels.Factories
{
  public class HomeVMFactory : IVMFactory<HomeVM>
  {
    //private readonly IAppointmentRepository _repository;
    private readonly IStateManager<AppointmentDTO> _state;

    public HomeVMFactory(
      //IAppointmentRepository repository,
      IStateManager<AppointmentDTO> state)
    {
      //_repository = repository;
      _state = state;
    }

    public HomeVM CreateVM()
    {
      return new HomeVM(_state);
    }
  }
}
