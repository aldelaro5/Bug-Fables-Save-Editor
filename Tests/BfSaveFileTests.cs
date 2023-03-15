using Xunit;

namespace BugFablesLib.Tests;

public class SaveDataTests
{
  private const string ValidSaveFileName = "SaveFiles/ValidSave.dat";
  private const string InvalidSaveFileName = "SaveFiles/InvalidSave.dat";

  private readonly BfPcSaveData _sud = new();

  [Fact]
  public void LoadFromFile_ShouldThrow_WhenFileDataIsEmpty()
  {
    Assert.ThrowsAny<Exception>(() => _sud.LoadFromString(""));
  }

  [Fact]
  public void LoadFromFile_ShouldThrow_WhenDataIsInvalid()
  {
    Assert.ThrowsAny<Exception>(() => _sud.LoadFromString(File.ReadAllText(InvalidSaveFileName)));
  }

  [Fact]
  public void LoadFromFile_ShouldLoadFile_WhenDataIsValid()
  {
    _sud.LoadFromString(File.ReadAllText(ValidSaveFileName));
  }

  [Fact]
  public void SaveToFile_ShouldNotChangeFile_WhenOverwritten()
  {
    string textBefore = File.ReadAllText(ValidSaveFileName);
    _sud.LoadFromString(textBefore);
    string textAfter = _sud.EncodeToString();
    Assert.Equal(textBefore, textAfter);
  }

  [Fact]
  public void SaveToFile_ShouldChangeFile_WhenOverwrittenWithChange()
  {
    string textBefore = File.ReadAllText(ValidSaveFileName);
    _sud.LoadFromString(textBefore);
    _sud.Global.Rank = 50;
    string textAfter = _sud.EncodeToString();
    Assert.NotEqual(textBefore, textAfter);
  }
}
