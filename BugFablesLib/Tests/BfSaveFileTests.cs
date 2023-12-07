using Xunit;

namespace BugFablesLib.Tests;

public class SaveDataTests
{
  private const string ValidPcSaveFileName = "SaveFiles/PcValidSave.dat";
  private const string ValidSwitchSaveFileName = "SaveFiles/SwitchValidSave.dat";
  private const string XboxAllValidFileName = "SaveFiles/XboxAllValidSaves";
  private const string XboxOnlyFirstValidXboxFileName = "SaveFiles/XboxOnlyFirstValidSaveFile";
  private const string XboxMiddleSaveBlankFileName = "SaveFiles/XboxMiddleSaveBlank";

  private readonly BfSaveData _sud = new();

  public static IEnumerable<object[]> AllSavesFormat(bool includeFileNane)
  {
    if (includeFileNane)
    {
      yield return new object[] { BfSaveData.PcSaveDataFormat, ValidPcSaveFileName };
      yield return new object[] { BfSaveData.SwitchSaveDataFormat, ValidSwitchSaveFileName };
      yield return new object[] { new BfXboxPcSaveDataFormat(_ => Task.FromResult(0)), XboxAllValidFileName };
      yield return new object[] { new BfXboxPcSaveDataFormat(_ => Task.FromResult(1)), XboxAllValidFileName };
      yield return new object[] { new BfXboxPcSaveDataFormat(_ => Task.FromResult(2)), XboxAllValidFileName };
      yield return new object[] { new BfXboxPcSaveDataFormat(_ => Task.FromResult(0)), XboxOnlyFirstValidXboxFileName };
      yield return new object[] { new BfXboxPcSaveDataFormat(_ => Task.FromResult(0)), XboxMiddleSaveBlankFileName };
      yield return new object[] { new BfXboxPcSaveDataFormat(_ => Task.FromResult(2)), XboxMiddleSaveBlankFileName };
    }
    else
    {
      yield return new object[] { BfSaveData.PcSaveDataFormat };
      yield return new object[] { BfSaveData.SwitchSaveDataFormat };
      yield return new object[] { new BfXboxPcSaveDataFormat(_ => Task.FromResult(0)) };
    }
  }

  [Theory]
  [MemberData(nameof(AllSavesFormat), false)]
  public async void LoadFromBytes_ShouldThrow_WhenFileDataIsEmpty(IBfSaveFileFormat saveFileFormat)
  {
    await Assert.ThrowsAnyAsync<Exception>(async () => await _sud.LoadFromBytes(Array.Empty<byte>(), saveFileFormat));
  }

  [Theory]
  [MemberData(nameof(AllSavesFormat), false)]
  public async void LoadFromBytes_ShouldThrow_WhenDataIsInvalid(IBfSaveFileFormat saveFileFormat)
  {
    byte[] randomBytes = new byte[1_000_000];
    Random.Shared.NextBytes(randomBytes);
    await Assert.ThrowsAnyAsync<Exception>(async () => await _sud.LoadFromBytes(randomBytes, saveFileFormat));
  }

  [Theory]
  [MemberData(nameof(AllSavesFormat), true)]
  public async void LoadFromBytes_ShouldLoadFile_WhenDataIsValid(IBfSaveFileFormat saveFileFormat, string fileName)
  {
    await _sud.LoadFromBytes(await File.ReadAllBytesAsync(fileName), saveFileFormat);
  }

  [Theory]
  [MemberData(nameof(AllSavesFormat), true)]
  public async void EncodeToBytes_ShouldNotChangeData_WhenEncodedBack(IBfSaveFileFormat saveFileFormat, string fileName)
  {
    byte[] bytesBefore = await File.ReadAllBytesAsync(fileName);
    await _sud.LoadFromBytes(bytesBefore, saveFileFormat);
    byte[] bytesAfter = await _sud.EncodeToBytes(saveFileFormat);
    Assert.True(bytesBefore.SequenceEqual(bytesAfter));
  }

  [Theory]
  [MemberData(nameof(AllSavesFormat), true)]
  public async void EncodeToBytes_ShouldChangeData_WhenEncodedWithChange(
    IBfSaveFileFormat saveFileFormat, string fileName)
  {
    byte[] bytesBefore = await File.ReadAllBytesAsync(fileName);
    await _sud.LoadFromBytes(bytesBefore, saveFileFormat);
    _sud.Global.Rank = 50;
    byte[] bytesAfter = await _sud.EncodeToBytes(saveFileFormat);
    Assert.False(bytesBefore.SequenceEqual(bytesAfter));
  }

  [Theory]
  [InlineData(-1)]
  [InlineData(4)]
  [InlineData(1)]
  public async void XboxPcSaveDataFormat_ShouldThrow_WhenCallbackReturnsAnInvalidIndex(int index)
  {
    await Assert.ThrowsAnyAsync<Exception>(async () =>
    {
      BfXboxPcSaveDataFormat format = new(_ => Task.FromResult(index));
      byte[] bytes = await File.ReadAllBytesAsync(XboxMiddleSaveBlankFileName);
      await _sud.LoadFromBytes(bytes, format);
      await _sud.EncodeToBytes(format);
    });
  }
}
