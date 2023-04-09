using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using BugFablesSaveEditor.Models;
using DynamicData;
using DynamicData.Binding;

namespace BugFablesSaveEditor;

public static class Utils
{
  public static Window MainWindow
  {
    get
    {
      var lifetime = Application.Current?.ApplicationLifetime!;
      if (lifetime is IClassicDesktopStyleApplicationLifetime)
        return ((IClassicDesktopStyleApplicationLifetime)lifetime).MainWindow!;
      // TODO: Figure out how to do dialogs on browser
      // if (lifetime is ISingleViewApplicationLifetime singleViewPlatform)
      //   return ((ISingleViewApplicationLifetime)lifetime).MainView;
      return null!;
    }
  }
}
