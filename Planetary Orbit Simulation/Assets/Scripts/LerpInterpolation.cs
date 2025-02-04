using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpInterpolation : MonoBehaviour
{
    private Vector3 pointA;
    private Vector3 pointB = new Vector3(5,-5,0);
    private float minValue = 0;
    private float maxValue = 5;
    private float desiredDuration = 3f;
     private float elapsedTime;

    [SerializeField]
    private AnimationCurve curve;
   
    private void Start() 
    {
        pointA = transform.position;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.L)) 
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime /desiredDuration;
            
            transform.position = Vector3.Lerp(pointA, pointB, curve.Evaluate(t));
        }

        if (Input.GetKey(KeyCode.C))
        {
            elapsedTime += Time.deltaTime * 2f;
            elapsedTime = Math.Clamp(elapsedTime, minValue, maxValue);

            Debug.Log($"Clamped Value {elapsedTime}");
        }

        if (Input.GetKey(KeyCode.R)) 
        {
            elapsedTime = 0;
            transform.position = pointA;
        }
    }

}
