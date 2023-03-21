using System.Text;
using Xunit;

namespace BugFablesLib.Tests;

public class PcSaveDataTests
{
  private const string ValidSaveFileName = "SaveFiles/ValidSave.dat";

  private readonly BfPcSaveData _sud = new();

  [Fact]
  public void LoadFromString_ShouldThrow_WhenFileDataIsEmpty()
  {
    Assert.ThrowsAny<Exception>(() => _sud.LoadFromString(string.Empty));
  }

  [Fact]
  public void LoadFromString_ShouldThrow_WhenDataIsInvalid()
  {
    byte[] randomBytes = new byte[1_000_000];
    Random.Shared.NextBytes(randomBytes);
    Assert.ThrowsAny<Exception>(() => _sud.LoadFromString(Encoding.UTF8.GetString(randomBytes)));
  }

  [Fact]
  public void LoadFromString_ShouldLoadFile_WhenDataIsValid()
  {
    _sud.LoadFromString(File.ReadAllText(ValidSaveFileName));
  }

  [Fact]
  public void EncodeToString_ShouldNotChangeData_WhenEncodedBack()
  {
    string textBefore = File.ReadAllText(ValidSaveFileName);
    _sud.LoadFromString(textBefore);
    string textAfter = _sud.EncodeToString();
    Assert.Equal(textBefore, textAfter);
  }

  [Fact]
  public void EncodeToString_ShouldChangeData_WhenEncodedWithChange()
  {
    string textBefore = File.ReadAllText(ValidSaveFileName);
    _sud.LoadFromString(textBefore);
    _sud.Global.Rank = 50;
    string textAfter = _sud.EncodeToString();
    Assert.NotEqual(textBefore, textAfter);
  }
}
