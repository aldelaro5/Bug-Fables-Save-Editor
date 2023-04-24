using Xunit;

namespace BugFablesLib.Tests;

public class SaveDataTests
{
  private const string ValidPcSaveFileName = "SaveFiles/PcValidSave.dat";
  private const string ValidSwitchSaveFileName = "SaveFiles/SwitchValidSave.dat";

  public static IEnumerable<object[]> AllSavesFormat(bool includeFileNane)
  {
    if (includeFileNane)
    {
      yield return new object[] { new BfPcSaveData(), ValidPcSaveFileName };
      yield return new object[] { new BfSwitchSaveData(), ValidSwitchSaveFileName };
    }
    else
    {
      yield return new object[] { new BfPcSaveData() };
      yield return new object[] { new BfSwitchSaveData() };
    }
  }

  [Theory]
  [MemberData(nameof(AllSavesFormat), false)]
  public void LoadFromBytes_ShouldThrow_WhenFileDataIsEmpty(BfSaveData saveData)
  {
    Assert.ThrowsAny<Exception>(() => saveData.LoadFromBytes(new byte[] { }));
  }

  [Theory]
  [MemberData(nameof(AllSavesFormat), false)]
  public void LoadFromBytes_ShouldThrow_WhenDataIsInvalid(BfSaveData saveData)
  {
    byte[] randomBytes = new byte[1_000_000];
    Random.Shared.NextBytes(randomBytes);
    Assert.ThrowsAny<Exception>(() => saveData.LoadFromBytes(randomBytes));
  }

  [Theory]
  [MemberData(nameof(AllSavesFormat), true)]
  public void LoadFromBytes_ShouldLoadFile_WhenDataIsValid(BfSaveData saveData, string fileName)
  {
    saveData.LoadFromBytes(File.ReadAllBytes(fileName));
  }

  [Theory]
  [MemberData(nameof(AllSavesFormat), true)]
  public void EncodeToBytes_ShouldNotChangeData_WhenEncodedBack(BfSaveData saveData, string fileName)
  {
    byte[] bytesBefore = File.ReadAllBytes(fileName);
    saveData.LoadFromBytes(bytesBefore);
    byte[] bytesAfter = saveData.EncodeToBytes();
    Assert.True(bytesBefore.SequenceEqual(bytesAfter));
  }

  [Theory]
  [MemberData(nameof(AllSavesFormat), true)]
  public void EncodeToBytes_ShouldChangeData_WhenEncodedWithChange(BfSaveData saveData, string fileName)
  {
    byte[] bytesBefore = File.ReadAllBytes(fileName);
    saveData.LoadFromBytes(bytesBefore);
    saveData.Global.Rank = 50;
    byte[] bytesAfter = saveData.EncodeToBytes();
    Assert.False(bytesBefore.SequenceEqual(bytesAfter));
  }
}
