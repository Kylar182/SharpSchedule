using System.Windows;
using System.Windows.Controls;

namespace SharpSchedule.Controls
{
  /// <summary>
  /// Interaction logic for ToggleSwitch.xaml
  /// </summary>
  public partial class ToggleSwitch : UserControl
  {
    public bool Input
    {
      get { return (bool)GetValue(InputProperty); }
      set { SetValue(InputProperty, value); }
    }

    public static readonly DependencyProperty InputProperty =
        DependencyProperty.Register(nameof(Input), typeof(bool), typeof(ToggleSwitch),
          new PropertyMetadata(true, InputPropChanged));

    private static void InputPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is ToggleSwitch inputBind)
        inputBind.UpdateInput();
    }

    public string HelperText
    {
      get { return (string)GetValue(HelperTextProperty); }
      set { SetValue(HelperTextProperty, value); }
    }

    public static readonly DependencyProperty HelperTextProperty =
        DependencyProperty.Register(nameof(HelperText), typeof(string), typeof(ToggleSwitch),
          new PropertyMetadata(string.Empty, HelperTextPropChanged));

    private static void HelperTextPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is ToggleSwitch inputBind)
        inputBind.UpdateHelperText();
    }

    public ToggleSwitch()
    {
      InitializeComponent();
    }

    private void UpdateInput()
    {
      inputBind.IsChecked = Input;
    }

    private void UpdateHelperText()
    {
      helperText.Text = HelperText;
    }
  }
}
