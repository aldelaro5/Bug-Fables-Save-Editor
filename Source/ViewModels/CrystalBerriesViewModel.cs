using System;
using System.Text;
using Avalonia.Collections;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using static BugFablesSaveEditor.BugFablesSave.Sections.CrystalBerries;

namespace BugFablesSaveEditor.ViewModels;

public class CrystalBerriesViewModel : ViewModelBase
{
  private CrystalBerry[] _crystalBerries;

  private DataGridCollectionView _crystalBerriesFiltered;
  private SaveData _saveData;

  private string _textFilter;

  private string[] Areas;

  public CrystalBerriesViewModel()
  {
    SaveData = new SaveData();
    Initialise();
  }

  public CrystalBerriesViewModel(SaveData saveData)
  {
    SaveData = saveData;
    Initialise();
  }

  public SaveData SaveData
  {
    get => _saveData;
    set
    {
      _saveData = value;
      this.RaisePropertyChanged();
    }
  }

  public CrystalBerry[] CrystalBerries
  {
    get => _crystalBerries;
    set
    {
      _crystalBerries = value;
      this.RaisePropertyChanged();
    }
  }

  public DataGridCollectionView CrystalBerriesFiltered
  {
    get => _crystalBerriesFiltered;
    set
    {
      _crystalBerriesFiltered = value;
      this.RaisePropertyChanged();
    }
  }

  public string TextFilter
  {
    get => _textFilter;
    set
    {
      _textFilter = value;
      this.RaisePropertyChanged();
      CrystalBerriesFiltered.Refresh();
    }
  }

  private void Initialise()
  {
    Areas = Common.GetEnumDescriptions<Area>();
    CrystalBerries = (CrystalBerry[])SaveData.Sections[SaveFileSection.CrystalBerries].Data;
    CrystalBerriesFiltered = new DataGridCollectionView(CrystalBerries);
    CrystalBerriesFiltered.Filter = FilterCrystalBerries;
  }

  private bool FilterCrystalBerries(object arg)
  {
    if (string.IsNullOrEmpty(TextFilter))
    {
      return true;
    }

    CrystalBerry crystalBerry = (CrystalBerry)arg;
    StringBuilder sb = new();
    sb.Append(crystalBerry.Index).Append(' ');
    sb.Append(Areas[(int)crystalBerry.Area]).Append(' ');
    sb.Append(crystalBerry.Description);
    return sb.ToString().Contains(TextFilter, StringComparison.OrdinalIgnoreCase);
  }

  public void ToggleAllFiltered()
  {
    bool newObtained = true;
    foreach (object? item in CrystalBerriesFiltered)
    {
      CrystalBerry cb = (CrystalBerry)item;
      if (cb.Obtained)
      {
        newObtained = false;
        break;
      }
    }

    foreach (object? item in CrystalBerriesFiltered)
    {
      CrystalBerry cb = (CrystalBerry)item;
      cb.Obtained = newObtained;
    }
  }
}
