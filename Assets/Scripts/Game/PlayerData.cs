using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int levelIsPlaying;
    public int highestLevel;
    public List<Level> levels;
    public PlayerData(Progress progress)
    {
        levelIsPlaying = progress.levelIsPlaying;
        highestLevel = progress.highestLevel;
        levels = progress.levels;
    }
    public PlayerData()
    {

    }

}
