using System;
using System.Collections.ObjectModel;
using System.Linq;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.Core.FilterUtils;

namespace BugFablesSaveEditor.Core.ViewModels;

public partial class CrystalBerriesViewModel : ObservableObject, IDisposable
{
  private readonly IDisposable _crystalBerriesDisposable;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagSaveDataModel> _crystalBerries;

  [ObservableProperty]
  private string _textFilter = "";

  public CrystalBerriesViewModel() : this(new()) { }

  public CrystalBerriesViewModel(Collection<FlagSaveData> crystalBerries)
  {
    var filter = GetSimpleTextFilterForFlags<FlagSaveDataModel, CrystalBerriesViewModel>(this, x => x.TextFilter);
    var flagsWithMetaData = crystalBerries.Select((data, i) => new FlagSaveDataModel(data)
    {
      Index = i,
      Description1 = ExtendedData.CrystalBerriesDetails.TryGetValue(i, out string[]? extData1) ? extData1[0] : "",
      Description2 = ExtendedData.CrystalBerriesDetails.TryGetValue(i, out string[]? extData2) ? extData2[1] : ""
    });

    _crystalBerriesDisposable = ObserveFlagsWithFilterAndSort(flagsWithMetaData, filter, out _crystalBerries);
  }

  [RelayCommand]
  private void ToggleAllShown()
  {
    foreach (var flagSaveData in CrystalBerries)
      flagSaveData.Enabled = !flagSaveData.Enabled;
  }

  public void Dispose()
  {
    _crystalBerriesDisposable.Dispose();
  }
}
