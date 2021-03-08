using System;
using System.Globalization;
using System.Windows.Data;

namespace SharpSchedule.Converters
{
  public class DivisionConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return double.TryParse(value.ToString(), out double numerator) &&
          double.TryParse(parameter.ToString(), out double denominator) &&
          denominator != 0 ?
          numerator / denominator : 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
