using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Avalonia.Data.Converters;

namespace BugFablesSaveEditor;

public class EnumToDescriptionConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value == null)
    {
      return "";
    }

    string enumValueName = "";
    if (value is int)
    {
      string[] values = Enum.GetNames((Type)parameter);
      if ((int)value >= values.Length - 1)
      {
        return "UNUSED " + (int)value;
      }

      enumValueName = values[(int)value];
    }
    else
    {
      enumValueName = value.ToString();
    }

    MemberInfo[] memberInfo = ((Type)parameter).GetMember(enumValueName);
    if (memberInfo.Length > 0)
    {
      object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
      if (attrs.Length > 0)
      {
        return ((DescriptionAttribute)attrs[0]).Description;
      }
    }

    return value.ToString();
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotSupportedException();
  }
}
