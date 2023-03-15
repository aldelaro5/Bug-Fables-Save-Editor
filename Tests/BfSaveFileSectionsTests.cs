using System.Globalization;
using BugFablesSaveEditor;
using Xunit;
using static BugFablesLib.BfSaveFile;

namespace BugFablesLib.Tests;

public class SaveSectionsTests
{
  private const string DecodedSaveFileName = "SaveFiles/DecodedTextSave.txt";

  private readonly string[] saveLines = File.ReadAllLines(DecodedSaveFileName);

  public static IEnumerable<object[]> AllSaveFileSections()
  {
    for (int i = 0; i < (int)SaveFileSection.COUNT; i++)
    {
      yield return new object[] { (SaveFileSection)i };
    }
  }

  [Theory]
  [MemberData(nameof(AllSaveFileSections))]
  public void Section_ShouldNotChange_WhenOverWrittenWithoutChange(SaveFileSection section)
  {
    // Test messing with the culture to make sure it doesn't break the parsing
    CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
    Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-fr");
    string strBefore = saveLines[(int)section];
    var sud = new BfSaveFile().Sections[(int)section];
    sud.Deserialize(saveLines[(int)section]);
    string strAfter = sud.Serialize();
    Thread.CurrentThread.CurrentCulture = cultureInfo;
    Assert.Equal(strBefore, strAfter);
  }

  [Theory]
  [InlineData(SaveFileSection.Header, Utils.PrimarySeparator)]
  [InlineData(SaveFileSection.PartyMembers, Utils.SecondarySeparator)]
  [InlineData(SaveFileSection.Global, Utils.PrimarySeparator)]
  [InlineData(SaveFileSection.Quests, Utils.SecondarySeparator)]
  [InlineData(SaveFileSection.Items, Utils.SecondarySeparator)]
  [InlineData(SaveFileSection.Medals, Utils.SecondarySeparator)]
  [InlineData(SaveFileSection.SamiraSongs, Utils.SecondarySeparator)]
  [InlineData(SaveFileSection.StatBonuses, Utils.SecondarySeparator)]
  [InlineData(SaveFileSection.Library, Utils.SecondarySeparator)]
  [InlineData(SaveFileSection.EnemyEncounters, Utils.SecondarySeparator)]
  public void SectionParsing_ShouldThrow_WhenIncorrectFieldsAmount(
    SaveFileSection section, string separator)
  {
    string sectionData = saveLines[(int)section];
    var sud = new BfSaveFile().Sections[(int)section];
    // One field too much
    sectionData += $"{separator}0";
    Assert.ThrowsAny<Exception>(() => sud.Deserialize(sectionData));
    // One field too low
    sectionData = sectionData.Split(separator).Skip(2)
      .Aggregate((acc, field) => $"{acc}{separator}{field}");
    Assert.ThrowsAny<Exception>(() => sud.Deserialize(sectionData));
  }
}
