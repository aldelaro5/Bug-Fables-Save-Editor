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

      var memberInfo = ((Type)parameter).GetMember(value.ToString());
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
