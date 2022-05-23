using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public static Progress instance;

    public int totalLevel = 10;
    public int levelIsPlaying = 1;
    public int highestLevel = 1;
    public List<Level> levels = new List<Level>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

        }
        DontDestroyOnLoad(gameObject);
    }

    public void SaveProgress()
    {
        SaveSystem.SaveProgress(this);
    }

    public void LoadProgress()
    {
        PlayerData data = SaveSystem.LoadProgress();
        if (data != null)
        {
            levelIsPlaying = data.levelIsPlaying;
            highestLevel = data.highestLevel;
            levels = data.levels;
        }
    }
}
