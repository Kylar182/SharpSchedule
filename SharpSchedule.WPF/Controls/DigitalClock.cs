using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SharpSchedule.Controls
{
  [TemplatePart(Name = "PART_Hour", Type = typeof(TextBlock))]
  [TemplatePart(Name = "PART_Colon", Type = typeof(UIElement))]
  [TemplatePart(Name = "PART_Minute", Type = typeof(TextBlock))]
  public class DigitalClock : Clock
  {
    private TextBlock minute;
    private UIElement colon;
    private TextBlock hour;

    public static readonly DependencyProperty ColonBlinkProperty =
      DependencyProperty.Register(nameof(ColonBlink), typeof(bool), typeof(DigitalClock),
        new PropertyMetadata(true));

    public bool ColonBlink
    {
      get { return (bool)GetValue(ColonBlinkProperty); }
      set { SetValue(ColonBlinkProperty, value); }
    }

    public bool Alarm
    {
      get { return (bool)GetValue(AlarmProperty); }
      set { SetValue(AlarmProperty, value); }
    }

    public static readonly DependencyProperty AlarmProperty =
        DependencyProperty.Register(nameof(Alarm), typeof(bool), typeof(DigitalClock),
          new PropertyMetadata(false, AlarmPropChanged));

    private static void AlarmPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is DigitalClock alarmBind)
        alarmBind.AlarmColor();
    }

    static DigitalClock()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(DigitalClock),
        new FrameworkPropertyMetadata(typeof(DigitalClock)));
    }

    public override void OnApplyTemplate()
    {
      Alarm = false;
      AlarmColor();

      hour = Template.FindName("PART_Hour", this) as TextBlock;
      colon = Template.FindName("PART_Colon", this) as UIElement;
      minute = Template.FindName("PART_Minute", this) as TextBlock;

      base.OnApplyTemplate();
    }

    protected override void OnTimeChanged(DateTime newTime)
    {
      if (colon != null)
      {
        if (ColonBlink)
        {
          colon.Visibility = colon.IsVisible ? Visibility.Hidden : Visibility.Visible;
        }
        else
        {
          colon.Visibility = Visibility.Visible;
        }
      }

      if (hour != null)
      {
        int iHour = Time.Hour > 12 ? Time.Hour - 12 : Time.Hour;

        if (iHour == 0)
          hour.Text = 12.ToString();
        else
          hour.Text = $"{iHour:0}";
      }

      if (minute != null)
      {
        string meridiem = Time.Hour > 11 ? "pm" : "am";

        minute.Text = $"{Time.Minute:00} {meridiem}";
      }

      base.OnTimeChanged(newTime);
    }

    private void AlarmColor()
    {
      if (hour != null)
      {
        hour.Foreground = Alarm ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF002F51")) :
        new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF840028"));
      }

      if (minute != null)
      {
        minute.Foreground = Alarm ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF002F51")) :
        new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF840028"));
      }
    }
  }
}
