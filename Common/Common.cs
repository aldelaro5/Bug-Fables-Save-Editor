using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor
{
  public static class Common
  {
    public const string FieldSeparator = ",";
    public const string ElementSeparator = "@";
    public static Window MainWindow { get => ((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).MainWindow; }
  }
}
