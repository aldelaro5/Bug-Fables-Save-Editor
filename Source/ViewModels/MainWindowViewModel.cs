using System;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.Views;
using MessageBox.Avalonia.Enums;
using ReactiveUI;

namespace BugFablesSaveEditor.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
  private CrystalBerriesViewModel _crystalBerriesViewModel;

  private string _currentFilePath = "No save file, open an existing file or create a new one";

  private FlagsViewModel _flagsViewModel;

  private GlobalViewModel _globalViewModel;

  private ItemsViewModel _itemsViewModel;

  private LibraryViewModel _libraryViewModel;

  private MedalsViewModel _medalsViewModel;

  private PartyViewModel _partyViewModel;

  private QuestsViewModel _questsViewModel;

  private SaveData _saveData;

  private bool _saveInUse;

  private SongsViewModel _songsViewModel;

  private StatsViewModel _statsViewModel;
  private bool fileSaved;

  public MainWindowViewModel()
  {
    SaveData saveData = new();
    SaveData = saveData;
    Initialise(saveData);
  }

  public MainWindowViewModel(SaveData saveData)
  {
    SaveData = saveData;
    Initialise(saveData);
  }

  public string CurrentFilePath
  {
    get => _currentFilePath;
    set
    {
      _currentFilePath = value;
      this.RaisePropertyChanged();
    }
  }

  public SaveData SaveData
  {
    get => _saveData;
    set
    {
      _saveData = value;
      this.RaisePropertyChanged();
    }
  }

  public bool SaveInUse
  {
    get => _saveInUse;
    set
    {
      _saveInUse = value;
      this.RaisePropertyChanged();
    }
  }

  public ReactiveCommand<Unit, Unit> CmdSaveFile { get; set; }

  public GlobalViewModel GlobalViewModel
  {
    get => _globalViewModel;
    set
    {
      _globalViewModel = value;
      this.RaisePropertyChanged();
    }
  }

  public PartyViewModel PartyViewModel
  {
    get => _partyViewModel;
    set
    {
      _partyViewModel = value;
      this.RaisePropertyChanged();
    }
  }

  public StatsViewModel StatsViewModel
  {
    get => _statsViewModel;
    set
    {
      _statsViewModel = value;
      this.RaisePropertyChanged();
    }
  }

  public QuestsViewModel QuestsViewModel
  {
    get => _questsViewModel;
    set
    {
      _questsViewModel = value;
      this.RaisePropertyChanged();
    }
  }

  public ItemsViewModel ItemsViewModel
  {
    get => _itemsViewModel;
    set
    {
      _itemsViewModel = value;
      this.RaisePropertyChanged();
    }
  }

  public MedalsViewModel MedalsViewModel
  {
    get => _medalsViewModel;
    set
    {
      _medalsViewModel = value;
      this.RaisePropertyChanged();
    }
  }

  public LibraryViewModel LibraryViewModel
  {
    get => _libraryViewModel;
    set
    {
      _libraryViewModel = value;
      this.RaisePropertyChanged();
    }
  }

  public FlagsViewModel FlagsViewModel
  {
    get => _flagsViewModel;
    set
    {
      _flagsViewModel = value;
      this.RaisePropertyChanged();
    }
  }

  public SongsViewModel SongsViewModel
  {
    get => _songsViewModel;
    set
    {
      _songsViewModel = value;
      this.RaisePropertyChanged();
    }
  }

  public CrystalBerriesViewModel CrystalBerriesViewModel
  {
    get => _crystalBerriesViewModel;
    set
    {
      _crystalBerriesViewModel = value;
      this.RaisePropertyChanged();
    }
  }

  public async void NewFile()
  {
    if (SaveInUse && !fileSaved)
    {
      MessageBoxView msg = Common.GetMessageBox("File in use", "An unsaed file is currently in use, " +
                                                               "creating a new file will loose all unsaved changes,\nare you sure you want to proceed?",
        ButtonEnum.YesNo, Icon.Warning);
      await msg.ShowDialog(Common.MainWindow);
      if (msg.ButtonResult == ButtonResult.No)
      {
        return;
      }
    }

    SaveData.ResetToDefault();
    CurrentFilePath = "New file being created, save it to store it";
    SaveInUse = true;
    fileSaved = false;
  }

  public async void OpenFile()
  {
    if (SaveInUse && !fileSaved)
    {
      MessageBoxView msg = Common.GetMessageBox("File in use", "An unsaed file is currently in use, " +
                                                               "opening a file will loose all unsaved changes,\nare you sure you want to proceed?",
        ButtonEnum.YesNo, Icon.Warning);
      await msg.ShowDialog(Common.MainWindow);
      if (msg.ButtonResult == ButtonResult.No)
      {
        return;
      }
    }

    OpenFileDialog dlg = new();
    dlg.Title = "Select a Bug Fables save file";
    dlg.Filters.Add(new FileDialogFilter { Name = "Bug Fables save (.dat)", Extensions = { "dat" } });
    dlg.AllowMultiple = false;
    string[] filePaths = await dlg.ShowAsync(Common.MainWindow);
    if (filePaths.Length == 1)
    {
      try
      {
        SaveData.ResetToDefault();
        SaveData.LoadFromFile(filePaths.First());
        CurrentFilePath = filePaths.First();
        SaveInUse = true;
        fileSaved = true;
      }
      catch (Exception ex)
      {
        SaveData.ResetToDefault();
        MessageBoxView msg = Common.GetMessageBox("Error opening save file",
          "An error occured while opening the save file: " + ex.Message, ButtonEnum.Ok, Icon.Error);
        await msg.ShowDialog(Common.MainWindow);
      }
    }
  }

  public void Exit()
  {
    Common.MainWindow.Close();
  }

  private void Initialise(SaveData saveData)
  {
    GlobalViewModel = new GlobalViewModel(saveData);
    PartyViewModel = new PartyViewModel(saveData);
    StatsViewModel = new StatsViewModel(saveData);
    QuestsViewModel = new QuestsViewModel(saveData);
    ItemsViewModel = new ItemsViewModel(saveData);
    MedalsViewModel = new MedalsViewModel(saveData);
    LibraryViewModel = new LibraryViewModel(saveData);
    FlagsViewModel = new FlagsViewModel(saveData);
    SongsViewModel = new SongsViewModel(saveData);
    CrystalBerriesViewModel = new CrystalBerriesViewModel(saveData);

    CmdSaveFile = ReactiveCommand.CreateFromTask(async () =>
    {
      SaveFileDialog dlg = new();
      dlg.Title = "Select the location to save the file";
      dlg.Filters.Add(new FileDialogFilter { Name = "Bug Fables save (.dat)", Extensions = { "dat" } });
      dlg.DefaultExtension = "dat";
      string filePath = await dlg.ShowAsync(Common.MainWindow);
      if (!string.IsNullOrEmpty(filePath))
      {
        try
        {
          SaveData.SaveToFile(filePath);
          CurrentFilePath = filePath;
          MessageBoxView msg = Common.GetMessageBox("File saved",
            "The file was saved successfully at " + CurrentFilePath,
            ButtonEnum.Ok, Icon.Warning);
          await msg.ShowDialog(Common.MainWindow);
          fileSaved = true;
        }
        catch (Exception ex)
        {
          MessageBoxView msg = Common.GetMessageBox("Error opening save file",
            "An error occured while saving the save file: " + ex.Message, ButtonEnum.Ok, Icon.Error);
          await msg.ShowDialog(Common.MainWindow);
        }
      }
    }, this.WhenAnyValue(x => x.SaveInUse));
  }
}
