using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringScript : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject endPoint;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (startPoint != null && endPoint != null) //Set string start and end point
        {
            lineRenderer.SetPosition(0, startPoint.transform.position);
            lineRenderer.SetPosition(1, endPoint.transform.position);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
