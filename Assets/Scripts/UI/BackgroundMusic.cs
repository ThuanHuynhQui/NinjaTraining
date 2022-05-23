using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;
    public AudioSource gameBackgroundMusic;
    public AudioSource menuBackgroundMusic;
    // Start is called before the first frame update
    void Awake()
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

    public void PlayGameMusic()
    {
        if (gameBackgroundMusic.isPlaying)
        {
            return;
        }
        gameBackgroundMusic.Play();
    }

    public void PlayMenuMusic()
    {
        if (menuBackgroundMusic.isPlaying)
        {
            return;
        }
        menuBackgroundMusic.Play();
    }

    public void StopGameMusic()
    {
        gameBackgroundMusic.Stop();
    }

    public void StopMenuMusic()
    {
        menuBackgroundMusic.Stop();
    }
}
