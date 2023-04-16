using System.Collections;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;

namespace BugFablesSaveEditor.Views;

public partial class FlagsPageView : UserControl
{
  private string _textFilter = "";

  public static readonly DirectProperty<FlagsPageView, string> TextFilterProperty =
    AvaloniaProperty.RegisterDirect<FlagsPageView, string>(
      nameof(TextFilter), o => o.TextFilter, (o, v) => o.TextFilter = v, defaultBindingMode: BindingMode.TwoWay);

  public string TextFilter
  {
    get => _textFilter;
    set => SetAndRaise(TextFilterProperty, ref _textFilter, value);
  }

  private bool? _filterUnused;

  public static readonly DirectProperty<FlagsPageView, bool?> FilterUnusedProperty =
    AvaloniaProperty.RegisterDirect<FlagsPageView, bool?>(
      nameof(FilterUnused), o => o.FilterUnused, (o, v) => o.FilterUnused = v, defaultBindingMode: BindingMode.TwoWay);

  public bool? FilterUnused
  {
    get => _filterUnused;
    set => SetAndRaise(FilterUnusedProperty, ref _filterUnused, value);
  }

  private IEnumerable _collection = null!;

  public static readonly DirectProperty<FlagsPageView, IEnumerable> CollectionProperty =
    AvaloniaProperty.RegisterDirect<FlagsPageView, IEnumerable>(
      nameof(Collection), o => o.Collection, (o, v) => o.Collection = v);

  public IEnumerable Collection
  {
    get => _collection;
    set => SetAndRaise(CollectionProperty, ref _collection, value);
  }

  private IDataTemplate _valueCellTemplate = null!;

  public static readonly DirectProperty<FlagsPageView, IDataTemplate> ValueCellTemplateProperty =
    AvaloniaProperty.RegisterDirect<FlagsPageView, IDataTemplate>(
      nameof(ValueCellTemplate), o => o.ValueCellTemplate, (o, v) => o.ValueCellTemplate = v);

  public IDataTemplate ValueCellTemplate
  {
    get => _valueCellTemplate;
    set => SetAndRaise(ValueCellTemplateProperty, ref _valueCellTemplate, value);
  }

  public FlagsPageView()
  {
    InitializeComponent();
  }
}
