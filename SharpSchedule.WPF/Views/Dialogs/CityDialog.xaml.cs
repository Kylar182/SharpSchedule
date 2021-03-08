using System.Windows;

namespace SharpSchedule.Views.Dialogs
{
  /// <summary>
  /// Interaction logic for CityDialog.xaml
  /// </summary>
  public partial class CityDialog : Window
  {
    public CityDialog()
    {
      InitializeComponent();
      WindowStartupLocation = WindowStartupLocation.CenterOwner;
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
