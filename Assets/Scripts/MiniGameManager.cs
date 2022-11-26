using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] GameObject main;
    [SerializeField] Player player;
    [SerializeField] Text generalText;
    [SerializeField] Text youHaveThis;
    [SerializeField] Text networthText;
    private float textTimer;
    private static int playForFree;
    private static float freeTimer;
    void Start()
    {
        generalText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(generalText.text == "") { textTimer = 0; }
        else if(generalText.text != "" && textTimer < 4) { textTimer += Time.deltaTime; }
        else if(textTimer > 4)
        {
            generalText.text = "";
        }
        if (playForFree <= 2 && freeTimer <= 86400)
        {
            freeTimer += CustomTime.deltaTime;
        }
        if(playForFree <= 1 && freeTimer >= 86400)
        {
            playForFree++;
            freeTimer -= 86400;
        }
        else if(playForFree == 2) { freeTimer = 0; }
        youHaveThis.text = "You have " + playForFree + " free pass.";
        networthText.text = "Your networth is: " + (int)player.getNetworth();
    }
    public static float getFreeTimer() { return freeTimer; }
    public static int getPlayForFree() { return playForFree; }
    public static void setFreeTimer(float timer) { freeTimer = timer; }
    public static void setPlayForFree(int count) { playForFree = count; }

    //////////////////////////////////////////////////////////////////////
    public static bool play()
    {
        if (playForFree > 0)
        {
            playForFree--;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void PlayGame0()
    {
        if(player.getNetworth() < 5000) { generalText.text = "Your networth should be at least 5000 to play minigames."; return; }
        if(playForFree > 0 || player.getCoins() >= 3)
        {
            main.GetComponent<PlayfabManager>().SaveScene(0);
        }
        else
        {
            generalText.text = "You don't have enough balance to play this game.";
        }
    }
}
