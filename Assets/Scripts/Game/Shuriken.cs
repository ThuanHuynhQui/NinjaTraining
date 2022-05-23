using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [SerializeField] private float maxDeactiveTime;
    public float deactiveTime;

    [SerializeField] private int maxBounceTime;
    public int bounceTime;

    public bool isThrown;
    private Player playerScript;

    private AudioSource balloonExplodeSFX;

    private void Start()
    {
        balloonExplodeSFX = GetComponent<AudioSource>();
        deactiveTime = 0f;
        bounceTime = 0;
        isThrown = false;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {
        //Run timers to deactive the shuriken. If shuriken is not hit any ball in 7 seconds or bounce 20 times without hit any ball
        if (GameManager.instance != null) 
        {
            if (isThrown && !GameManager.instance.isGameOver)
            {
                deactiveTime += Time.deltaTime;
                if (deactiveTime >= maxDeactiveTime)
                {
                    isThrown = false;
                    playerScript.LoadShuriken();
                }
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        //Reset timer if shuriken hit a ball or count how many times shuriken has bounce without hit any ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(collision.gameObject);
            deactiveTime = 0f; //Reset deactive timer
            bounceTime = 0; //Reset bounce time counter
            balloonExplodeSFX.Play(); //Play balloon explode sfx
            GameManager.instance.balls--;
        }
        else if(!GameManager.instance.isGameOver)
        {
            bounceTime++;
            if (bounceTime >= maxBounceTime)
            {
                isThrown = false;
                playerScript.LoadShuriken();
            }
        }
    }
}
