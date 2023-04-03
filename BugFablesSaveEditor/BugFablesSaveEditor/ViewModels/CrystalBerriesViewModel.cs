using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using DynamicData.Binding;
using Reactive.Bindings.Extensions;

namespace BugFablesSaveEditor.ViewModels;

public partial class CrystalBerryViewModel : ObservableObject
{
  [ObservableProperty]
  private int _index;

  [ObservableProperty]
  private ObservableFlagSaveData _flag = null!;

  public string Area { get; init; } = "";
  public string Location { get; init; } = "";
}

public partial class CrystalBerriesViewModel : ObservableObject
{
  private readonly ObservableBfCollection<FlagSaveData, ObservableFlagSaveData>
    _crystalBerriesSaveData;

  [ObservableProperty]
  private ReadOnlyObservableCollection<CrystalBerryViewModel> _crystalBerriesSaveDataFiltered =
    null!;

  [ObservableProperty]
  private string _textFilter = "";

  public CrystalBerriesViewModel()
  {
    _crystalBerriesSaveData = new(new(), _ => new List<ObservableFlagSaveData>());
  }

  public CrystalBerriesViewModel(
    ObservableBfCollection<FlagSaveData, ObservableFlagSaveData> crystalBerries)
  {
    _crystalBerriesSaveData = crystalBerries;
    _crystalBerriesSaveData
      .Select((data, i) => new CrystalBerryViewModel
      {
        Index = i,
        Flag = data,
        Area = ExtendedData.CrystalBerriesDetails[i][0],
        Location = ExtendedData.CrystalBerriesDetails[i][1],
      })
      .ToObservable()
      .ToObservableChangeSet()
      .Filter(this.WhenValueChanged(x => x.TextFilter)
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(CrystalBerryFilter!))
      .Sort(SortExpressionComparer<CrystalBerryViewModel>.Ascending(x => x.Index))
      .ObserveOnUIDispatcher()
      .Bind(out _crystalBerriesSaveDataFiltered)
      .Subscribe();
  }

  private Func<CrystalBerryViewModel, bool> CrystalBerryFilter(string x)
  {
    return vm => x == string.Empty ||
                 vm.Index.ToString().Contains(x, StringComparison.OrdinalIgnoreCase) ||
                 vm.Area.Contains(x, StringComparison.OrdinalIgnoreCase) ||
                 vm.Location.Contains(x, StringComparison.OrdinalIgnoreCase);
  }

  [RelayCommand]
  private void ToggleAllShown()
  {
    foreach (var flagSaveData in CrystalBerriesSaveDataFiltered)
      flagSaveData.Flag.Enabled.Value = !flagSaveData.Flag.Enabled.Value;
  }
}
