using System.Windows;
using SharpSchedule.WPF;
using SharpSchedule.ViewModels;

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
      window.DataContext = new MainVM();
      window.Show();

      base.OnStartup(e);
    }
  }
}
