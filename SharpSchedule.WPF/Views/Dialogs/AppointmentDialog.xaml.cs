using System.Windows;

namespace SharpSchedule.Views.Dialogs
{
  /// <summary>
  /// Interaction logic for AppointmentDialog.xaml
  /// </summary>
  public partial class AppointmentDialog : Window
  {
    public AppointmentDialog()
    {
      InitializeComponent();
      WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }

    private void Dialog_True(object sender, RoutedEventArgs e)
    {
      DialogResult = true;
    }

    private void Dialog_False(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
    }
  }
}
