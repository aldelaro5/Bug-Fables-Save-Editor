using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Humanizer;

namespace BugFablesSaveEditor.Views;

public partial class ReorderableCollectionView : UserControl
{
  public static readonly StyledProperty<List<string>?> ShownColumnsProperty =
    AvaloniaProperty.Register<ReorderableCollectionView, List<string>?>(nameof(ShownColumns));

  private bool _setFirstColumnToEnd;

  public List<string>? ShownColumns
  {
    get { return GetValue(ShownColumnsProperty); }
    set { SetValue(ShownColumnsProperty, value); }
  }

  public ReorderableCollectionView()
  {
    InitializeComponent();
  }

  private void DataGrid_OnAutoGeneratingColumn(object? sender,
                                               DataGridAutoGeneratingColumnEventArgs e)
  {
    // If we are generating columns, we want to have LayoutUpdated move the first column
    // (delete) to the end, but only once
    _setFirstColumnToEnd = false;

    if (ShownColumns?.Count > 0 && !ShownColumns.Contains(e.PropertyName))
    {
      e.Cancel = true;
      return;
    }

    // CheckBox for bool
    if (e.PropertyType == typeof(bool))
    {
      var dataTemplate = new FuncDataTemplate(typeof(object), (_, _) =>
        new CheckBox
        {
          Margin = new Thickness(35, 0, 0, 0),
          [!ToggleButton.IsCheckedProperty] = new Binding(e.PropertyName)
        }
      );
      e.Column = new DataGridTemplateColumn { CellTemplate = dataTemplate };
    }
    // ComboBox for enums
    else if (e.PropertyType.IsEnum)
    {
      var dataTemplate = new FuncDataTemplate(typeof(object), (_, _) =>
        new ComboBox
        {
          [!WidthProperty] = new Binding("$parent.Bounds.Width"),
          [!SelectingItemsControl.SelectedIndexProperty] = new Binding(e.PropertyName)
          {
            Converter = new EnumValueConverter(), ConverterParameter = e.PropertyType
          },
          Items = e.PropertyType.GetEnumDescriptions()
        });
      e.Column =
        new DataGridTemplateColumn { Width = new DataGridLength(200), CellTemplate = dataTemplate };
    }
    // Text for everything else
    else
    {
      var column = new DataGridTextColumn { Width = new DataGridLength(200) };
      e.Column = column;
      column.Binding = new Binding(e.PropertyName);
    }

    e.Column.Header = e.PropertyName.Humanize(LetterCasing.Title);
  }

  private void DataGrid_OnLayoutUpdated(object? sender, EventArgs e)
  {
    if (_setFirstColumnToEnd)
      return;

    // At this point, the auto generated columns are in, we can safely move the first columnd
    // (delete) to the end
    DataGrid dataGrid = (DataGrid)sender!;
    var column = dataGrid.Columns[0];
    column.DisplayIndex = dataGrid.Columns.Count - 1;

    _setFirstColumnToEnd = true;
  }

  // Avalonia 11 preview 4 has issues where it's not possible to force the user to double click
  // the ComboBox to pop the menu so we are forcing it by first listening to menu open property
  // changes on the ComboBox and when it does pop open, we force it back to false
  private void DataGrid_OnCellPointerPressed(object? sender, DataGridCellPointerPressedEventArgs e)
  {
    DataGrid dataGrid = (DataGrid)sender!;
    if (e.Cell.Content is ComboBox cmb && dataGrid.SelectedIndex != e.Row.GetIndex())
      cmb.PropertyChanged += CmbOnPropertyChanged;
  }

  private void CmbOnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
  {
    if (e.Property.Name != ComboBox.IsDropDownOpenProperty.Name)
      return;

    var comboBox = (ComboBox)sender!;
    comboBox.IsDropDownOpen = false;

    // This even is no longer needed so we can allow the user to pop the menu next click
    comboBox.PropertyChanged -= CmbOnPropertyChanged;
  }
}
