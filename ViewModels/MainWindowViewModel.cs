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
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.BugFablesSave.Sections;
using BugFablesSaveEditor.BugFablesEnums;

namespace BugFablesSaveEditor.ViewModels
{
  public class MainWindowViewModel : ViewModelBase
  {
    private string _currentFilePath = "No save file, open an existing file or create a new one";
    public string CurrentFilePath
    {
      get { return _currentFilePath; }
      set { _currentFilePath = value; this.RaisePropertyChanged(); }
    }

    private SaveData _saveData;
    public SaveData SaveData
    {
      get { return _saveData; }
      set { _saveData = value; this.RaisePropertyChanged(); }
    }

    private bool _saveInUse = false;
    public bool SaveInUse
    {
      get { return _saveInUse; }
      set { _saveInUse = value; this.RaisePropertyChanged(); }
    }

    public ReactiveCommand<Unit, Unit> CmdNewFile
    {
      get => ReactiveCommand.Create(() =>
      {
        //SaveData = new SaveData();
        CurrentFilePath = "New file being created, save it to store it";
        SaveInUse = true;
      });
    }

    public ReactiveCommand<Unit, Unit> CmdOpenFile
    {
      get => ReactiveCommand.CreateFromTask(async () =>
      {
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.Title = "Select a Bug Fables save file";
        dlg.Filters.Add(new FileDialogFilter() { Name = "Bug Fables save (.dat)", Extensions = { "dat" } });
        dlg.AllowMultiple = false;
        string[] filePaths = await dlg.ShowAsync(Common.MainWindow);
        if (filePaths.Length == 1)
        {
          try
          {
            SaveData.LoadFromFile(filePaths.First());
            CurrentFilePath = filePaths.First();
            SaveInUse = true;
          }
          catch (Exception ex)
          {
            //SaveData = new SaveData();
            var msg = MessageBoxManager.GetMessageBoxStandardWindow("Error opening save file",
                        "An error occured while opening the save file: " + ex.Message, ButtonEnum.Ok, Icon.Error);
            await msg.ShowDialog(Common.MainWindow);
          }
        }
      });
    }

    public ReactiveCommand<Unit, Unit> CmdSaveFile
    {
      get => ReactiveCommand.CreateFromTask(async () =>
      {
        SaveFileDialog dlg = new SaveFileDialog();
        dlg.Title = "Select the location to save the file";
        dlg.Filters.Add(new FileDialogFilter() { Name = "Bug Fables save (.dat)", Extensions = { "dat" } });
        dlg.DefaultExtension = "dat";
        string filePath = await dlg.ShowAsync(Common.MainWindow);
        if (!string.IsNullOrEmpty(filePath))
        {
          try
          {
            SaveData.SaveToFile(filePath);
            CurrentFilePath = filePath;
          }
          catch (Exception ex)
          {
            var msg = MessageBoxManager.GetMessageBoxStandardWindow("Error opening save file",
                        "An error occured while saving the save file: " + ex.Message, ButtonEnum.Ok, Icon.Error);
            await msg.ShowDialog(Common.MainWindow);
          }
        }
      }, this.WhenAnyValue(x => x.SaveInUse));
    }

    public ReactiveCommand<Unit, Unit> CmdExit
    {
      get => ReactiveCommand.Create(() =>
      {
        Common.MainWindow.Close();
      });
    }

    private GlobalViewModel _globalViewModel;
    public GlobalViewModel GlobalViewModel
    {
      get { return _globalViewModel; }
      set { _globalViewModel = value; this.RaisePropertyChanged(); }
    }

    private PartyViewModel _partyViewModel;
    public PartyViewModel PartyViewModel
    {
      get { return _partyViewModel; }
      set { _partyViewModel = value; this.RaisePropertyChanged(); }
    }

    private StatsViewModel _statsViewModel;
    public StatsViewModel StatsViewModel
    {
      get { return _statsViewModel; }
      set { _statsViewModel = value; this.RaisePropertyChanged(); }
    }

    private QuestsViewModel _questsViewModel;
    public QuestsViewModel QuestsViewModel
    {
      get { return _questsViewModel; }
      set { _questsViewModel = value; this.RaisePropertyChanged(); }
    }

    private ItemsViewModel _itemsViewModel;
    public ItemsViewModel ItemsViewModel
    {
      get { return _itemsViewModel; }
      set { _itemsViewModel = value; this.RaisePropertyChanged(); }
    }

    private MedalsViewModel _medalsViewModel;
    public MedalsViewModel MedalsViewModel
    {
      get { return _medalsViewModel; }
      set { _medalsViewModel = value; this.RaisePropertyChanged(); }
    }

    private LibraryViewModel _libraryViewModel;
    public LibraryViewModel LibraryViewModel
    {
      get { return _libraryViewModel; }
      set { _libraryViewModel = value; this.RaisePropertyChanged(); }
    }

    private FlagsViewModel _flagsViewModel;
    public FlagsViewModel FlagsViewModel
    {
      get { return _flagsViewModel; }
      set { _flagsViewModel = value; this.RaisePropertyChanged(); }
    }

    private SongsViewModel _songsViewModel;
    public SongsViewModel SongsViewModel
    {
      get { return _songsViewModel; }
      set { _songsViewModel = value; this.RaisePropertyChanged(); }
    }

    public MainWindowViewModel()
    {
      SaveData saveData = new SaveData();
      SaveData = saveData;

      GlobalViewModel = new GlobalViewModel(saveData);
      PartyViewModel = new PartyViewModel(saveData);
      StatsViewModel = new StatsViewModel(saveData);
      QuestsViewModel = new QuestsViewModel(saveData);
      ItemsViewModel = new ItemsViewModel(saveData);
      MedalsViewModel = new MedalsViewModel(saveData);
      LibraryViewModel = new LibraryViewModel(saveData);
      FlagsViewModel = new FlagsViewModel(saveData);
      SongsViewModel = new SongsViewModel(saveData);
    }

    public MainWindowViewModel(SaveData saveData)
    {
      SaveData = saveData;
      GlobalViewModel = new GlobalViewModel(saveData);
      PartyViewModel = new PartyViewModel(saveData);
      StatsViewModel = new StatsViewModel(saveData);
      QuestsViewModel = new QuestsViewModel(saveData);
      ItemsViewModel = new ItemsViewModel(saveData);
      MedalsViewModel = new MedalsViewModel(saveData);
      LibraryViewModel = new LibraryViewModel(saveData);
      FlagsViewModel = new FlagsViewModel(saveData);
      SongsViewModel = new SongsViewModel(saveData);
    }
  }
}
