namespace SharpSchedule.ViewModels.Factories
{
  public class HomeVMFactory : IVMFactory<HomeVM>
  {
    public HomeVM CreateVM()
    {
      return new HomeVM();
    }
  }
}
