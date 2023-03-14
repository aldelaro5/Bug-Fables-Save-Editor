using System.IO;

namespace BugFablesSaveEditor.Data.Service;

public class BugFablesDataService : IBugFablesDataService
{
  public string[] AnimIds { get; } = GetData(nameof(AnimIds));
  public string[] Areas { get; } = GetData(nameof(Areas));
  public string[] BoardQuests { get; } = GetData(nameof(BoardQuests));
  public string[] Discoveries { get; } = GetData(nameof(Discoveries));
  public string[] Enemies { get; } = GetData(nameof(Enemies));
  public string[] Items { get; } = GetData(nameof(Items));
  public string[] Maps { get; } = GetData(nameof(Maps));
  public string[] Medals { get; } = GetData(nameof(Medals));
  public string[] Musics { get; } = GetData(nameof(Musics));
  public string[] Recipes { get; } = GetData(nameof(Recipes));
  public string[] Records { get; } = GetData(nameof(Records));

  private static string[] GetData(string name) => File.ReadAllLines($"Data/{name}Names/.txt");
}
