using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public int threeStarsCondition;
    public int twoStarsCondition;
    public int totalShuriken;
    public bool isGameOver;
    public int balls;

    public Image popUpWindow;
    public TextMeshProUGUI gameStatus;
    public TextMeshProUGUI shurikensText;
    public List<GameObject> stars;
    public Button nextLevelButton;

    private int levelIsPlaying;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        levelIsPlaying = SceneManager.GetActiveScene().buildIndex;
        UnlockNewLevel(levelIsPlaying); //Create item in list if not exist yet.

        //Prepare for the game
        if (totalShuriken == 0)
        {
            totalShuriken = 3;
        }
        UpdateShurikenText();
        balls = 0;
        isGameOver = false;
        BackgroundMusic.instance.PlayGameMusic(); //Play music in game
    }

    public void CheckGameStatus()
    {
        if (balls <= 0)
        {
            LevelComplete();
        }
        else if (totalShuriken <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        gameStatus.text = "Fail!";
        popUpWindow.gameObject.SetActive(true);
    }

    public void LevelComplete()
    {
        isGameOver = true;
        gameStatus.text = "Complete!";
        //Calculate star acquired;
        int stars;
        if (totalShuriken >= threeStarsCondition)
        {
            stars = 3;
        }
        else if (totalShuriken >= twoStarsCondition)
        {
            stars = 2;
        }
        else
        {
            stars = 1;
        }
        //Set stars on the UI
        SetStars(stars);
        //Save highscore if it is the best
        SaveNewProgress(stars);
        //Show next level button
        if (levelIsPlaying < Progress.instance.totalLevel)
        {
            UnlockNewLevel(levelIsPlaying + 1);
            nextLevelButton.gameObject.SetActive(true);
        }
        //Unlock next level
        
        //Show result panel
        popUpWindow.gameObject.SetActive(true);
    }

    private void SetStars(int totalStar) //Set stars on the result panel
    {
        for (int i = 0; i < totalStar; i++)
        {
            stars[i].GetComponent<Star>().AcquireStar();
        }
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateShurikenText()
    {
        shurikensText.text = "Shuriken: " + totalShuriken;
    }

    public void BackToSelectLevel() //Back to select level menu
    {
        BackgroundMusic.instance.StopGameMusic();
        SceneManager.LoadScene("Level Select", LoadSceneMode.Single);
    }

    public void NextLevelButtonClicked() //Go to next level
    {
        SceneManager.LoadScene(levelIsPlaying + 1, LoadSceneMode.Single);
    }

    private void SaveNewProgress(int stars)
    {
        //Save the new record
        if (Progress.instance.levels[levelIsPlaying -1].stars < stars)
        {
            Progress.instance.levels[levelIsPlaying -1].stars = stars;
            Progress.instance.SaveProgress();
        }
    }
    //Create and unlock new level in levels list in player progression
    private void UnlockNewLevel(int level)
    {
        if (SearchForLevel(level)) //Check if level is existed
        {
            Level lv = new Level();
            lv.levelNumber = level;
            lv.isUnlock = true;
            lv.stars = 0;
            Progress.instance.levels.Add(lv);
            if (Progress.instance.highestLevel < level)
            {
                Progress.instance.highestLevel = level;
            }
        }
        Progress.instance.levelIsPlaying = level;
        Progress.instance.SaveProgress();
    }

    private bool SearchForLevel(int levelNumber)
    {
        foreach(Level lv in Progress.instance.levels)
        {
            if (lv.levelNumber == levelNumber)
            {
                return false;
            }
        }
        return true;
    }
}
