using SharpSchedule.State.Navigators;

namespace SharpSchedule.ViewModels.Factories
{
  /// <summary>
  /// Factory DI to Create a New View Model and inject Scoped 
  /// services into it without breaking the Factory Singleton
  /// </summary>
  public interface IRootVMFactory
  {
    ViewModelBase CreateVM(ViewType type);
  }
}
