using System.Globalization;
using BugFablesSaveEditor;
using Xunit;
using static BugFablesLib.BfSaveData.SaveFileSection;

namespace BugFablesLib.Tests;

public class SaveSectionsTests
{
  private const string DecodedSaveFileName = "SaveFiles/DecodedTextSave.txt";

  private readonly string[] saveLines = File.ReadAllLines(DecodedSaveFileName);

  public static IEnumerable<object[]> AllSaveFileSections()
  {
    for (int i = 0; i < (int)COUNT; i++)
    {
      yield return new object[] { (BfSaveData.SaveFileSection)i };
    }
  }

  [Theory]
  [MemberData(nameof(AllSaveFileSections))]
  public void Section_ShouldNotChange_WhenOverWrittenWithoutChange(
    BfSaveData.SaveFileSection section)
  {
    // Test messing with the culture to make sure it doesn't break the parsing
    CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
    Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-fr");
    string strBefore = saveLines[(int)section];
    var sud = new BfPcSaveData().Data[(int)section];
    sud.Deserialize(saveLines[(int)section]);
    string strAfter = sud.Serialize();
    Thread.CurrentThread.CurrentCulture = cultureInfo;
    Assert.Equal(strBefore, strAfter);
  }

  [Theory]
  [InlineData(Header, Utils.PrimarySeparator)]
  [InlineData(PartyMembers, Utils.SecondarySeparator)]
  [InlineData(Global, Utils.PrimarySeparator)]
  [InlineData(Quests, Utils.SecondarySeparator)]
  [InlineData(Items, Utils.SecondarySeparator)]
  [InlineData(Medals, Utils.SecondarySeparator)]
  [InlineData(SamiraSongs, Utils.SecondarySeparator)]
  [InlineData(StatBonuses, Utils.SecondarySeparator)]
  [InlineData(Library, Utils.SecondarySeparator)]
  [InlineData(EnemyEncounters, Utils.SecondarySeparator)]
  public void SectionParsing_ShouldThrow_WhenIncorrectFieldsAmount(
    BfSaveData.SaveFileSection section, string separator)
  {
    string sectionData = saveLines[(int)section];
    var sud = new BfPcSaveData().Data[(int)section];
    // One field too much
    sectionData += $"{separator}0";
    Assert.ThrowsAny<Exception>(() => sud.Deserialize(sectionData));
    // One field too low
    sectionData = sectionData.Split(separator).Skip(2)
      .Aggregate((acc, field) => $"{acc}{separator}{field}");
    Assert.ThrowsAny<Exception>(() => sud.Deserialize(sectionData));
  }
}
