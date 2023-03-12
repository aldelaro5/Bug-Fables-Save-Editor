using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Avalonia.Data.Converters;

namespace BugFablesSaveEditor.Enums;

public class EnumToDescriptionConverter : IValueConverter
{
  public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if (value is null || parameter is null)
      return "";

    string enumValueName;
    if (value is int i)
    {
      string[] values = Enum.GetNames((Type)parameter);
      if (i >= values.Length - 1)
      {
        return "UNUSED " + i;
      }

      enumValueName = values[i];
    }
    else
    {
      enumValueName = value.ToString() ?? string.Empty;
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

    return value.ToString() ?? string.Empty;
  }

  public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    throw new NotSupportedException();
  }
}
