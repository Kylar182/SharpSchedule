using SharpSchedule.State.Navigators;

namespace SharpSchedule.ViewModels.Factories
{
  public interface IVMAbstractFactory
  {
    ViewModelBase CreateVM(ViewType type);
  }
}
