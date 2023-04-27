using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Styling;
using DynamicData;

namespace BugFablesSaveEditor.Views;

public partial class FlagsCollectionView : UserControl
{
  private string _textFilter = "";

  public static readonly DirectProperty<FlagsCollectionView, string> TextFilterProperty =
    AvaloniaProperty.RegisterDirect<FlagsCollectionView, string>(
      nameof(TextFilter), o => o.TextFilter, (o, v) => o.TextFilter = v, defaultBindingMode: BindingMode.TwoWay);

  public string TextFilter
  {
    get => _textFilter;
    set => SetAndRaise(TextFilterProperty, ref _textFilter, value);
  }

  private bool? _filterUnused;

  public static readonly DirectProperty<FlagsCollectionView, bool?> FilterUnusedProperty =
    AvaloniaProperty.RegisterDirect<FlagsCollectionView, bool?>(
      nameof(FilterUnused), o => o.FilterUnused, (o, v) => o.FilterUnused = v, defaultBindingMode: BindingMode.TwoWay);

  public bool? FilterUnused
  {
    get => _filterUnused;
    set => SetAndRaise(FilterUnusedProperty, ref _filterUnused, value);
  }

  private ICommand? _toggleAllShownCommand;

  public static readonly DirectProperty<FlagsCollectionView, ICommand?> ToggleAllShownCommandProperty =
    AvaloniaProperty.RegisterDirect<FlagsCollectionView, ICommand?>(
      nameof(ToggleAllShownCommand), o => o.ToggleAllShownCommand, (o, v) => o.ToggleAllShownCommand = v,
      defaultBindingMode: BindingMode.TwoWay);

  public ICommand? ToggleAllShownCommand
  {
    get => _toggleAllShownCommand;
    set => SetAndRaise(ToggleAllShownCommandProperty, ref _toggleAllShownCommand, value);
  }

  private IEnumerable _collection = null!;

  public static readonly DirectProperty<FlagsCollectionView, IEnumerable> CollectionProperty =
    AvaloniaProperty.RegisterDirect<FlagsCollectionView, IEnumerable>(
      nameof(Collection), o => o.Collection, (o, v) => o.Collection = v);

  public IEnumerable Collection
  {
    get => _collection;
    set => SetAndRaise(CollectionProperty, ref _collection, value);
  }

  private string _description1Header = "";

  public static readonly DirectProperty<FlagsCollectionView, string> Description1HeaderProperty =
    AvaloniaProperty.RegisterDirect<FlagsCollectionView, string>(
      nameof(Description1Header), o => o.Description1Header, (o, v) => o.Description1Header = v);

  public string Description1Header
  {
    get => _description1Header;
    set => SetAndRaise(Description1HeaderProperty, ref _description1Header, value);
  }

  private string _description2Header = "";

  public static readonly DirectProperty<FlagsCollectionView, string> Description2HeaderProperty =
    AvaloniaProperty.RegisterDirect<FlagsCollectionView, string>(
      nameof(Description2Header), o => o.Description2Header, (o, v) => o.Description2Header = v);

  public string Description2Header
  {
    get => _description2Header;
    set => SetAndRaise(Description2HeaderProperty, ref _description2Header, value);
  }

  private IDataTemplate _valueCellTemplate = null!;

  public static readonly DirectProperty<FlagsCollectionView, IDataTemplate> ValueCellTemplateProperty =
    AvaloniaProperty.RegisterDirect<FlagsCollectionView, IDataTemplate>(
      nameof(ValueCellTemplate), o => o.ValueCellTemplate, (o, v) => o.ValueCellTemplate = v);

  public IDataTemplate ValueCellTemplate
  {
    get => _valueCellTemplate;
    set => SetAndRaise(ValueCellTemplateProperty, ref _valueCellTemplate, value);
  }

  private DataGridColumnsTemplate _additionalColumnsTemplate = null!;

  public static readonly DirectProperty<FlagsCollectionView, DataGridColumnsTemplate>
    AdditionalColumnsTemplateProperty = AvaloniaProperty.RegisterDirect<FlagsCollectionView, DataGridColumnsTemplate>(
      nameof(AdditionalColumnsTemplate), o => o.AdditionalColumnsTemplate, (o, v) => o.AdditionalColumnsTemplate = v);

  public DataGridColumnsTemplate AdditionalColumnsTemplate
  {
    get => _additionalColumnsTemplate;
    set
    {
      SetAndRaise(AdditionalColumnsTemplateProperty, ref _additionalColumnsTemplate, value);
      var columnsList = (ObservableCollection<DataGridColumn>)((ITemplate)AdditionalColumnsTemplate).Build();
      DataGrid.Columns.AddRange(columnsList.ToArray());
    }
  }


  private bool _showSecondDescription;

  public static readonly DirectProperty<FlagsCollectionView, bool> ShowSecondDescriptionProperty =
    AvaloniaProperty.RegisterDirect<FlagsCollectionView, bool>(
      nameof(ShowSecondDescription), o => o.ShowSecondDescription, (o, v) => o.ShowSecondDescription = v);

  public bool ShowSecondDescription
  {
    get => _showSecondDescription;
    set => SetAndRaise(ShowSecondDescriptionProperty, ref _showSecondDescription, value);
  }

  public FlagsCollectionView()
  {
    InitializeComponent();
  }
}
