using SharpSchedule.ViewModels;

namespace SharpSchedule.State.Navigators
{
  /// <summary>
  /// Enum of All Views to match VMs too
  /// </summary>
  public enum ViewType
  {
    Login,
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
  }
}
