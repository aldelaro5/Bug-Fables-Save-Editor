using Xunit;

namespace BugFablesLib.Tests;

public class SaveDataTests
{
  private const string ValidPcSaveFileName = "SaveFiles/PcValidSave.dat";
  private const string ValidSwitchSaveFileName = "SaveFiles/SwitchValidSave.dat";

  private readonly BfSaveData _sud = new();

  public static IEnumerable<object[]> AllSavesFormat(bool includeFileNane)
  {
    if (includeFileNane)
    {
      yield return new object[] { BfSaveData.PcSaveDataFormat, ValidPcSaveFileName };
      yield return new object[] { BfSaveData.SwitchSaveDataFormat, ValidSwitchSaveFileName };
    }
    else
    {
      yield return new object[] { BfSaveData.PcSaveDataFormat };
      yield return new object[] { BfSaveData.SwitchSaveDataFormat };
    }
  }

  [Theory]
  [MemberData(nameof(AllSavesFormat), false)]
  public void LoadFromBytes_ShouldThrow_WhenFileDataIsEmpty(IBfSaveFileFormat saveFileFormat)
  {
    Assert.ThrowsAny<Exception>(() => _sud.LoadFromBytes(new byte[] { }, saveFileFormat));
  }

  [Theory]
  [MemberData(nameof(AllSavesFormat), false)]
  public void LoadFromBytes_ShouldThrow_WhenDataIsInvalid(IBfSaveFileFormat saveFileFormat)
  {
    byte[] randomBytes = new byte[1_000_000];
    Random.Shared.NextBytes(randomBytes);
    Assert.ThrowsAny<Exception>(() => _sud.LoadFromBytes(randomBytes, saveFileFormat));
  }

  [Theory]
  [MemberData(nameof(AllSavesFormat), true)]
  public void LoadFromBytes_ShouldLoadFile_WhenDataIsValid(IBfSaveFileFormat saveFileFormat, string fileName)
  {
    _sud.LoadFromBytes(File.ReadAllBytes(fileName), saveFileFormat);
  }

  [Theory]
  [MemberData(nameof(AllSavesFormat), true)]
  public void EncodeToBytes_ShouldNotChangeData_WhenEncodedBack(IBfSaveFileFormat saveFileFormat, string fileName)
  {
    byte[] bytesBefore = File.ReadAllBytes(fileName);
    _sud.LoadFromBytes(bytesBefore, saveFileFormat);
    byte[] bytesAfter = _sud.EncodeToBytes(saveFileFormat);
    Assert.True(bytesBefore.SequenceEqual(bytesAfter));
  }

  [Theory]
  [MemberData(nameof(AllSavesFormat), true)]
  public void EncodeToBytes_ShouldChangeData_WhenEncodedWithChange(IBfSaveFileFormat saveFileFormat, string fileName)
  {
    byte[] bytesBefore = File.ReadAllBytes(fileName);
    _sud.LoadFromBytes(bytesBefore, saveFileFormat);
    _sud.Global.Rank = 50;
    byte[] bytesAfter = _sud.EncodeToBytes(saveFileFormat);
    Assert.False(bytesBefore.SequenceEqual(bytesAfter));
  }
}
