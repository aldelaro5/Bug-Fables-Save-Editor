using System;
using System.Collections;
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

public partial class FlagsViewModel : ObservableRecipient
{
  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagViewModel> _flags = null!;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagvarViewModel> _flagvars = null!;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagstringViewModel> _flagstrings = null!;

  [ObservableProperty]
  private ReadOnlyObservableCollection<FlagViewModel> _regionalFlags = null!;

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

  private readonly ObservableCollection<FlagViewModel> _regionalFlagsSaveData;

  public FlagsViewModel() : this(
    new(new(), x => new ObservableFlagSaveData(x)),
    new(new(), x => new ObservableFlagvarSaveData(x)),
    new(new(), x => new ObservableFlagstringSaveData(x)),
    new(new(), x => new ObservableFlagSaveData(x)))
  {
  }

  public FlagsViewModel(ViewModelCollection<FlagSaveData, ObservableFlagSaveData> flags,
                        ViewModelCollection<FlagvarSaveData, ObservableFlagvarSaveData> flagvars,
                        ViewModelCollection<FlagstringSaveData, ObservableFlagstringSaveData> flagstrings,
                        ViewModelCollection<FlagSaveData, ObservableFlagSaveData> regionalFlags)
  {
    _regionalFlagsSaveData = new(regionalFlags.CollectionView
      .Select((x, i) => new FlagViewModel { Index = i, Flag = x }).ToList());

    flags.CollectionView
      .Select((data, i) => new FlagViewModel { Index = i, Flag = data, Description = ExtendedData.FlagsDetails[i] })
      .AsObservableChangeSet()
      .Filter(this.WhenValueChanged(x => x.TextFilterFlags)
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(FlagTextFilter!))
      .Sort(SortExpressionComparer<FlagViewModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _flags)
      .Subscribe();

    flagvars.CollectionView
      .Select((data, i) => new FlagvarViewModel()
      {
        Index = i, Flag = data, Description = ExtendedData.FlagvarsDetails[i]
      })
      .AsObservableChangeSet()
      .Filter(this.WhenValueChanged(x => x.TextFilterFlagvars)
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(FlagvarTextFilter!))
      .Sort(SortExpressionComparer<FlagvarViewModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _flagvars)
      .Subscribe();

    flagstrings.CollectionView
      .Select((data, i) => new FlagstringViewModel()
      {
        Index = i, Flag = data, Description = ExtendedData.FlagstringsDetails[i]
      })
      .AsObservableChangeSet()
      .Filter(this.WhenValueChanged(x => x.TextFilterFlagstrings)
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(FlagstringTextFilter!))
      .Sort(SortExpressionComparer<FlagstringViewModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _flagstrings)
      .Subscribe();

    _regionalFlagsSaveData
      .AsObservableChangeSet()
      .Filter(this.WhenChanged(x => x.TextFilterRegionalFlags, x => x.FilterUnusedRegionals,
          (_, text, keepUnused) => (text, keepUnused))
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(RegionalFlagFilter!))
      .Sort(SortExpressionComparer<FlagViewModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _regionalFlags)
      .Subscribe();

    WeakReferenceMessenger.Default
      .Register<FlagsViewModel, ValueChangedMessage<ObservableBfNamedId>>(this,
        (r, message) =>
        {
          if (string.IsNullOrEmpty(message.Value.Name))
            return;

          string[] descriptions = ExtendedData.RegionalFlagsDetails[message.Value.Name];
          foreach (var flagSaveData in r._regionalFlagsSaveData)
          {
            if (flagSaveData.Index < descriptions.Length)
              flagSaveData.Description = descriptions[flagSaveData.Index];
            else
              flagSaveData.Description = "";
          }
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
}
