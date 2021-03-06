using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ReactiveUI;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using Avalonia.Controls;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;
using System.Linq;

namespace BugFablesSaveEditor.ViewModels
{
  public class MainWindowViewModel : ViewModelBase
  {
    public string Greeting => "Welcome to Avalonia!";

    public MainWindowViewModel()
    {
    }
  }
}
