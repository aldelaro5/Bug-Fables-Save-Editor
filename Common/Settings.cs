using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace BugFablesSaveEditor
{
  public class Settings
  {
    public bool ShowStartupWarning { get; set; } = true;

    public Settings()
    {

    }
  }

  public static class SettingsManager
  {
    private const string settingsFilePath = "setting.json";
    public static Settings Settings = new Settings();

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
      Settings = JsonSerializer.Deserialize<Settings>(json);
    }

    public static void Save()
    {
      string json = JsonSerializer.Serialize(Settings);
      File.WriteAllText(settingsFilePath, json);
    }
  }
}
