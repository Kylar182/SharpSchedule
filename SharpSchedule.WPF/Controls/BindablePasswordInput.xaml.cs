using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SharpSchedule.Controls
{
  /// <summary>
  /// Interaction logic for BindablePasswordInput.xaml
  /// </summary>
  public partial class BindablePasswordInput : UserControl
  {
    public string Input
    {
      get { return (string)GetValue(InputProperty); }
      set { SetValue(InputProperty, value); }
    }

    public static readonly DependencyProperty InputProperty =
        DependencyProperty.Register(nameof(Input), typeof(string), typeof(BindablePasswordInput),
          new PropertyMetadata(string.Empty));

    public string HelperText
    {
      get { return (string)GetValue(HelperTextProperty); }
      set { SetValue(HelperTextProperty, value); }
    }

    public static readonly DependencyProperty HelperTextProperty =
        DependencyProperty.Register(nameof(HelperText), typeof(string), typeof(BindablePasswordInput),
          new PropertyMetadata(string.Empty, HelperTextPropChanged));

    private static void HelperTextPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is BindablePasswordInput helperText)
        helperText.UpdateHelperText();
    }

    public bool Valid
    {
      get { return (bool)GetValue(ValidProperty); }
      set { SetValue(ValidProperty, value); }
    }

    public static readonly DependencyProperty ValidProperty =
        DependencyProperty.Register(nameof(Valid), typeof(bool), typeof(BindablePasswordInput),
          new PropertyMetadata(false, ValidPropChanged));

    private static void ValidPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is BindablePasswordInput helperText)
        helperText.UpdateValidColor();
    }

    public BindablePasswordInput()
    {
      InitializeComponent();
      Valid = false;
      UpdateValidColor();
    }

    private void PasswordInput_PasswordChanged(object sender, RoutedEventArgs e)
    {
      Input = passwordInput.Password;
    }

    private void UpdateHelperText()
    {
      helperText.Text = HelperText;
    }

    private void UpdateValidColor()
    {
      helperText.Foreground = Valid ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF002f51")) :
        new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF840028"));
    }
  }
}
