using System.IO;
using UnityEngine;
using System;

public class SaveSystem
{
    public static SaveSystem Instance = new SaveSystem();
    public static GameData data = new GameData();
    public static LevelState levelState = new LevelState();
    public void SaveData()
    {
        string json = JsonUtility.ToJson(data,true);

        string directoryPath = Path.Combine(Application.persistentDataPath, "SaveFile");
        string filePath = Path.Combine(directoryPath, "Save.json");

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        File.WriteAllText(filePath, json);
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveFile/Save.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/SaveFile/Save.json");
            data = JsonUtility.FromJson<GameData>(json);
        }
    }
}
