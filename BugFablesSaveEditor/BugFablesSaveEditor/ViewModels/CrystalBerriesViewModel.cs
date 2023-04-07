using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using DynamicData.Binding;

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
  [ObservableProperty]
  private ReadOnlyObservableCollection<CrystalBerryViewModel> _crystalBerriesSaveDataFiltered = null!;

  [ObservableProperty]
  private string _textFilter = "";

  public CrystalBerriesViewModel() : this(new(new(), x => new ObservableFlagSaveData((x)))) { }

  public CrystalBerriesViewModel(ViewModelCollection<FlagSaveData, ObservableFlagSaveData> crystalBerries)
  {
    crystalBerries
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
      .ObserveOn(SynchronizationContext.Current!)
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
      flagSaveData.Flag.Enabled = !flagSaveData.Flag.Enabled;
  }
}
