using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace BugFablesSaveEditor.Utils;

public enum ReorderDirection
{
  Up,
  Down
}

public static class Common
{
  public const string FieldSeparator = ",";
  public const string ElementSeparator = "@";

  public static Window MainWindow =>
    ((IClassicDesktopStyleApplicationLifetime)Application.Current?.ApplicationLifetime!)
    .MainWindow!;


  public static string[] GetEnumDescriptions(this Type type)
  {
    if (!type.IsEnum)
      return Array.Empty<string>();

    string[] values = Enum.GetNames(type);
    string[] descriptions = new string[values.Length];
    for (int i = 0; i < values.Length; i++)
    {
      string name = values[i];
      MemberInfo[] memberInfo = type.GetMember(name);
      if (memberInfo.Length > 0)
      {
        object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (attrs.Length > 0)
        {
          descriptions[i] = ((DescriptionAttribute)attrs[0]).Description;
          continue;
        }
      }

      descriptions[i] = values[i];
    }

    return descriptions;
  }

  public static string[] GetEnumDescriptions<T>()
    where T : struct, Enum
  {
    Type type = typeof(T);
    List<T> values = Enum.GetValues<T>().ToList();
    values.Remove(values.Last());
    string[] descriptions = new string[values.Count];
    for (int i = 0; i < values.Count; i++)
    {
      MemberInfo[] memberInfo = type.GetMember(values[i].ToString());
      if (memberInfo.Length > 0)
      {
        object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (attrs.Length > 0)
        {
          descriptions[i] = ((DescriptionAttribute)attrs[0]).Description;
          continue;
        }
      }

      descriptions[i] = values[i].ToString();
    }

    return descriptions;
  }
}
