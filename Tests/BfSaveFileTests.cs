using Xunit;

namespace BugFablesLib.Tests;

public class SaveDataTests
{
  private const string ValidSaveFileName = "SaveFiles/ValidSave.dat";
  private const string InvalidSaveFileName = "SaveFiles/InvalidSave.dat";

  private readonly BfSaveFile _sud = new();

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
    string tempFilePath = Path.GetTempFileName();
    _sud.SaveToFile(tempFilePath);
    string textAfter = File.ReadAllText(tempFilePath);
    File.Delete(tempFilePath);
    Assert.Equal(textBefore, textAfter);
  }

  [Fact]
  public void SaveToFile_ShouldChangeFile_WhenOverwrittenWithChange()
  {
    string textBefore = File.ReadAllText(ValidSaveFileName);
    _sud.LoadFromFile(ValidSaveFileName);
    _sud.Global.Rank = 50;
    string tempFilePath = Path.GetTempFileName();
    _sud.SaveToFile(tempFilePath);
    string textAfter = File.ReadAllText(tempFilePath);
    Assert.NotEqual(textBefore, textAfter);
    File.Delete(tempFilePath);
  }
}
