using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DynamicData;
using DynamicData.Binding;

namespace BugFablesSaveEditor.ViewModels;

public partial class FlagViewModel : ObservableObject
{
  [ObservableProperty]
  private int _index;

  [ObservableProperty]
  private ObservableFlagSaveData _flag = null!;

  [ObservableProperty]
  private string _description = "";
}

public partial class FlagvarViewModel : ObservableObject
{
  [ObservableProperty]
  private int _index;

  [ObservableProperty]
  private ObservableFlagvarSaveData _flag = null!;

  public string Description { get; init; } = "";
}

public partial class FlagstringViewModel : ObservableObject
{
  [ObservableProperty]
  private int _index;

  [ObservableProperty]
  private ObservableFlagstringSaveData _flag = null!;

  public string Description { get; init; } = "";
}

public partial class FlagsViewModel : ObservableRecipient, IDisposable
{
  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagViewModel> _flags;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagvarViewModel> _flagvars;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagstringViewModel> _flagstrings;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagViewModel> _regionalFlags;

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

  private readonly Collection<FlagViewModel> _regionals;

  private readonly IDisposable _flagsDispose;
  private readonly IDisposable _flagVarsDispose;
  private readonly IDisposable _flagStringsDispose;
  private readonly IDisposable _regionaFlagsDispose;

  public FlagsViewModel() : this(new(), new(), new(), new()) { }

  public FlagsViewModel(Collection<FlagSaveData> flags, Collection<FlagvarSaveData> flagvars,
                        Collection<FlagstringSaveData> flagstrings, Collection<FlagSaveData> regionalFlags)
  {
    _flagsDispose = flags
      .Select((data, i) =>
        new FlagViewModel
        {
          Index = i,
          Flag = new(data),
          Description = ExtendedData.FlagsDetails.TryGetValue(i, out string[]? extData) ? extData[0] : ""
        })
      .AsObservableChangeSet()
      .Filter(this.WhenValueChanged(x => x.TextFilterFlags)
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(FlagTextFilter!))
      .Sort(SortExpressionComparer<FlagViewModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _flags)
      .Subscribe();

    _flagVarsDispose = flagvars
      .Select((data, i) => new FlagvarViewModel()
      {
        Index = i,
        Flag = new(data),
        Description = ExtendedData.FlagvarsDetails.TryGetValue(i, out string[]? extData) ? extData[0] : ""
      })
      .AsObservableChangeSet()
      .Filter(this.WhenValueChanged(x => x.TextFilterFlagvars)
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(FlagvarTextFilter!))
      .Sort(SortExpressionComparer<FlagvarViewModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _flagvars)
      .Subscribe();

    _flagStringsDispose = flagstrings
      .Select((data, i) => new FlagstringViewModel()
      {
        Index = i,
        Flag = new(data),
        Description = ExtendedData.FlagstringsDetails.TryGetValue(i, out string[]? extData) ? extData[0] : ""
      })
      .AsObservableChangeSet()
      .Filter(this.WhenValueChanged(x => x.TextFilterFlagstrings)
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(FlagstringTextFilter!))
      .Sort(SortExpressionComparer<FlagstringViewModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _flagstrings)
      .Subscribe();

    _regionals = new(regionalFlags.Select((x, i) => new FlagViewModel { Index = i, Flag = new(x) }).ToList());
    _regionaFlagsDispose = _regionals
      .AsObservableChangeSet()
      .Filter(this.WhenChanged(x => x.TextFilterRegionalFlags, x => x.FilterUnusedRegionals, x => RegionalArea,
          (_, text, keepUnused, _) => (text, keepUnused))
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(RegionalFlagFilter!))
      .Sort(SortExpressionComparer<FlagViewModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _regionalFlags)
      .Subscribe();

    WeakReferenceMessenger.Default.Register<FlagsViewModel, ValueChangedMessage<ObservableBfNamedId>>(this,
      (r, message) =>
      {
        if (string.IsNullOrEmpty(message.Value.Name))
          return;

        foreach (FlagViewModel regional in r._regionals)
        {
          var regionalFlagsDetail = ExtendedData.RegionalFlagsDetails[message.Value.Name];
          regional.Description = regionalFlagsDetail.TryGetValue(regional.Index, out string[]? value) ? value[0] : "";
        }

        r.RegionalArea = message.Value.Name;
      });
  }

  private Func<FlagViewModel, bool> RegionalFlagFilter((string text, bool keepUnused) filter)
  {
    return vm => (filter.keepUnused || vm.Description != string.Empty) &&
                 (filter.text == string.Empty ||
                  (vm.Description == string.Empty && filter.keepUnused) ||
                  vm.Index.ToString().Contains(filter.text, StringComparison.OrdinalIgnoreCase) ||
                  vm.Description.Contains(filter.text, StringComparison.OrdinalIgnoreCase));
  }

  private Func<FlagViewModel, bool> FlagTextFilter(string x)
  {
    return vm => x == string.Empty ||
                 vm.Index.ToString().Contains(x, StringComparison.OrdinalIgnoreCase) ||
                 vm.Description.Contains(x, StringComparison.OrdinalIgnoreCase);
  }

  private Func<FlagvarViewModel, bool> FlagvarTextFilter(string x)
  {
    return vm => x == string.Empty ||
                 vm.Index.ToString().Contains(x, StringComparison.OrdinalIgnoreCase) ||
                 vm.Description.Contains(x, StringComparison.OrdinalIgnoreCase);
  }

  private Func<FlagstringViewModel, bool> FlagstringTextFilter(string x)
  {
    return vm => x == string.Empty ||
                 vm.Index.ToString().Contains(x, StringComparison.OrdinalIgnoreCase) ||
                 vm.Description.Contains(x, StringComparison.OrdinalIgnoreCase);
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
