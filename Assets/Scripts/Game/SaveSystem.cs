using UnityEngine;
using System.IO;
using Newtonsoft.Json;
public class SaveSystem
{
    public static void SaveProgress(Progress progress)
    {
        string path = Application.persistentDataPath + "/progress.json";

        PlayerData data = new PlayerData(progress);

        string json = JsonConvert.SerializeObject(data);

        File.WriteAllText(path, json);
        Debug.Log("Saved!");

    }

    public static PlayerData LoadProgress()
    {
        string path = Application.persistentDataPath + "/progress.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = new PlayerData();
            data = JsonConvert.DeserializeObject<PlayerData>(json);
            Debug.Log("Loaded From File");
            return data;
        }
        else
        {
            return null;
        }
        
    }
}
