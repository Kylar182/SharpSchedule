namespace SharpSchedule.ViewModels.Factories
{
  public class AddressesVMFactory : IVMFactory<AddressesVM>
  {
    public AddressesVM CreateVM()
    {
      return new AddressesVM();
    }
  }
}
