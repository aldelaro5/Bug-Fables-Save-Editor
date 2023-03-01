using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using BugFablesSaveEditor.Views;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.Views;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace BugFablesSaveEditor
{
  public enum ReorderDirection
  {
    Up,
    Down
  }

  public static class Common
  {
    public const string FieldSeparator = ",";
    public const string ElementSeparator = "@";

    public static Window MainWindow { get => ((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).MainWindow; }

    public static MessageBoxView GetMessageBox(string title, string text, ButtonEnum buttons, Icon icon)
    {
      var msg = MessageBoxManager.GetMessageBoxStandardWindow(title, text, buttons, icon);
      Type type = msg.GetType();
      FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
      MsBoxStandardWindow standardMsgBox = (MsBoxStandardWindow)fields[0].GetValue(msg);

      MessageBoxView view = new MessageBoxView(title, text, buttons, icon, standardMsgBox);

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
