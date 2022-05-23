using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    public Image noFillStar;
    public Image filledStar;

    private void Start()
    {
        noFillStar.gameObject.SetActive(true);
    }

    public void AcquireStar()
    {
        noFillStar.gameObject.SetActive(false);
        filledStar.gameObject.SetActive(true);
    }
}
