using System.IO;
using System.Text.Json;

namespace BugFablesSaveEditor.Utils;

public class Settings
{
  public bool ShowStartupWarning { get; set; } = true;
}

public static class SettingsManager
{
  private const string settingsFilePath = "setting.json";
  public static Settings Settings = new();

  public static void Load()
  {
    if (!File.Exists(settingsFilePath))
    {
      StreamWriter sw = File.CreateText(settingsFilePath);
      sw.Close();
      Save();
      return;
    }

    string json = File.ReadAllText(settingsFilePath);
    Settings = JsonSerializer.Deserialize<Settings>(json)!;
  }

  public static void Save()
  {
    string json = JsonSerializer.Serialize(Settings);
    File.WriteAllText(settingsFilePath, json);
  }
}
