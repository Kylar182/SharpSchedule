using SharpSchedule.Models;
using SharpSchedule.ViewModels;

namespace SharpSchedule.State.Navigators
{
  public class Navigator : ObservableObject, INavigator
  {
    private ViewModelBase _currentVM;

    public ViewModelBase CurrentVM
    {
      get
      {
        return _currentVM;
      }
      set
      {
        _currentVM = value;
        OnPropChanged(nameof(CurrentVM));
      }
    }
  }
}
