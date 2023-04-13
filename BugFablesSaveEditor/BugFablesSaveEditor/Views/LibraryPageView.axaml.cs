using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using BugFablesSaveEditor.Models;

namespace BugFablesSaveEditor.Views;

public partial class LibraryPageView : UserControl
{
  private bool _filterUnused;

  public static readonly DirectProperty<LibraryPageView, bool> FilterUnusedProperty =
    AvaloniaProperty.RegisterDirect<LibraryPageView, bool>(
      nameof(FilterUnused),
      o => o.FilterUnused,
      (o, v) => o.FilterUnused = v,
      defaultBindingMode: BindingMode.TwoWay);

  public bool FilterUnused
  {
    get => _filterUnused;
    set => SetAndRaise(FilterUnusedProperty, ref _filterUnused, value);
  }

  private string _testFilter = "";

  public static readonly DirectProperty<LibraryPageView, string> TestFilterProperty =
    AvaloniaProperty.RegisterDirect<LibraryPageView, string>(
      nameof(TestFilter),
      o => o.TestFilter,
      (o, v) => o.TestFilter = v,
      defaultBindingMode: BindingMode.TwoWay);

  public string TestFilter
  {
    get => _testFilter;
    set => SetAndRaise(TestFilterProperty, ref _testFilter, value);
  }

  private IList<FlagSaveDataModel> _flagsCollection = null!;

  public static readonly DirectProperty<LibraryPageView, IList<FlagSaveDataModel>> FlagsCollectionProperty =
    AvaloniaProperty.RegisterDirect<LibraryPageView, IList<FlagSaveDataModel>>(
      nameof(FlagsCollection),
      o => o.FlagsCollection,
      (o, v) => o.FlagsCollection = v,
      defaultBindingMode: BindingMode.TwoWay);

  public IList<FlagSaveDataModel> FlagsCollection
  {
    get => _flagsCollection;
    set => SetAndRaise(FlagsCollectionProperty, ref _flagsCollection, value);
  }

  public LibraryPageView()
  {
    InitializeComponent();
  }
}
