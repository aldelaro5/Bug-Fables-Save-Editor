using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class DesignSaveDataViewModel : ObservableObject
{
  [ObservableProperty]
  private SaveData _saveData;

  public DesignSaveDataViewModel()
  {
    SaveData = new SaveData();
    SaveData.LoadFromFile(@"C:\Users\aldel\Documents\save1.dat");
  }

  public string[] Areas => Common.GetEnumDescriptions<Area>();

  public string[] Maps => Common.GetEnumDescriptions<Map>();

  public string[] SaveProgressIcons => Common.GetEnumDescriptions<SaveProgressIcon>();
}
