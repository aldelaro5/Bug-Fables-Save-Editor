using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.LogicalTree;
using Avalonia.Xaml.Interactions.DragAndDrop;

namespace BugFablesSaveEditor.Behaviors;

public class DataGridDragHandlder : IDragHandler
{
  public static DataGrid? CurrentDndSourceDataGrid { get; set; }

  public void BeforeDragDrop(object? sender, PointerEventArgs e, object? context)
  {
    var dataGrid = (sender as DataGridRow)?.GetLogicalAncestors().OfType<DataGrid>().FirstOrDefault();
    if (dataGrid is not null)
      CurrentDndSourceDataGrid ??= dataGrid;
  }

  public void AfterDragDrop(object? sender, PointerEventArgs e, object? context)
  {
  }
}
