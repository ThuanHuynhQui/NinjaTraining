using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadBall());
    }

    IEnumerator LoadBall() //Assign balls to the Game Manager
    {
        bool isLoaded = false;
        int loadTime = 0;
        do
        {
            yield return new WaitForSeconds(0.2f);
            if (GameObject.Find("GameManager") != null)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().balls++;
                isLoaded = true;
            }
            else
                loadTime++;
        } while (!isLoaded && loadTime <= 10);
        if (!isLoaded && loadTime > 10)
        {
            Debug.Log("Fail to load balls");
        }
    }

}
