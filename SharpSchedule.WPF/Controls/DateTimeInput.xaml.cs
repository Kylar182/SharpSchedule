using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SharpSchedule.Controls
{
  /// <summary>
  /// Interaction logic for DateTimeInput.xaml
  /// </summary>
  public partial class DateTimeInput : UserControl
  {
    public DateTime? Input
    {
      get { return (DateTime?)GetValue(InputProperty); }
      set { SetValue(InputProperty, value); }
    }

    public static readonly DependencyProperty InputProperty =
        DependencyProperty.Register(nameof(Input), typeof(DateTime?), typeof(DateTimeInput),
          new PropertyMetadata(null, InputPropChanged));

    private static void InputPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is DateTimeInput inputDateBind)
        inputDateBind.UpdateInput();
    }

    public string HelperText
    {
      get { return (string)GetValue(HelperTextProperty); }
      set { SetValue(HelperTextProperty, value); }
    }

    public static readonly DependencyProperty HelperTextProperty =
        DependencyProperty.Register(nameof(HelperText), typeof(string), typeof(DateTimeInput),
          new PropertyMetadata(string.Empty, HelperTextPropChanged));

    private static void HelperTextPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is DateTimeInput inputDateBind)
        inputDateBind.UpdateHelperText();
    }

    public bool Valid
    {
      get { return (bool)GetValue(ValidProperty); }
      set { SetValue(ValidProperty, value); }
    }

    public static readonly DependencyProperty ValidProperty =
        DependencyProperty.Register(nameof(Valid), typeof(bool), typeof(DateTimeInput),
          new PropertyMetadata(false, ValidPropChanged));

    private static void ValidPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is DateTimeInput inputDateBind)
        inputDateBind.UpdateValidColor();
    }

    public bool Enabled
    {
      get { return (bool)GetValue(DisabledProperty); }
      set { SetValue(DisabledProperty, value); }
    }

    public static readonly DependencyProperty DisabledProperty =
        DependencyProperty.Register(nameof(Enabled), typeof(bool), typeof(DateTimeInput),
          new PropertyMetadata(true, EnabledPropChanged));

    private static void EnabledPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is DateTimeInput inputDateBind)
        inputDateBind.DisableInput();
    }

    public DateTimeInput()
    {
      InitializeComponent();

      int iHour = 0;
      while (iHour < 24)
      {
        int hour = iHour > 12 ? iHour - 12 : iHour;
        inputHourBind.Items.Add($"{hour:00}");
        iHour++;
      }

      int minute = 0;
      while (minute < 60)
      {
        inputMinuteBind.Items.Add($"{minute:00}");
        minute++;
      }
    }

    private void inputDateBind_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
      if (inputDateBind.SelectedDate.HasValue)
      {
        DateTime? date = Input;
        if (date.HasValue && date.Value.Date != inputDateBind.SelectedDate.Value.Date)
        {
          date = inputDateBind.SelectedDate.Value.Date;

          date = date.Value.AddHours(Input.Value.Hour);
          date = date.Value.AddMinutes(Input.Value.Minute);

          Input = date;
        }
      }
      else
        Input = null;
    }

    private void inputHourBind_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (Input.HasValue)
      {
        DateTime date = Input.Value.Date;

        if (inputHourBind.SelectedIndex >= 0)
          date = date.AddHours(inputHourBind.SelectedIndex);

        date = date.AddMinutes(Input.Value.Minute);

        Input = date;
      }
    }

    private void inputMinuteBind_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (Input.HasValue)
      {
        DateTime date = Input.Value.Date;

        date = date.AddHours(Input.Value.Hour);

        if (inputMinuteBind.SelectedIndex >= 0)
          date = date.AddMinutes(inputMinuteBind.SelectedIndex);

        Input = date;
      }
    }

    private void UpdateInput()
    {
      if (Input.HasValue)
      {
        inputDateBind.Text = Input.Value.ToShortDateString();
        inputHourBind.SelectedIndex = Input.Value.Hour;
        inputMinuteBind.SelectedIndex = Input.Value.Minute;
      }
      else
        inputDateBind.Text = string.Empty;
    }

    private void UpdateHelperText()
    {
      helperText.Text = HelperText;
    }

    private void UpdateValidColor()
    {
      helperText.Foreground = Valid ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF002F51")) :
        new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF840028"));
    }

    private void DisableInput()
    {
      inputDateBind.IsEnabled = Enabled;
    }
  }
}
