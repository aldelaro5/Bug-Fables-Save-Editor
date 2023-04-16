using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Xaml.Interactions.DragAndDrop;

namespace BugFablesSaveEditor.Views;

public partial class EditableModelCollectionView : UserControl, IDisposable
{
  private ICommand _deleteCommand = null!;

  public static readonly DirectProperty<EditableModelCollectionView, ICommand> DeleteCommandProperty =
    AvaloniaProperty.RegisterDirect<EditableModelCollectionView, ICommand>(
      nameof(DeleteCommand),
      o => o.DeleteCommand,
      (o, v) => o.DeleteCommand = v,
      defaultBindingMode: BindingMode.OneWay);

  public ICommand DeleteCommand
  {
    get => _deleteCommand;
    set => SetAndRaise(DeleteCommandProperty, ref _deleteCommand, value);
  }

  private ICommand _addCommand = null!;

  public static readonly DirectProperty<EditableModelCollectionView, ICommand> AddCommandProperty =
    AvaloniaProperty.RegisterDirect<EditableModelCollectionView, ICommand>(
      nameof(AddCommand),
      o => o.AddCommand,
      (o, v) => o.AddCommand = v,
      defaultBindingMode: BindingMode.OneWay);

  public ICommand AddCommand
  {
    get => _addCommand;
    set => SetAndRaise(AddCommandProperty, ref _addCommand, value);
  }

  private object _newModel = null!;

  public static readonly DirectProperty<EditableModelCollectionView, object> NewModelProperty =
    AvaloniaProperty.RegisterDirect<EditableModelCollectionView, object>(
      nameof(NewModel),
      o => o.NewModel,
      (o, v) => o.NewModel = v,
      defaultBindingMode: BindingMode.OneWay);

  public object NewModel
  {
    get => _newModel;
    set => SetAndRaise(NewModelProperty, ref _newModel, value);
  }

  private IEnumerable _collection = null!;

  public static readonly DirectProperty<EditableModelCollectionView, IEnumerable> CollectionProperty =
    AvaloniaProperty.RegisterDirect<EditableModelCollectionView, IEnumerable>(
      nameof(Collection),
      o => o.Collection,
      (o, v) => o.Collection = v,
      defaultBindingMode: BindingMode.OneWay);

  public IEnumerable Collection
  {
    get => _collection;
    set => SetAndRaise(CollectionProperty, ref _collection, value);
  }

  private string _titleLabel = "";

  public static readonly DirectProperty<EditableModelCollectionView, string> TitleLabelProperty =
    AvaloniaProperty.RegisterDirect<EditableModelCollectionView, string>(
      nameof(TitleLabel),
      o => o.TitleLabel,
      (o, v) => o.TitleLabel = v,
      defaultBindingMode: BindingMode.OneTime);

  public string TitleLabel
  {
    get => _titleLabel;
    set => SetAndRaise(TitleLabelProperty, ref _titleLabel, value);
  }

  private ObservableCollection<DataGridColumn> _columns = new();

  public static readonly DirectProperty<EditableModelCollectionView, ObservableCollection<DataGridColumn>>
    ColumnsProperty =
      AvaloniaProperty.RegisterDirect<EditableModelCollectionView, ObservableCollection<DataGridColumn>>(
        nameof(Columns), o => o.Columns, (o, v) => o.Columns = v);

  public ObservableCollection<DataGridColumn> Columns
  {
    get => _columns;
    set => SetAndRaise(ColumnsProperty, ref _columns, value);
  }

  private IDropHandler _dndDropHandler = null!;

  public static readonly DirectProperty<EditableModelCollectionView, IDropHandler> DndDropHandlerProperty =
    AvaloniaProperty.RegisterDirect<EditableModelCollectionView, IDropHandler>(
      nameof(DndDropHandler), o => o.DndDropHandler, (o, v) => o.DndDropHandler = v);

  public IDropHandler DndDropHandler
  {
    get => _dndDropHandler;
    set => SetAndRaise(DndDropHandlerProperty, ref _dndDropHandler, value);
  }

  private Control _addView = null!;

  public static readonly DirectProperty<EditableModelCollectionView, Control> AddViewProperty =
    AvaloniaProperty.RegisterDirect<EditableModelCollectionView, Control>(
      nameof(AddView), o => o.AddView, (o, v) => o.AddView = v);

  public Control AddView
  {
    get => _addView;
    set => SetAndRaise(AddViewProperty, ref _addView, value);
  }

  private DataGridDeleteColumn _deleteColumn;

  public EditableModelCollectionView()
  {
    InitializeComponent();
    _deleteColumn = new DataGridDeleteColumn { DeleteCommand = DeleteCommand };
    Columns.CollectionChanged += Columns_CollectionChanged;
    DataGrid.Columns.Add(new DataGridDndIconColumn());
    DataGrid.Columns.Add(_deleteColumn);
  }

  private void Columns_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
  {
    if (e.Action != NotifyCollectionChangedAction.Add || e.NewItems is null)
      return;

    DataGrid.Columns.Add((DataGridColumn)e.NewItems[0]!);

    _deleteColumn.DisplayIndex = DataGrid.Columns.Count - 1;
  }

  public void Dispose()
  {
    Columns.CollectionChanged -= Columns_CollectionChanged;
  }
}
