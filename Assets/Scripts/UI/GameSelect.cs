using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BackgroundMusic.instance.PlayMenuMusic();
        LoadLevel();
    }

    public void SwitchToLevelScene(int levelIndex)
    {
        BackgroundMusic.instance.StopMenuMusic();
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    private void LoadLevel()
    {
        Color32 brownYellow = new Color32(173, 144, 56, 255);
        List<Level> levels = Progress.instance.levels;
        GameObject[] levelButtons = GameObject.FindGameObjectsWithTag("LevelButton"); //Get all level buttons
        for (int i = 0; i < Progress.instance.highestLevel; i++) //Enable buttons of the level that already unlocked
        {
            Level level = levels[i];
            GameObject levelButton = levelButtons[level.levelNumber-1];
            GameObject button = levelButton.transform.GetChild(0).gameObject;
            GameObject stars = levelButton.transform.GetChild(1).gameObject;
            if (level != null)
            {
                button.GetComponent<Image>().color = brownYellow;
                button.GetComponent<Button>().interactable = true;
                stars.SetActive(true);
                stars.GetComponent<Stars>().SetStar(level.stars);
            }
        }
    }

}
