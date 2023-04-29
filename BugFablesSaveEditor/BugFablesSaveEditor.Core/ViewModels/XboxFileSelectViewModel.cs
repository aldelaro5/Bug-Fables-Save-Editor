using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DialogHostAvalonia;

namespace BugFablesSaveEditor.Core.ViewModels;

public partial class XboxFileSelectViewModel : ObservableObject
{
  public int ResultIndex { get; private set; }

  [ObservableProperty]
  private string[] _fileNames;

  [ObservableProperty]
  private bool _selectedFile0 = true;

  [ObservableProperty]
  private bool _selectedFile1;

  [ObservableProperty]
  private bool _selectedFile2;

  [ObservableProperty]
  private bool _file0Exists;

  [ObservableProperty]
  private bool _file1Exists;

  [ObservableProperty]
  private bool _file2Exists;

  public XboxFileSelectViewModel(string[] fileNames)
  {
    _file0Exists = !string.IsNullOrEmpty(fileNames[0]);
    _file1Exists = !string.IsNullOrEmpty(fileNames[1]);
    _file2Exists = !string.IsNullOrEmpty(fileNames[2]);
    _fileNames = fileNames.Select(x => string.IsNullOrEmpty(x) ? "No File" : x).ToArray();
  }

  [RelayCommand]
  private void Confirm()
  {
    ResultIndex = SelectedFile0 ? 0 : SelectedFile1 ? 1 : 2;
    DialogHost.Close(Utils.DialogSessionName);
  }
}
