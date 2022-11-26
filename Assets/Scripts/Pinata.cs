using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Pinata : MonoBehaviour
{
    private static double _health = 10;
    private static int lootPerClick;
    private static int loot = 5;
    private static int lootFromPerk = 0;
    private static float tempRespawnTime;
    private static int lootRange0 = 0;
    private static int lootRange1 = 3;

    public static int GetLootRange(int range)
    {
        if(range > 0)
        {
            return lootRange1;
        }
        else
        {
            return lootRange0;
        }
    }

    private static float respawnTime = 15;
    [SerializeField] GameObject[] pImages;
    private static GameObject[] images;
    private static Player player;
    public static int _this = 0;
    void Start()
    {
        Debug.Log(_this);
    }
    private void OnEnable()
    {
        player = FindObjectOfType<Player>();
        images = new GameObject[pImages.Length];
        for (int i = 0; i < images.Length; i++) { images[i] = pImages[i]; }
    }
    void FixedUpdate()
    {
        if(_this == 0) { tempRespawnTime = 15; }
        if (player.getShop().equipped[1]) {
            lootPerClick = 0; }
        else if (player.getShop().equipped[3] && player.getShop().koalaCondition)
        {
            lootPerClick = (int)UnityEngine.Random.Range(lootRange0+1, lootRange1+1);
        }
        else 
         { lootPerClick = (int)UnityEngine.Random.Range(lootRange0, lootRange1); }
        PinataImage();
    }

    public static double getHealth()
    {
        return _health;
    }
    public static void setLootFromPerk(int perk)
    {
        lootFromPerk = perk;
    }
    public static double getLootFromPerk() { return lootFromPerk; }
    public static void setHealth(double health)
    {
        _health = health;
    }
    public double getHealth2()
    {
        return _health;
    }
    public static void setTempRespawnTime(float time) { tempRespawnTime = time; }
    public static int getLoot()
    {
        return loot;
    }
    public int getLoot2()
    {
        return loot;
    }
    public static int getLootPerClick()
    {
        return lootPerClick;
    }
    public int getLootPerClick2()
    {
        return lootPerClick;
    }
    public GameObject[] getImages()
    {
        return images;
    }
    public static float getRespawnTime()
    {

        return respawnTime;
    }
    public static float getTempRespawnTime() { return tempRespawnTime; }
    public int getLootRange0() { return lootRange0; }
    public int getLootRange1() { return lootRange1; } 
    public static void setRespawnTime(float respawn) { respawnTime = respawn; }
    public static void buyDefault()
    {
        images[_this].SetActive(false);
        _this = 0;
        images[_this].SetActive(true);
        _health = 10;
        loot = 5;
        lootRange0 = 0;
        lootRange1 = 3;
        respawnTime = 15;
        tempRespawnTime = 15;
    }
    public static void buyPet3Perk()
    {
        bool var = false;
        player.getShop().callPinataThis();
        if (player.getEnemyHealth() == _health) { var = true; }
        _health = _health + (_health * (player.getShop().getPerks()[0]) / 100);
        if(var) { player.setEnemyHealth(_health); }
        loot = loot - (int)(loot * 0.5);
        lootRange0 = lootRange0 + (lootRange0 * player.getShop().getPerks()[0] / 100);
        lootRange1 = lootRange1 + (lootRange1 * player.getShop().getPerks()[0] / 100);
        respawnTime = respawnTime + (respawnTime * player.getShop().getPerks()[0]/ 100);
        tempRespawnTime = tempRespawnTime + (tempRespawnTime * player.getShop().getPerks()[0] / 100);
    }
    public static void buyID0()
    {
        player.getInventoryPinata()[0] = true;
        images[_this].SetActive(false);
        _this = 1;
        images[_this].SetActive(true);
        _health = 5;
        loot = 5;
        respawnTime = 4;
        tempRespawnTime = 4;
        lootRange0 = 0;
        lootRange1 = 2;
    }
    public static void buyID1()
    {
        player.getInventoryPinata()[1] = true;
        images[_this].SetActive(false);
        _this = 2;
        images[_this].SetActive(true);
        _health = 24;
        loot = 24;
        respawnTime = 39;
        tempRespawnTime = 39;
        lootRange0 = 1;
        lootRange1 = 4;
    }
    public static void buyID2()
    {
        player.getInventoryPinata()[2] = true;
        images[_this].SetActive(false);
        _this = 3;
        images[_this].SetActive(true);
        _health = 164;
        loot = 61;
        respawnTime = 100;
        tempRespawnTime = 100;
        lootRange0 = 1;
        lootRange1 = 2;
    }
    public static void buyID3() 
    {
        player.getInventoryPinata()[3] = true;
        images[_this].SetActive(false);
        _this = 4;
        images[_this].SetActive(true);
        _health = 200;
        loot = 25;
        respawnTime = 92;
        tempRespawnTime = 92;
        lootRange0 = 1;
        lootRange1 = 5;
    }
    public static void buyID4()
    {
        player.getInventoryPinata()[4] = true;
        images[_this].SetActive(false);
        _this = 5;
        images[_this].SetActive(true);
        _health = 120;
        loot = 0;
        respawnTime = 102;
        tempRespawnTime = 102;
        lootRange0 = 2;
        lootRange1 = 6;
    }
    public static void buyID5()
    {
        player.getInventoryPinata()[5] = true;
        images[_this].SetActive(false);
        _this = 6;
        images[_this].SetActive(true);
        _health = 60;
        loot = 20;
        respawnTime = 2.5f;
        tempRespawnTime = 2.5f;
        lootRange0 = 0;
        lootRange1 = 1;
    }
    public void setImages(GameObject[] a)
    {
        images = a;
    }

    public static int getThis()
    {
        return _this;
    }
    public int getThis0() { return _this; }
    public void setLootPerClick(int lootperclick)
    {
        lootPerClick = lootperclick;
    }
    public static void setLoot(int _loot)
    {
        loot = _loot;
    }
    public static void setLootRange0(int _lootRange0)
    {
        lootRange0 = _lootRange0;
    }
    public static void setLootRange1(int _lootRange1)
    {
        lootRange1 = _lootRange1;
    }
    void PinataImage()
    {
        if (player.getPinata() != null && _this != 0)
        {
            foreach(var p in images) { p.SetActive(false); }
            getImages()[_this].SetActive(true);
        }
        else if (player.getPinata() != null && _this == 0) { foreach (var p in images) { p.SetActive(false); } getImages()[_this].SetActive(true); }
        else if (player.getPinata() == null && _this != 0)
        {
            foreach (var p in images) { p.SetActive(false); }
        }
        else if (player.getPinata() == null && _this == 0)
        {
            foreach (var p in images) { p.SetActive(false); }
        }
    }
}
