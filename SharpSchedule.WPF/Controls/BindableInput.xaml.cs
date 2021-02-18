﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

    public bool Valid
    {
      get { return (bool)GetValue(ValidProperty); }
      set { SetValue(ValidProperty, value); }
    }

    public static readonly DependencyProperty ValidProperty =
        DependencyProperty.Register(nameof(Valid), typeof(bool), typeof(BindableInput),
          new PropertyMetadata(false, ValidPropChanged));

    private static void ValidPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is BindableInput helperText)
        helperText.UpdateValidColor();
    }

    public BindableInput()
    {
      InitializeComponent();
    }

    private void InputBind_TextChanged(object sender, TextChangedEventArgs e)
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

    private void UpdateValidColor()
    {
      helperText.Foreground = Valid ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF002f51")) :
        new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF840028"));
    }
  }
}
