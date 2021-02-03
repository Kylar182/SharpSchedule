namespace SharpSchedule.ViewModels.Factories
{
  public class AppointmentsVMFactory : IVMFactory<AppointmentsVM>
  {
    public AppointmentsVM CreateVM()
    {
      return new AppointmentsVM();
    }
  }
}
