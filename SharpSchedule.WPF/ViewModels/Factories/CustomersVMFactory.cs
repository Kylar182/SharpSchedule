namespace SharpSchedule.ViewModels.Factories
{
  public class CustomersVMFactory : IVMFactory<CustomersVM>
  {
    public CustomersVM CreateVM()
    {
      return new CustomersVM();
    }
  }
}
