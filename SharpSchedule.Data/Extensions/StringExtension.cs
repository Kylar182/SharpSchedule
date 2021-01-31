using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SharpSchedule.Data.Extensions
{
  /// <summary>
  /// String Extension Class Methods
  /// </summary>
  public static class StringExtension
  {
    /// <summary>
    /// Trims the String and returns Null if it's empty space
    /// </summary>
    public static string TrimFix(this string rawString)
    {
      if (!string.IsNullOrWhiteSpace(rawString))
      {
        return rawString.Trim();
      }
      return null;
    }

    /// <summary>
    /// Pluralizes the Current string
    /// </summary>
    public static string ToPlural(this string str)
    {
      if (str.IsEmpty())
        return str;
      else if (str.EndsWith("e"))
        return str + "s";
      else if (str.Substring(str.Length - 2) == "es")
        return str;
      else if (!str.EndsWith("s"))
        return str + "s";
      else
        return str + "es";
    }

    /// <summary>
    /// If string is not null/empty and longer than specified length, 
    /// it will be cut and returned as the specified length
    /// If it's not as long as the specified length it's returned as is
    /// If it's null / empty, '...' is returned
    /// </summary>
    public static string FriendlyLength(this string longString, int length)
    {
      if (!longString.IsEmpty())
      {
        if (longString.Length > length)
          return longString.Substring(0, length);
        else
          return longString;
      }
      else
        return "...";
    }

    /// If string is not null/empty and longer than specified length, it will 
    /// be cut and returned as the specified length using the end of the String
    /// If it's not as long as the specified length it's returned as is
    /// If it's null / empty, '...' is returned
    /// </summary>
    public static string FriendlyEnd(this string longString, int length)
    {
      if (!longString.IsEmpty())
      {
        if (longString.Length > length)
          return longString.Substring((longString.Length - length), length);
        else
          return longString;
      }
      else
        return "...";
    }

    /// <summary>
    /// Gets the number portion of the string and adds 1 to it
    /// </summary>
    public static string IncrementNumbers(this string numString)
    {
      return numString.IncrementNumbers(1);
    }

    /// <summary>
    /// Gets the number portion of the string and adds the increment to it
    /// </summary>
    public static string IncrementNumbers(this string numString, int increment)
    {
      if (numString.IsEmpty())
        return numString;
      else if (!numString.Where(Char.IsDigit).Any())
        return numString;
      else
      {
        int remove = 0;
        string prefix = numString.TrimFix();
        prefix = Regex.Match(numString, "^\\D+").Value;
        string[] split = numString.Split("-");
        string number = Regex.Replace(split[0], "^\\D+", "");
        long i = long.Parse(number) + increment;
        if (split.Length > 1)
        {
          foreach (var s in split)
          {
            if (s != split[0])
              remove += s.Length;
          }
        }
        return prefix + i.ToString($"D{(split.Length < 1 ? numString.TrimFix().Length : numString.TrimFix().Length - (split.Length - 1)) - prefix.Length - remove}");
      }
    }

    /// <summary>
    /// Gets the number portion of the string and adds concatenates the max portion
    /// </summary>
    public static string SetMax(this string numString, int max)
    {
      if (numString.IsEmpty())
        return numString;
      else if (!numString.Where(Char.IsDigit).Any())
        return numString;
      else
      {
        int remove = 0;
        string prefix = numString.TrimFix();
        prefix = Regex.Match(numString, "^\\D+").Value;
        string[] split = numString.Split("-");
        string number = Regex.Replace(split[0], "^\\D+", "");
        long i = long.Parse(number);
        if (split.Length > 1)
        {
          foreach (var s in split)
          {
            if (s != split[0])
              remove += s.Length;
          }
        }
        string iString = i.ToString();

        int D = max - iString.Length - remove - prefix.Length;
        return prefix + i.ToString($"D{D}");
      }
    }

    /// <summary>
    /// Gets the number portion of the string and subtracts 1 from it
    /// </summary>
    public static string DecrementNumbers(this string numString)
    {
      if (numString.IsEmpty())
        return numString;
      else if (!numString.Where(Char.IsDigit).Any())
        return numString;
      else
      {
        int remove = 0;
        string prefix = numString.TrimFix();
        prefix = Regex.Match(numString, "^\\D+").Value;
        string[] split = numString.Split("-");
        string number = Regex.Replace(split[0], "^\\D+", "");
        long i = long.Parse(number) - 1;
        if (split.Length > 1)
        {
          foreach (var s in split)
          {
            if (s != split[0])
              remove += s.Length;
          }
        }

        return prefix + i.ToString($"D{(split.Length < 1 ? numString.TrimFix().Length : numString.TrimFix().Length - (split.Length - 1)) - prefix.Length - remove}");
      }
    }

    /// <summary>
    /// Shortented IsNullOrWhiteSpace 
    /// </summary>
    public static bool IsEmpty(this string str)
    {
      if (str.TrimFix() == null)
        return true;

      return false;
    }

    /// <summary>
    /// Shortented IsNullOrWhiteSpace 
    /// </summary>
    public static bool MinMax(this string str, int min, int? max)
    {
      if (str.IsEmpty() && (min > 0))
        return false;
      else if (min == 0 && str.IsEmpty())
        return true;
      else if (str.TrimFix().Length >= min)
      {
        if (max == null)
          return true;

        return str.TrimFix().Length < max.Value;
      }

      return false;
    }

    /// <summary>
    /// Regex Matching (Typically for Validation)
    /// </summary>
    public static bool GetMatch(this string input, Regex regex)
    {
      Match match = regex.Match(input);
      if (match.Success)
        return true;

      return false;
    }

    /// <summary>
    /// Gets the number portion of the string and Returns it
    /// </summary>
    public static long GetNum(this string numString)
    {
      if (numString.IsEmpty())
        return 0;
      else if (!numString.Where(Char.IsDigit).Any())
        return 0;
      else
      {
        string[] split = numString.TrimFix().Split("-");
        string number = Regex.Replace(split[0], "^\\D+", "");
        return long.Parse(number);
      }
    }
  }
}
