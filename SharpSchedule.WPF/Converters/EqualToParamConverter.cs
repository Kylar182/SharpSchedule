﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace SharpSchedule.Converters
{
  /// <summary>
  /// Converter to test if value as a String 
  /// is the same as the Parameter as a String
  /// </summary>
  public class EqualToParamConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value != null && parameter != null)
        return value.ToString() == parameter.ToString();
      else
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
