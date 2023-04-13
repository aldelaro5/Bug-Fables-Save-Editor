using System.Collections.Generic;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using BugFablesSaveEditor.Models;

namespace BugFablesSaveEditor.Views;

public partial class BfNamedIdCollectionView : UserControl
{
  private ICommand _deleteCommand = null!;

  public static readonly DirectProperty<BfNamedIdCollectionView, ICommand> DeleteCommandProperty =
    AvaloniaProperty.RegisterDirect<BfNamedIdCollectionView, ICommand>(
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

  public static readonly DirectProperty<BfNamedIdCollectionView, ICommand> AddCommandProperty =
    AvaloniaProperty.RegisterDirect<BfNamedIdCollectionView, ICommand>(
      nameof(AddCommand),
      o => o.AddCommand,
      (o, v) => o.AddCommand = v,
      defaultBindingMode: BindingMode.OneWay);

  public ICommand AddCommand
  {
    get => _addCommand;
    set => SetAndRaise(AddCommandProperty, ref _addCommand, value);
  }

  private BfNamedIdModel _newNamedId = null!;

  public static readonly DirectProperty<BfNamedIdCollectionView, BfNamedIdModel> NewNamedIdProperty =
    AvaloniaProperty.RegisterDirect<BfNamedIdCollectionView, BfNamedIdModel>(
      nameof(NewNamedId),
      o => o.NewNamedId,
      (o, v) => o.NewNamedId = v,
      defaultBindingMode: BindingMode.OneWay);

  public BfNamedIdModel NewNamedId
  {
    get => _newNamedId;
    set => SetAndRaise(NewNamedIdProperty, ref _newNamedId, value);
  }

  private IList<BfNamedIdModel> _namedIdsCollection = null!;

  public static readonly DirectProperty<BfNamedIdCollectionView, IList<BfNamedIdModel>> NamedIdsCollectionProperty =
    AvaloniaProperty.RegisterDirect<BfNamedIdCollectionView, IList<BfNamedIdModel>>(
      nameof(NamedIdsCollection),
      o => o.NamedIdsCollection,
      (o, v) => o.NamedIdsCollection = v,
      defaultBindingMode: BindingMode.OneWay);

  public IList<BfNamedIdModel> NamedIdsCollection
  {
    get => _namedIdsCollection;
    set => SetAndRaise(NamedIdsCollectionProperty, ref _namedIdsCollection, value);
  }

  private string _titleLabel = "";

  public static readonly DirectProperty<BfNamedIdCollectionView, string> TitleLabelProperty =
    AvaloniaProperty.RegisterDirect<BfNamedIdCollectionView, string>(
      nameof(TitleLabel),
      o => o.TitleLabel,
      (o, v) => o.TitleLabel = v,
      defaultBindingMode: BindingMode.OneTime);

  public string TitleLabel
  {
    get => _titleLabel;
    set => SetAndRaise(TitleLabelProperty, ref _titleLabel, value);
  }

  public BfNamedIdCollectionView()
  {
    InitializeComponent();
  }
}
