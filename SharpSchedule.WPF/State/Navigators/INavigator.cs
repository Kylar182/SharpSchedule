using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SharpSchedule.ViewModels;

namespace SharpSchedule.State.Navigators
{
  public enum ViewType
  {
    Home,
    Customers,
    Appointments
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
