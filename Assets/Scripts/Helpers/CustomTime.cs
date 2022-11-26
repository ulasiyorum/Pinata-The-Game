using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTime : MonoBehaviour
{
    public static float deltaTime;
    private static System.DateTime timeThen;
    private static System.DateTime timeNow;

    void Start()
    {
        deltaTime = 0;
        timeNow = DateTime.Now;
        timeThen = DateTime.Now;
    }

    void FixedUpdate()
    {
        Tick();
    }

    private static void Tick()
    {
        timeNow = System.DateTime.Now;
        TimeSpan interval = timeThen - timeNow;
        timeThen = System.DateTime.Now;
        deltaTime = (float)interval.TotalSeconds;
    
    }
}
