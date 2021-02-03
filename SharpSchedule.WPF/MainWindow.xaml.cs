using System.Windows;

namespace SharpSchedule.WPF
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow(object dataContext)
    {
      InitializeComponent();
      DataContext = dataContext;
    }
  }
}
