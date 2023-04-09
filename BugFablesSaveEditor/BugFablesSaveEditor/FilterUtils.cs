using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Threading;
using BugFablesSaveEditor.Models;
using DynamicData;
using DynamicData.Binding;

namespace BugFablesSaveEditor;

public static class FilterUtils
{
  public static IDisposable ObserveFlagsWithFilterAndSort<TViewModel>(IEnumerable<TViewModel> data,
                                                                      IObservable<Func<TViewModel, bool>> filter,
                                                                      out ReadOnlyObservableCollection<TViewModel>
                                                                        result)
    where TViewModel : IFlagViewModel
  {
    return data
      .AsObservableChangeSet()
      .Filter(filter)
      .Sort(SortExpressionComparer<TViewModel>.Ascending(x => x.Index))
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out result)
      .Subscribe();
  }

  public static IObservable<Func<TFlagViewModel, bool>> GetSimpleTextFilterForFlags<TFlagViewModel, TViewModel>
    (TViewModel viewModel, Expression<Func<TViewModel, string>> filterChange)
    where TFlagViewModel : IFlagViewModel
    where TViewModel : INotifyPropertyChanged
  {
    return viewModel.WhenValueChanged(filterChange)
      .Throttle(TimeSpan.FromMilliseconds(250))
      .Select(x => FlagTextFilter<TFlagViewModel>(x!));
  }

  private static Func<TFlagViewModel, bool> FlagTextFilter<TFlagViewModel>(string x)
    where TFlagViewModel : IFlagViewModel
  {
    return vm => x == string.Empty ||
                 vm.Index.ToString().Contains(x, StringComparison.OrdinalIgnoreCase) ||
                 vm.Description1.Contains(x, StringComparison.OrdinalIgnoreCase) ||
                 (!string.IsNullOrEmpty(vm.Description2) &&
                  vm.Description2.Contains(x, StringComparison.OrdinalIgnoreCase));
  }

  public static Func<FlagSaveDataModel, bool> FlagTextFilterWithUnused((string text, bool keepUnused) filter)
  {
    return vm => (filter.keepUnused || vm.Description1 != string.Empty) &&
                 (filter.text == string.Empty ||
                  (vm.Description1 == string.Empty && filter.keepUnused) ||
                  vm.Index.ToString().Contains(filter.text, StringComparison.OrdinalIgnoreCase) ||
                  vm.Description1.Contains(filter.text, StringComparison.OrdinalIgnoreCase));
  }
}
