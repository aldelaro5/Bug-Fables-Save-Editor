using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Metadata;
using Avalonia.Styling;

namespace BugFablesSaveEditor;

public class DataGridColumnsTemplate : ITemplate
{
  [Content]
  [TemplateContent(TemplateResultType = typeof(ObservableCollection<DataGridColumn>))]
  public object? Content { get; set; }

  object ITemplate.Build() => TemplateContent.Load<ObservableCollection<DataGridColumn>>(Content).Result;
}
