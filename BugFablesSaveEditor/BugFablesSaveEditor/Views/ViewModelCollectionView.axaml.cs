using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Styling;
using Avalonia.Xaml.Interactions.DragAndDrop;
using DynamicData;

namespace BugFablesSaveEditor.Views;

public partial class ViewModelCollectionView : UserControl
{
  private IViewModelCollection? _viewModelCollection;

  public static readonly DirectProperty<ViewModelCollectionView, IViewModelCollection?> ViewModelCollectionProperty =
    AvaloniaProperty.RegisterDirect<ViewModelCollectionView, IViewModelCollection?>(
      nameof(ViewModelCollection),
      o => o.ViewModelCollection,
      (o, v) => o.ViewModelCollection = v,
      defaultBindingMode: BindingMode.OneWay);

  public IViewModelCollection? ViewModelCollection
  {
    get => _viewModelCollection;
    set => SetAndRaise(ViewModelCollectionProperty, ref _viewModelCollection, value);
  }

  private string _titleLabel = "";

  public static readonly DirectProperty<ViewModelCollectionView, string> TitleLabelProperty =
    AvaloniaProperty.RegisterDirect<ViewModelCollectionView, string>(
      nameof(TitleLabel),
      o => o.TitleLabel,
      (o, v) => o.TitleLabel = v,
      defaultBindingMode: BindingMode.OneTime);

  public string TitleLabel
  {
    get => _titleLabel;
    set => SetAndRaise(TitleLabelProperty, ref _titleLabel, value);
  }

  private DataGridColumnsTemplate _columnsTemplate = null!;

  public static readonly DirectProperty<ViewModelCollectionView, DataGridColumnsTemplate>
    ColumnsTemplateProperty = AvaloniaProperty.RegisterDirect<ViewModelCollectionView, DataGridColumnsTemplate>(
      nameof(ColumnsTemplate), o => o.ColumnsTemplate, (o, v) => o.ColumnsTemplate = v);

  public DataGridColumnsTemplate ColumnsTemplate
  {
    get => _columnsTemplate;
    set
    {
      SetAndRaise(ColumnsTemplateProperty, ref _columnsTemplate, value);
      var columnsList = (ObservableCollection<DataGridColumn>)((ITemplate)ColumnsTemplate).Build();
      DataGrid.Columns.AddRange(columnsList.ToArray());
      DataGrid.Columns[1].DisplayIndex = DataGrid.Columns.Count - 1;
    }
  }

  private IDropHandler _dndDropHandler = null!;

  public static readonly DirectProperty<ViewModelCollectionView, IDropHandler> DndDropHandlerProperty =
    AvaloniaProperty.RegisterDirect<ViewModelCollectionView, IDropHandler>(
      nameof(DndDropHandler), o => o.DndDropHandler, (o, v) => o.DndDropHandler = v);

  public IDropHandler DndDropHandler
  {
    get => _dndDropHandler;
    set => SetAndRaise(DndDropHandlerProperty, ref _dndDropHandler, value);
  }

  private DataTemplate _addViewTemplate = null!;

  public static readonly DirectProperty<ViewModelCollectionView, DataTemplate> AddViewTemplateProperty =
    AvaloniaProperty.RegisterDirect<ViewModelCollectionView, DataTemplate>(
      nameof(AddViewTemplate), o => o.AddViewTemplate, (o, v) => o.AddViewTemplate = v);

  public DataTemplate AddViewTemplate
  {
    get => _addViewTemplate;
    set => SetAndRaise(AddViewTemplateProperty, ref _addViewTemplate, value);
  }

  public ViewModelCollectionView()
  {
    InitializeComponent();
  }
}
