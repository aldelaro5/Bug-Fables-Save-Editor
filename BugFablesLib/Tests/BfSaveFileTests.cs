using System.Text;
using Xunit;

namespace BugFablesLib.Tests;

public class PcSaveDataTests
{
  private const string ValidSaveFileName = "SaveFiles/ValidSave.dat";

  private readonly BfPcSaveData _sud = new();

  [Fact]
  public void LoadFromBytes_ShouldThrow_WhenFileDataIsEmpty()
  {
    Assert.ThrowsAny<Exception>(() => _sud.LoadFromBytes(new byte[] { }));
  }

  [Fact]
  public void LoadFromBytes_ShouldThrow_WhenDataIsInvalid()
  {
    byte[] randomBytes = new byte[1_000_000];
    Random.Shared.NextBytes(randomBytes);
    Assert.ThrowsAny<Exception>(() => _sud.LoadFromBytes(randomBytes));
  }

  [Fact]
  public void LoadFromBytes_ShouldLoadFile_WhenDataIsValid()
  {
    _sud.LoadFromBytes(File.ReadAllBytes(ValidSaveFileName));
  }

  [Fact]
  public void EncodeToBytes_ShouldNotChangeData_WhenEncodedBack()
  {
    byte[] bytes = File.ReadAllBytes(ValidSaveFileName);
    string textBefore = Encoding.UTF8.GetString(bytes);
    _sud.LoadFromBytes(bytes);
    string textAfter = Encoding.UTF8.GetString(_sud.EncodeToBytes());
    Assert.Equal(textBefore, textAfter);
  }

  [Fact]
  public void EncodeToBytes_ShouldChangeData_WhenEncodedWithChange()
  {
    byte[] bytes = File.ReadAllBytes(ValidSaveFileName);
    string textBefore = Encoding.UTF8.GetString(bytes);
    _sud.LoadFromBytes(bytes);
    _sud.Global.Rank = 50;
    string textAfter = Encoding.UTF8.GetString(_sud.EncodeToBytes());
    Assert.NotEqual(textBefore, textAfter);
  }
}
