using System.Windows;
using System.Windows.Controls.Primitives;

namespace SharpSchedule.Controls
{
  public class ToggleBase : ToggleButton
  {
    static ToggleBase()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleBase),
                                new FrameworkPropertyMetadata(typeof(ToggleBase)));


    }
  }
}
