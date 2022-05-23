using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LineRenderer throwingArm;
    [SerializeField] private Transform idlePosition;
    [SerializeField] private Transform throwingArmPosition;
    [SerializeField] private Rigidbody2D hook;
    [SerializeField] private float releaseTime = 0.15f;
    [SerializeField] private float throwingRange = 2;
    [SerializeField] private GameObject shuriken;

    private Rigidbody2D shurikenRb;
    private SpringJoint2D shurikenJoint;
    private Shuriken shurikenScript;

    private bool isMouseDown;


    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.25f);
        //Ready Shuriken
        AssignShurikenComponent();
        LoadShuriken();

        //Ready Throwing Arm
        throwingArm.positionCount = 2;
        throwingArm.SetPosition(0, throwingArmPosition.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMouseDown && !shurikenScript.isThrown && !GameManager.instance.isGameOver)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, hook.position) > throwingRange)
            {
                //Set Arm and Shuriken position if out of range
                shurikenRb.position = hook.position + (mousePos - hook.position).normalized * throwingRange;
                SetArm(shurikenRb.position);
            }
            else
            {
                //Set Arm and Shuriken position if in range
                shurikenRb.position = mousePos;
                SetArm(mousePos);
            }  
        }
        else
        {
            ResetArm();
        }
    }

    private void OnMouseDown()
    {
        if (!GameManager.instance.isGameOver)
        {
            isMouseDown = true;
            shurikenRb.isKinematic = true;
        }
    }
    private void OnMouseUp()
    {
        if (!GameManager.instance.isGameOver)
        {
            isMouseDown = false;
            shurikenRb.isKinematic = false;
            StartCoroutine(Release());
        }
    }

    /* ------------- Shuriken --------------- */

    IEnumerator Release() //Shoot the shuriken when release mouse
    {
        yield return new WaitForSeconds(releaseTime);
        shurikenJoint.enabled = false;
        shurikenScript.isThrown = true;
        GameManager.instance.totalShuriken--;
    }
    public void LoadShuriken() //Reset shuriken and load to ninja hand
    {
        shuriken.SetActive(false);
        GameManager.instance.CheckGameStatus();
        GameManager.instance.UpdateShurikenText();
        if (GameManager.instance.totalShuriken > 0)
        {
            shurikenRb.velocity = Vector2.zero;
            shuriken.transform.position = hook.position;
            shurikenJoint.enabled = true;
            shurikenScript.isThrown = false;
            shurikenScript.deactiveTime = 0f;
            shurikenScript.bounceTime = 0;
            shuriken.SetActive(true);
        }
    }
    void AssignShurikenComponent() //Initiate shuriken component
    {
        shurikenRb = shuriken.GetComponent<Rigidbody2D>();
        shurikenJoint = shuriken.GetComponent<SpringJoint2D>();
        shurikenScript = shuriken.GetComponent<Shuriken>();
    }
    
    /* ------------- Throwing Arm --------------- */
    void ResetArm()
    {
        SetArm(idlePosition.position);
    }
    void SetArm(Vector3 position)
    {
        throwingArm.SetPosition(1, position);
    }
}
