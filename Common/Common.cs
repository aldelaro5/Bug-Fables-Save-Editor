using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using BugFablesSaveEditor.BugFablesSave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BugFablesSaveEditor
{
  public static class Common
  {
    public const string FieldSeparator = ",";
    public const string ElementSeparator = "@";

    public static Window MainWindow { get => ((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).MainWindow; }

    public static string[] GetEnumDescriptions<T>()
      where T : struct, Enum
    {
      var type = typeof(T);
      var values = Enum.GetValues<T>().ToList();
      values.Remove(values.Last());
      string[] descriptions = new string[values.Count];
      for (int i = 0; i < values.Count; i++)
      {
        var memberInfo = type.GetMember(values[i].ToString());
        if (memberInfo.Length > 0)
        {
          var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
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
}
