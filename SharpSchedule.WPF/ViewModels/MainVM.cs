using SharpSchedule.State.Navigators;

namespace SharpSchedule.ViewModels
{
  /// <summary>
  /// Parent VM for all VMs in the Single Page Application
  /// </summary>
  public class MainVM : ViewModelBase
  {
    public INavigator Navigator { get; set; }

    public MainVM(INavigator navigator)
    {
      Navigator = navigator;
      Navigator.UpdateCurrentVM.Execute(ViewType.Login);
    }
  }
}
