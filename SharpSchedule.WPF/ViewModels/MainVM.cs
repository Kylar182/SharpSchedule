using SharpSchedule.State.Navigators;

namespace SharpSchedule.ViewModels
{
  public class MainVM : ViewModelBase
  {
    public INavigator Navigator { get; set; }

    public MainVM(INavigator navigator)
    {
      Navigator = navigator;
      Navigator.UpdateCurrentVM.Execute(ViewType.Home);
    }
  }
}
