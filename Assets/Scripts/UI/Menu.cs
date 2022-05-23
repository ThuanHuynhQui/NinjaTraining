using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f); //wait for progress initiate
        BackgroundMusic.instance.StopGameMusic();
        BackgroundMusic.instance.PlayMenuMusic();
        Progress.instance.LoadProgress();
    }

    public void LevelButtonClicked()
    {
        SceneManager.LoadScene("Level Select", LoadSceneMode.Single);
    }

    public void PlayButtonClicked()
    {
        BackgroundMusic.instance.StopMenuMusic();
        SceneManager.LoadScene(Progress.instance.levelIsPlaying, LoadSceneMode.Single);
    }

    public void CreditButtonClicked()
    {
        SceneManager.LoadScene("Credit", LoadSceneMode.Single);
    }
}
