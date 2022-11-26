using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusCoin : MonoBehaviour
{
    private static float bonusTimer = 0;
    private static int bonusCounter = 0;
    [SerializeField] Player player;
    void Start()
    {
        
    }


    void Update()
    {
        bonusTimer += CustomTime.deltaTime;
        if (bonusTimer > 21600) 
        { if ((bonusCounter < 2) || (bonusCounter < 3 && player.getTreeBonus()[2])) { bonusCounter++; bonusTimer -= 21600; } else { bonusTimer = 0; }  }

        GetComponent<Text>().text = "Current: " + bonusCounter + " (Next In: " + (6 - (int)bonusTimer/3600) + " hrs)";
    }


    public void GetBonus()
    {
        if(bonusCounter > 0) { player.addToCoins(1); bonusCounter--; }
    }
    public static float getBonusTimer() { return bonusTimer; }
    public static int getBonusCounter() { return bonusCounter; }

    public static void setBonusTimer(float timer) { bonusTimer = timer; }
    public static void setBonusCounter(int counter) { bonusCounter = counter; }
}
