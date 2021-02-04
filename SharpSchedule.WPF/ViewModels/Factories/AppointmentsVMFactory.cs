using SharpSchedule.Data.Repositories.Scheduling;

namespace SharpSchedule.ViewModels.Factories
{
  public class AppointmentsVMFactory : IVMFactory<AppointmentsVM>
  {
    private readonly IAppointmentRepository _repository;

    public AppointmentsVMFactory(IAppointmentRepository repository)
    {
      _repository = repository;
    }

    public AppointmentsVM CreateVM()
    {
      return new AppointmentsVM(_repository);
    }
  }
}
