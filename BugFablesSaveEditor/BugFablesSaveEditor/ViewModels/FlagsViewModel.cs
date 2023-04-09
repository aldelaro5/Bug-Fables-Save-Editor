using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Threading;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DynamicData;
using DynamicData.Binding;

namespace BugFablesSaveEditor.ViewModels;

public partial class FlagsViewModel : ObservableRecipient, IDisposable
{
  private readonly Collection<FlagSaveDataModel> _regionals;

  private readonly IDisposable _flagsDispose;
  private readonly IDisposable _flagVarsDispose;
  private readonly IDisposable _flagStringsDispose;
  private readonly IDisposable _regionalFlagsDispose;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagSaveDataModel> _flags;

  [ObservableProperty]
  private string _textFilterFlags = "";

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagvarSaveDataModel> _flagvars;

  [ObservableProperty]
  private string _textFilterFlagvars = "";

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagstringSaveDataModel> _flagstrings;

  [ObservableProperty]
  private string _textFilterFlagstrings = "";

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagSaveDataModel> _regionalFlags;

  [ObservableProperty]
  private string _textFilterRegionalFlags = "";

  [ObservableProperty]
  private bool _filterUnusedRegionals;

  [ObservableProperty]
  private string _regionalArea = "";

  public FlagsViewModel() : this(new(), new(), new(), new()) { }

  public FlagsViewModel(Collection<FlagSaveData> flags, Collection<FlagvarSaveData> flagvars,
                        Collection<FlagstringSaveData> flagstrings, Collection<FlagSaveData> regionalFlags)
  {
    var flagsWithMetaData = flags.Select((data, i) =>
      AssignMetaData(new FlagSaveDataModel(data), i, ExtendedData.FlagsDetails));

    _flagsDispose = ObserveDataWithFilterAndSort(flagsWithMetaData,
      x => x.TextFilterFlags, out _flags);

    var flagvarsWithMetaData = flagvars.Select((data, i) =>
      AssignMetaData(new FlagvarSaveDataModel(data), i, ExtendedData.FlagvarsDetails));

    _flagVarsDispose = ObserveDataWithFilterAndSort(flagvarsWithMetaData,
      x => x.TextFilterFlagvars, out _flagvars);

    var flagtringsWithMetaData = flagstrings.Select((data, i) =>
      AssignMetaData(new FlagstringSaveDataModel(data), i, ExtendedData.FlagstringsDetails));

    _flagStringsDispose = ObserveDataWithFilterAndSort(flagtringsWithMetaData,
      x => x.TextFilterFlagstrings, out _flagstrings);

    _regionals = new(regionalFlags.Select((x, i) => new FlagSaveDataModel(x) { Index = i }).ToList());
    _regionalFlagsDispose = _regionals
      .AsObservableChangeSet()
      .Filter(this.WhenChanged(x => x.TextFilterRegionalFlags, x => x.FilterUnusedRegionals, x => RegionalArea,
          (_, text, keepUnused, _) => (text, keepUnused))
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(RegionalFlagFilter!))
      .Sort(SortExpressionComparer<FlagSaveDataModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _regionalFlags)
      .Subscribe();

    WeakReferenceMessenger.Default.Register<FlagsViewModel, ValueChangedMessage<BfNamedIdModel>>(this, OnAreaChanged);
  }

  private void OnAreaChanged(FlagsViewModel r, ValueChangedMessage<BfNamedIdModel> message)
  {
    if (string.IsNullOrEmpty(message.Value.Name)) return;

    foreach (FlagSaveDataModel regional in r._regionals)
    {
      var regionalFlagsDetail = ExtendedData.RegionalFlagsDetails[message.Value.Name];
      regional.Description1 = regionalFlagsDetail.TryGetValue(regional.Index, out string[]? value) ? value[0] : "";
    }

    r.RegionalArea = message.Value.Name;
  }

  private T AssignMetaData<T>(T flagViewModel, int index, Dictionary<int, string[]> extendedData)
    where T : IFlagViewModel
  {
    flagViewModel.Index = index;
    flagViewModel.Description1 = extendedData.TryGetValue(index, out string[]? extData) ? extData[0] : "";
    return flagViewModel;
  }

  private IDisposable ObserveDataWithFilterAndSort<T>(IEnumerable<T> data,
                                                      Expression<Func<FlagsViewModel, string>> filterChange,
                                                      out ReadOnlyObservableCollection<T> result)
    where T : IFlagViewModel
  {
    return data
      .AsObservableChangeSet()
      .Filter(this.WhenValueChanged(filterChange)
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(x => FlagTextFilter<T>(x!)))
      .Sort(SortExpressionComparer<T>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out result)
      .Subscribe();
  }

  private Func<FlagSaveDataModel, bool> RegionalFlagFilter((string text, bool keepUnused) filter)
  {
    return vm => (filter.keepUnused || vm.Description1 != string.Empty) &&
                 (filter.text == string.Empty ||
                  (vm.Description1 == string.Empty && filter.keepUnused) ||
                  vm.Index.ToString().Contains(filter.text, StringComparison.OrdinalIgnoreCase) ||
                  vm.Description1.Contains(filter.text, StringComparison.OrdinalIgnoreCase));
  }

  private Func<T, bool> FlagTextFilter<T>(string x) where T : IFlagViewModel
  {
    return vm => x == string.Empty ||
                 vm.Index.ToString().Contains(x, StringComparison.OrdinalIgnoreCase) ||
                 vm.Description1.Contains(x, StringComparison.OrdinalIgnoreCase);
  }

  public void Dispose()
  {
    _flagsDispose.Dispose();
    _flagVarsDispose.Dispose();
    _flagStringsDispose.Dispose();
    _regionalFlagsDispose.Dispose();
    WeakReferenceMessenger.Default.UnregisterAll(this);
  }
}
