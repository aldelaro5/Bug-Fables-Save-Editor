using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.BugFablesSave.Sections;
using Xunit;

namespace BugFablesSaveEditor.Tests;

public class SaveDataTests
{
  private const string ValidSaveFileName = "SaveFiles/ValidSave.dat";
  private const string InvalidSaveFileName = "SaveFiles/InvalidSave.dat";

  private readonly SaveData _sud = new();

  [Fact]
  public void LoadFromFile_ShouldThrow_WhenFileNameIsEmpty()
  {
    Assert.ThrowsAny<Exception>(() => _sud.LoadFromFile(""));
  }

  [Fact]
  public void LoadFromFile_ShouldThrow_WhenFileNameDoesNotExist()
  {
    Assert.ThrowsAny<Exception>(() => _sud.LoadFromFile("NotExist.dat"));
  }

  [Fact]
  public void LoadFromFile_ShouldThrow_WhenFileIsInvalid()
  {
    Assert.ThrowsAny<Exception>(() => _sud.LoadFromFile(InvalidSaveFileName));
  }

  [Fact]
  public void LoadFromFile_ShouldLoadFile_WhenFileIsValid()
  {
    _sud.LoadFromFile(ValidSaveFileName);
  }

  [Fact]
  public void SaveToFile_ShouldThrow_WhenFileNameIsEmpty()
  {
    _sud.LoadFromFile(ValidSaveFileName);
    Assert.ThrowsAny<Exception>(() => _sud.SaveToFile(""));
  }

  [Fact]
  public void SaveToFile_ShouldNotChangeFile_WhenOverwritten()
  {
    string textBefore = File.ReadAllText(ValidSaveFileName);
    _sud.LoadFromFile(ValidSaveFileName);
    string tempFilePath = "test.dat";
    _sud.SaveToFile(tempFilePath);
    string textAfter = File.ReadAllText(tempFilePath);
    Assert.Equal(textBefore, textAfter);
    File.Delete(tempFilePath);
  }

  [Fact]
  public void SaveToFile_ShouldChangeFile_WhenOverwrittenWithChange()
  {
    string textBefore = File.ReadAllText(ValidSaveFileName);
    _sud.LoadFromFile(ValidSaveFileName);
    ((Global.GlobalInfo)_sud.Sections[SaveFileSection.Global].Data).Rank = 50;
    string tempFilePath = Path.GetTempFileName();
    string textAfter;
    using (File.CreateText(tempFilePath))
    {
      _sud.SaveToFile(tempFilePath);
      textAfter = File.ReadAllText(tempFilePath);
    }

    Assert.NotEqual(textBefore, textAfter);
  }
}
