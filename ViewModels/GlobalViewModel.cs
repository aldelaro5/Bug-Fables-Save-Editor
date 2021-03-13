﻿using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave.Sections;

namespace BugFablesSaveEditor.ViewModels
{
  public class GlobalViewModel : ViewModelBase
  {
    private SaveData _saveData;
    public SaveData SaveData
    {
      get
      {
        return _saveData;
      }
      set
      {
        _saveData = value;
        this.RaisePropertyChanged();
      }
    }

    public string[] Areas
    {
      get
      {
        return Common.GetEnumDescriptions<Area>();
      }
    }

    public string[] Maps
    {
      get
      {
        return Common.GetEnumDescriptions<Map>();
      }
    }

    public string[] SaveProgressIcons
    {
      get
      {
        return Common.GetEnumDescriptions<SaveProgressIcon>();
      }
    }

    public GlobalViewModel(SaveData saveData)
    {
      SaveData = saveData;
    }

    public ReactiveCommand<Unit, Unit> CmdNewFile
    {
      get => ReactiveCommand.Create(() =>
      {
        int vgfr = 0;
      });
    }
  }
}
