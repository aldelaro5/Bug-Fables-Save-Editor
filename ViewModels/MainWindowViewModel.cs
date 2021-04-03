using Avalonia.Controls;
using BugFablesSaveEditor.BugFablesSave;
using Common.MessageBox.Enums;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;

namespace BugFablesSaveEditor.ViewModels
{
  public class MainWindowViewModel : ViewModelBase
  {
    private bool fileSaved = false;

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

    private CrystalBerriesViewModel _crystalBerriesViewModel;
    public CrystalBerriesViewModel CrystalBerriesViewModel
    {
      get { return _crystalBerriesViewModel; }
      set { _crystalBerriesViewModel = value; this.RaisePropertyChanged(); }
    }

    public ReactiveCommand<Unit, Unit> CmdSaveFile { get; set; }

    public MainWindowViewModel()
    {
      SaveData saveData = new SaveData();
      SaveData = saveData;
      Initialise(saveData);
    }

    public MainWindowViewModel(SaveData saveData)
    {
      SaveData = saveData;
      Initialise(saveData);
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
        SaveFileDialog dlg = new SaveFileDialog();
        dlg.Title = "Select the location to save the file";
        dlg.Filters.Add(new FileDialogFilter() { Name = "Bug Fables save (.dat)", Extensions = { "dat" } });
        dlg.DefaultExtension = "dat";
        string filePath = await dlg.ShowAsync(CommonUtils.MainWindow);
        if (!string.IsNullOrEmpty(filePath))
        {
          try
          {
            SaveData.SaveToFile(filePath);
            CurrentFilePath = filePath;
            var msg = CommonUtils.GetMessageBox("File saved", "The file was saved successfully at " + CurrentFilePath,
                                           ButtonEnum.Ok, Icon.Info);
            await msg.ShowDialog(CommonUtils.MainWindow);
            fileSaved = true;
          }
          catch (Exception ex)
          {
            var msg = CommonUtils.GetMessageBox("Error opening save file",
                        "An error occured while saving the save file: " + ex.Message, ButtonEnum.Ok, Icon.Error);
            await msg.ShowDialog(CommonUtils.MainWindow);
          }
        }
      }, this.WhenAnyValue(x => x.SaveInUse));
    }



    public async void NewFile()
    {
      if (SaveInUse && !fileSaved)
      {
        var msg = CommonUtils.GetMessageBox("File in use", "An unsaved file is currently in use, " +
                  "creating a new file will loose all unsaved changes,\nare you sure you want to proceed?",
                  ButtonEnum.YesNo, Icon.Question);
        await msg.ShowDialog(CommonUtils.MainWindow);
        if (msg.ButtonResult == ButtonResult.No)
          return;
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
        var msg = CommonUtils.GetMessageBox("File in use", "An unsaved file is currently in use, " +
                  "opening a file will loose all unsaved changes,\nare you sure you want to proceed?",
                  ButtonEnum.YesNo, Icon.Question);
        await msg.ShowDialog(CommonUtils.MainWindow);
        if (msg.ButtonResult == ButtonResult.No)
          return;
      }

      OpenFileDialog dlg = new OpenFileDialog();
      dlg.Title = "Select a Bug Fables save file";
      dlg.Filters.Add(new FileDialogFilter() { Name = "Bug Fables save (.dat)", Extensions = { "dat" } });
      dlg.AllowMultiple = false;
      string[] filePaths = await dlg.ShowAsync(CommonUtils.MainWindow);
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
          var msg = CommonUtils.GetMessageBox("Error opening save file",
                      "An error occured while opening the save file: " + ex.Message, ButtonEnum.Ok, Icon.Error);
          await msg.ShowDialog(CommonUtils.MainWindow);
        }
      }
    }

    public void Exit()
    {
      CommonUtils.MainWindow.Close();
    }
  }
}
