using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class GlobalViewModel : ObservableObject
{
  [ObservableProperty]
  private ObservableGlobalSaveData _globalSaveData;

  [ObservableProperty]
  private ObservableHeaderSaveData _headerSaveData;

  public GlobalViewModel(): this(new(new()), new(new())) { }

  public GlobalViewModel(ObservableGlobalSaveData globalSaveData,
                         ObservableHeaderSaveData headerSaveData)
  {
    _globalSaveData = globalSaveData;
    _headerSaveData = headerSaveData;
  }
}
