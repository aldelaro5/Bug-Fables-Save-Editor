using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DynamicData.Binding;
using static BugFablesSaveEditor.FilterUtils;

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
    _flagsDispose = SetupAndSubscribeToFlags(flags, ExtendedData.FlagsDetails,
      x => x.TextFilterFlags, out _flags);
    _flagVarsDispose = SetupAndSubscribeToFlags(flagvars, ExtendedData.FlagvarsDetails,
      x => x.TextFilterFlagvars, out _flagvars);
    _flagStringsDispose = SetupAndSubscribeToFlags(flagstrings, ExtendedData.FlagstringsDetails,
      x => x.TextFilterFlagstrings, out _flagstrings);

    _regionals = new(regionalFlags.Select((x, i) => new FlagSaveDataModel(x) { Index = i }).ToList());
    _regionalFlagsDispose = SetupAndSubscribeToRegionals(out _regionalFlags);

    WeakReferenceMessenger.Default.Register<FlagsViewModel, ValueChangedMessage<BfNamedIdModel>>(this, OnAreaChanged);
  }

  private IDisposable SetupAndSubscribeToRegionals(out ReadOnlyObservableCollection<FlagSaveDataModel> result)
  {
    var regionalFilter =
      this.WhenChanged(x => x.TextFilterRegionalFlags, x => x.FilterUnusedRegionals, x => RegionalArea,
          (_, text, keepUnused, _) => (text, keepUnused))
        .Throttle(TimeSpan.FromMilliseconds(250))
        .Select(FlagTextFilterWithUnused!);

    return ObserveFlagsWithFilterAndSort(_regionals, regionalFilter, out result);
  }

  private IDisposable SetupAndSubscribeToFlags<TModel, TViewModel>(IEnumerable<TModel> flags,
                                                                   Dictionary<int, string[]> extendedData,
                                                                   Expression<Func<FlagsViewModel, string>> textFilter,
                                                                   out ReadOnlyObservableCollection<TViewModel> result)
    where TViewModel : IModelWrapper<TModel>, IFlagViewModel
  {
    var flagsWithMetaData =
      flags.Select((data, i) => AssignMetaData((TViewModel)TViewModel.WrapModel(data), i, extendedData));

    return ObserveFlagsWithFilterAndSort(flagsWithMetaData,
      GetSimpleTextFilterForFlags<TViewModel, FlagsViewModel>(this, textFilter),
      out result);
  }

  private static void OnAreaChanged(FlagsViewModel r, ValueChangedMessage<BfNamedIdModel> message)
  {
    if (string.IsNullOrEmpty(message.Value.Name)) return;

    foreach (FlagSaveDataModel regional in r._regionals)
    {
      var regionalFlagsDetail = ExtendedData.RegionalFlagsDetails[message.Value.Name];
      regional.Description1 = regionalFlagsDetail.TryGetValue(regional.Index, out string[]? value) ? value[0] : "";
    }

    r.RegionalArea = message.Value.Name;
  }

  private static TFlagViewModel AssignMetaData<TFlagViewModel>(TFlagViewModel flagViewModel, int index,
                                                               Dictionary<int, string[]> extendedData)
    where TFlagViewModel : IFlagViewModel
  {
    flagViewModel.Index = index;
    flagViewModel.Description1 = extendedData.TryGetValue(index, out string[]? extData) ? extData[0] : "";
    return flagViewModel;
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
