using Avalonia.Collections;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using System;
using System.Text;
using static BugFablesSaveEditor.BugFablesSave.Sections.CrystalBerries;

namespace BugFablesSaveEditor.ViewModels
{
  public class CrystalBerriesViewModel : ViewModelBase
  {
    private SaveData _saveData;
    public SaveData SaveData
    {
      get { return _saveData; }
      set { _saveData = value; this.RaisePropertyChanged(); }
    }

    private string[] Areas;

    private CrystalBerry[] _crystalBerries;
    public CrystalBerry[] CrystalBerries
    {
      get { return _crystalBerries; }
      set { _crystalBerries = value; this.RaisePropertyChanged(); }
    }

    private DataGridCollectionView _crystalBerriesFiltered;
    public DataGridCollectionView CrystalBerriesFiltered
    {
      get { return _crystalBerriesFiltered; }
      set { _crystalBerriesFiltered = value; this.RaisePropertyChanged(); }
    }

    private string _textFilter;
    public string TextFilter
    {
      get { return _textFilter; }
      set
      {
        _textFilter = value;
        this.RaisePropertyChanged();
        CrystalBerriesFiltered.Refresh();
      }
    }

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

    private void Initialise()
    {
      Areas = CommonUtils.GetEnumDescriptions<Area>();
      CrystalBerries = (CrystalBerry[])SaveData.Sections[SaveFileSection.CrystalBerries].Data;
      CrystalBerriesFiltered = new DataGridCollectionView(CrystalBerries);
      CrystalBerriesFiltered.Filter = FilterCrystalBerries;
    }

    private bool FilterCrystalBerries(object arg)
    {
      if (string.IsNullOrEmpty(TextFilter))
        return true;

      CrystalBerry crystalBerry = (CrystalBerry)arg;
      StringBuilder sb = new StringBuilder();
      sb.Append(crystalBerry.Index).Append(' ');
      sb.Append(Areas[(int)crystalBerry.Area]).Append(' ');
      sb.Append(crystalBerry.Description);
      return sb.ToString().Contains(TextFilter, StringComparison.OrdinalIgnoreCase);
    }

    public void ToggleAllFiltered()
    {
      bool newObtained = true;
      foreach (var item in CrystalBerriesFiltered)
      {
        CrystalBerry cb = (CrystalBerry)item;
        if (cb.Obtained)
        {
          newObtained = false;
          break;
        }
      }

      foreach (var item in CrystalBerriesFiltered)
      {
        CrystalBerry cb = (CrystalBerry)item;
        cb.Obtained = newObtained;
      }
    }
  }
}
