using System.Windows.Input;
using SharpSchedule.ViewModels;

namespace SharpSchedule.State.Navigators
{
  /// <summary>
  /// Enum of All Views to match VMs too
  /// </summary>
  public enum ViewType
  {
    Home,
    Customers,
    Appointments,
    Addresses
  }

  /// <summary>
  /// Holds Current View Model that the User has Navigated too
  /// </summary>
  public interface INavigator
  {
    /// <summary>
    /// Current View Model of the Current View
    /// </summary>
    ViewModelBase CurrentVM { get; set; }
    /// <summary>
    /// Command to update the Current View Model
    /// </summary>
    ICommand UpdateCurrentVM { get; }
  }
}
