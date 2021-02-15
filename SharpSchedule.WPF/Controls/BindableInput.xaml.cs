using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SharpSchedule.Controls
{
  /// <summary>
  /// Interaction logic for BindableInput.xaml
  /// </summary>
  public partial class BindableInput : UserControl
  {
    public string Input
    {
      get { return (string)GetValue(InputProperty); }
      set { SetValue(InputProperty, value); }
    }

    public static readonly DependencyProperty InputProperty =
        DependencyProperty.Register(nameof(Input), typeof(string), typeof(BindableInput), 
          new PropertyMetadata(string.Empty, InputPropChanged));

    private static void InputPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is BindableInput inputBind)
        inputBind.UpdateInput();
    }

    public string HelperText
    {
      get { return (string)GetValue(HelperTextProperty); }
      set { SetValue(HelperTextProperty, value); }
    }

    public static readonly DependencyProperty HelperTextProperty =
        DependencyProperty.Register(nameof(HelperText), typeof(string), typeof(BindableInput), 
          new PropertyMetadata(string.Empty, HelperTextPropChanged));

    private static void HelperTextPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is BindableInput helperText)
        helperText.UpdateHelperText();
    }

    public BindableInput()
    {
      InitializeComponent();
    }

    private void InputBind_SourceUpdated(object sender, DataTransferEventArgs e)
    {
      Input = inputBind.Text;
    }

    private void UpdateInput()
    {
      inputBind.Text = Input;
    }

    private void UpdateHelperText()
    {
      helperText.Text = HelperText;
    }
  }
}
