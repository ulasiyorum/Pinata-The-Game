using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives : MonoBehaviour
{
    private static int dailyObj;
    private static float objTimer;
    void Start()
    {
        
    }
    private void Awake()
    {
        objTimer = 0;
    }

    void Update()
    {
        
        if(objTimer >= 86400 && dailyObj == 0) { objTimer++; }
        else if(dailyObj == 0) { objTimer += CustomTime.deltaTime; }
        else { objTimer = 0; }
    }

    public static int getDailyObj()
    {
        return dailyObj;
    }
    public static float getObjTimer()
    {
        return objTimer;
    }
    public static void setObjTimer(float time) { objTimer = time; }
    public static void setDailyObj(int count) { dailyObj = count; }
}
