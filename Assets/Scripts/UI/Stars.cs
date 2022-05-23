using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public GameObject[] stars = new GameObject[3];

    public void SetStar(int totalStar)
    {
        for (int i = 0; i < totalStar; i++)
        {
            stars[i].GetComponent<Star>().AcquireStar();
        }
    }
}
