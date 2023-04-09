using System;
using System.Collections.ObjectModel;
using System.Linq;
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
  private readonly IDisposable _regionaFlagsDispose;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagSaveDataModel> _flags;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagvarSaveDataModel> _flagvars;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagstringSaveDataModel> _flagstrings;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagSaveDataModel> _regionalFlags;

  [ObservableProperty]
  private string _textFilterFlags = "";

  [ObservableProperty]
  private string _textFilterFlagvars = "";

  [ObservableProperty]
  private string _textFilterFlagstrings = "";

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
    _flagsDispose = flags
      .Select((data, i) =>
        new FlagSaveDataModel(data)
        {
          Index = i,
          Description1 = ExtendedData.FlagsDetails.TryGetValue(i, out string[]? extData) ? extData[0] : ""
        })
      .AsObservableChangeSet()
      .Filter(this.WhenValueChanged(x => x.TextFilterFlags)
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(FlagTextFilter!))
      .Sort(SortExpressionComparer<FlagSaveDataModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _flags)
      .Subscribe();

    _flagVarsDispose = flagvars
      .Select((data, i) => new FlagvarSaveDataModel(data)
      {
        Index = i,
        Description1 = ExtendedData.FlagvarsDetails.TryGetValue(i, out string[]? extData) ? extData[0] : ""
      })
      .AsObservableChangeSet()
      .Filter(this.WhenValueChanged(x => x.TextFilterFlagvars)
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(FlagvarTextFilter!))
      .Sort(SortExpressionComparer<FlagvarSaveDataModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _flagvars)
      .Subscribe();

    _flagStringsDispose = flagstrings
      .Select((data, i) => new FlagstringSaveDataModel(data)
      {
        Index = i,
        Description1 = ExtendedData.FlagstringsDetails.TryGetValue(i, out string[]? extData) ? extData[0] : ""
      })
      .AsObservableChangeSet()
      .Filter(this.WhenValueChanged(x => x.TextFilterFlagstrings)
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(FlagstringTextFilter!))
      .Sort(SortExpressionComparer<FlagstringSaveDataModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _flagstrings)
      .Subscribe();

    _regionals = new(regionalFlags.Select((x, i) => new FlagSaveDataModel(x) { Index = i }).ToList());
    _regionaFlagsDispose = _regionals
      .AsObservableChangeSet()
      .Filter(this.WhenChanged(x => x.TextFilterRegionalFlags, x => x.FilterUnusedRegionals, x => RegionalArea,
          (_, text, keepUnused, _) => (text, keepUnused))
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(RegionalFlagFilter!))
      .Sort(SortExpressionComparer<FlagSaveDataModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _regionalFlags)
      .Subscribe();

    WeakReferenceMessenger.Default.Register<FlagsViewModel, ValueChangedMessage<BfNamedIdModel>>(this,
      (r, message) =>
      {
        if (string.IsNullOrEmpty(message.Value.Name))
          return;

        foreach (FlagSaveDataModel regional in r._regionals)
        {
          var regionalFlagsDetail = ExtendedData.RegionalFlagsDetails[message.Value.Name];
          regional.Description1 = regionalFlagsDetail.TryGetValue(regional.Index, out string[]? value) ? value[0] : "";
        }

        r.RegionalArea = message.Value.Name;
      });
  }

  private Func<FlagSaveDataModel, bool> RegionalFlagFilter((string text, bool keepUnused) filter)
  {
    return vm => (filter.keepUnused || vm.Description1 != string.Empty) &&
                 (filter.text == string.Empty ||
                  (vm.Description1 == string.Empty && filter.keepUnused) ||
                  vm.Index.ToString().Contains(filter.text, StringComparison.OrdinalIgnoreCase) ||
                  vm.Description1.Contains(filter.text, StringComparison.OrdinalIgnoreCase));
  }

  private Func<FlagSaveDataModel, bool> FlagTextFilter(string x)
  {
    return vm => x == string.Empty ||
                 vm.Index.ToString().Contains(x, StringComparison.OrdinalIgnoreCase) ||
                 vm.Description1.Contains(x, StringComparison.OrdinalIgnoreCase);
  }

  private Func<FlagvarSaveDataModel, bool> FlagvarTextFilter(string x)
  {
    return vm => x == string.Empty ||
                 vm.Index.ToString().Contains(x, StringComparison.OrdinalIgnoreCase) ||
                 vm.Description1.Contains(x, StringComparison.OrdinalIgnoreCase);
  }

  private Func<FlagstringSaveDataModel, bool> FlagstringTextFilter(string x)
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
    _regionaFlagsDispose.Dispose();
    WeakReferenceMessenger.Default.UnregisterAll(this);
  }
}
