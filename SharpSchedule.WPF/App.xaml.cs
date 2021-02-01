using System.Windows;
using SharpSchedule.WPF;

namespace SharpSchedule
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      Window window = new MainWindow();
      window.Show();

      base.OnStartup(e);
    }
  }
}
