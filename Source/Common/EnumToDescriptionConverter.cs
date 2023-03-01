using Avalonia.Data.Converters;
using System;
using System.ComponentModel;
using System.Globalization;

namespace BugFablesSaveEditor
{
  public class EnumToDescriptionConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value == null)
        return "";

      string enumValueName = "";
      if (value is int)
      {
        string[] values = Enum.GetNames((Type)parameter);
        if ((int)value >= values.Length - 1)
          return "UNUSED " + (int)value;
        else
          enumValueName = values[(int)value];
      }
      else
      {
        enumValueName = value.ToString();
      }

      var memberInfo = ((Type)parameter).GetMember(enumValueName);
      if (memberInfo.Length > 0)
      {
        var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (attrs.Length > 0)
          return ((DescriptionAttribute)attrs[0]).Description;
      }
      return value.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotSupportedException();
    }
  }
}
