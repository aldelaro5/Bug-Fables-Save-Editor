using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using DynamicData.Binding;

namespace BugFablesSaveEditor.ViewModels;

public partial class CrystalBerriesViewModel : ObservableObject
{
  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagSaveDataModel> _crystalBerriesSaveDataFiltered;

  [ObservableProperty]
  private string _textFilter = "";

  public CrystalBerriesViewModel() : this(new()) { }

  public CrystalBerriesViewModel(Collection<FlagSaveData> crystalBerries)
  {
    crystalBerries
      .Select((data, i) => new FlagSaveDataModel(data)
      {
        Index = i,
        Description1 = ExtendedData.CrystalBerriesDetails.TryGetValue(i, out string[]? extData1) ? extData1[0] : "",
        Description2 = ExtendedData.CrystalBerriesDetails.TryGetValue(i, out string[]? extData2) ? extData2[1] : ""
      })
      .ToObservable()
      .ToObservableChangeSet()
      .Filter(this.WhenValueChanged(x => x.TextFilter)
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(CrystalBerryFilter!))
      .Sort(SortExpressionComparer<FlagSaveDataModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _crystalBerriesSaveDataFiltered)
      .Subscribe();
  }

  private Func<FlagSaveDataModel, bool> CrystalBerryFilter(string x)
  {
    return vm => x == string.Empty ||
                 vm.Index.ToString().Contains(x, StringComparison.OrdinalIgnoreCase) ||
                 vm.Description1.Contains(x, StringComparison.OrdinalIgnoreCase) ||
                 vm.Description2.Contains(x, StringComparison.OrdinalIgnoreCase);
  }

  [RelayCommand]
  private void ToggleAllShown()
  {
    foreach (var flagSaveData in CrystalBerriesSaveDataFiltered)
      flagSaveData.Enabled = !flagSaveData.Enabled;
  }
}
