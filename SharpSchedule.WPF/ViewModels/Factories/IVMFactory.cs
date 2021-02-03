namespace SharpSchedule.ViewModels.Factories
{
  public interface IVMFactory<T> where T : ViewModelBase
  {
    T CreateVM();
  }
}
