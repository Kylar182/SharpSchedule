using SharpSchedule.State.Navigators;

namespace SharpSchedule.ViewModels
{
  public class MainVM : ViewModelBase
  {
    public INavigator Navigator { get; set; } = new Navigator();
  }
}
