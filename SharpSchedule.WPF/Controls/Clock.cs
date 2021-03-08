using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SharpSchedule.Controls
{
  public class Clock : Control
  {
    public static readonly DependencyProperty TimeProperty = DependencyProperty.Register
      (nameof(Time), typeof(DateTime), typeof(Clock),
        new PropertyMetadata(DateTime.Now, TimePropertyChanged));

    private static void TimePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is Clock)
      {
        Clock clock = d as Clock;
        clock.RaiseEvent(new RoutedPropertyChangedEventArgs<DateTime>
                                    ((DateTime)e.OldValue, (DateTime)e.NewValue, TimeChangedEvent));
      }
    }

    public static readonly RoutedEvent TimeChangedEvent = EventManager.RegisterRoutedEvent
      (nameof(TimeChanged), RoutingStrategy.Bubble, typeof
                                            (RoutedPropertyChangedEventHandler<DateTime>), typeof(Clock));

    public DateTime Time
    {
      get { return (DateTime)GetValue(TimeProperty); }
      set { SetValue(TimeProperty, value); }
    }

    public event RoutedPropertyChangedEventHandler<DateTime> TimeChanged
    {
      add
      {
        AddHandler(TimeChangedEvent, value);
      }
      remove
      {
        RemoveHandler(TimeChangedEvent, value);
      }
    }

    public override void OnApplyTemplate()
    {
      OnTimeChanged(DateTime.Now);

      DispatcherTimer timer = new DispatcherTimer();
      timer.Interval = new TimeSpan(0, 0, 1);
      timer.Tick += (s, e) => OnTimeChanged(DateTime.Now);
      timer.Start();

      base.OnApplyTemplate();
    }

    protected virtual void OnTimeChanged(DateTime newTime)
    {
      Time = newTime;
    }
  }
}
