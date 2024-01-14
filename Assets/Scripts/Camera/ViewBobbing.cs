using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ViewBobbing : MonoBehaviour
{
    

    public float minY = 0.33f;
    public float maxY = 0.35f;

    public float speed = 0.03f;

    private float accumulatedTime;

    private void Update()
    {
        accumulatedTime += Time.deltaTime;

       float newY = Mathf.PingPong(accumulatedTime * speed, maxY - minY) + minY;
       transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
    }



}
