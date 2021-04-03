using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using BugFablesSaveEditor.Views;
using Common.MessageBox.Enums;
using System;
using System.ComponentModel;
using System.Linq;

namespace BugFablesSaveEditor
{
  public enum ReorderDirection
  {
    Up,
    Down
  }

  public static class CommonUtils
  {
    public const string FieldSeparator = ",";
    public const string ElementSeparator = "@";

    public static Window MainWindow { get => ((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).MainWindow; }

    public static MessageBoxView GetMessageBox(string title, string text, ButtonEnum buttons, Icon icon)
    {
      MessageBoxView view = new MessageBoxView(title, text, buttons, icon);

      return view;
    }

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
