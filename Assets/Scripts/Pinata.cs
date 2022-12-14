using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;
using PlayFab.ClientModels;

[Serializable]
public class Pinata : MonoBehaviour
{


    // These are pinata's attributes in general
    private static double _health = 10;
    private static int _lootPerClick;
    private static int _loot = 5;
    private static int _lootFromPerk = 0;
    private static float _tempRespawnTime;
    private static int _lootRange0 = 0;
    private static int _lootRange1 = 3;
    private static float _respawnTime = 15;
    private static Sprite[] images;
    private static Sprite empty;
    public static Sprite[] Images
    {
        get => images;
        set => images = value;
    } // this is not initialized

    public static GameObject Body
    {
        get
        {
            return Instance.i.PinataObject.gameObject;
        }
    }

    public static Sprite BodySprite
    {
        get
        {
            return Body.GetComponent<SpriteRenderer>().sprite;
        }
        set
        {
            Body.GetComponent<SpriteRenderer>().sprite = value;
        }
    }


    private static Player player;
    public static int _this = 0;
    public static int Current { get => _this; set => _this = value; }

    // These are spawned pinata's attributes
    private double health;
    private float respawnTime;
    public float RespawnTime { get => respawnTime; set => respawnTime = value; }
    public double Health { get => health; set => health = value; }


    void Start()
    {
        respawnTime = _respawnTime;
        player = FindObjectOfType<Player>();
        empty = GameAssets.Instance.empty;
        images = new Sprite[GameAssets.Instance.PinataImages.Length];
        for (int i = 0; i < GameAssets.Instance.PinataImages.Length; i++)
        {
            images[i] = GameAssets.Instance.PinataImages[i];
        }
        Respawn();
    }
    private void OnEnable()
    {

    }
    void FixedUpdate()
    {
        if(_this == 0) { _tempRespawnTime = 15; }

        if (CreateRandomChance.Gamble(Instance.i.Player.getCloverChance(), 6))
            _lootPerClick = _lootRange1;
        else
            _lootPerClick = UnityEngine.Random.Range(_lootRange0, _lootRange1);
        
    }

    public static double getHealth()
    {
        return _health;
    }
    public static void setLootFromPerk(int perk)
    {
        _lootFromPerk = perk;
    }
    public static double getLootFromPerk() { return _lootFromPerk; }
    public static void setHealth(double health)
    {
        _health = health;
    }
    public double getHealth2()
    {
        return _health;
    }
    public static void setTempRespawnTime(float time) { _tempRespawnTime = time; }
    public static int getLoot()
    {
        return _loot;
    }
    public int getLoot2()
    {
        return _loot;
    }
    public static int getLootPerClick()
    {
        return _lootPerClick;
    }
    public int getLootPerClick2()
    {
        return _lootPerClick;
    }
    public static float getRespawnTime()
    {

        return _respawnTime;
    }
    public static float getTempRespawnTime() { return _tempRespawnTime; }
    public int getLootRange0() { return _lootRange0; }
    public int getLootRange1() { return _lootRange1; } 
    public static void setRespawnTime(float respawn) { _respawnTime = respawn; }
    public static void buyDefault()
    {
        _this = 0;
        BodySprite = images[_this];
        _health = 10;
        _loot = 5;
        _lootRange0 = 0;
        _lootRange1 = 3;
        _respawnTime = 15;
        _tempRespawnTime = 15;
    }
    public static void buyPet3Perk()
    {
        bool var = false;
        player.getShop().callPinataThis();
        if (player.getEnemyHealth() == _health) { var = true; }
        _health = _health + (_health * (player.getShop().getPerks()[0]) / 100);
        if(var) { player.setEnemyHealth(_health); }
        _loot = _loot - (int)(_loot * 0.5);
        _lootRange0 = _lootRange0 + (_lootRange0 * player.getShop().getPerks()[0] / 100);
        _lootRange1 = _lootRange1 + (_lootRange1 * player.getShop().getPerks()[0] / 100);
        _respawnTime = _respawnTime + (_respawnTime * player.getShop().getPerks()[0]/ 100);
        _tempRespawnTime = _tempRespawnTime + (_tempRespawnTime * player.getShop().getPerks()[0] / 100);
    }
    public static void buyID0()
    {
        player.getInventoryPinata()[0] = true;
        _this = 1;
        BodySprite = images[_this];
        _health = 5;
        _loot = 5;
        _respawnTime = 4;
        _tempRespawnTime = 4;
        _lootRange0 = 0;
        _lootRange1 = 2;
    }
    public static void buyID1()
    {
        player.getInventoryPinata()[1] = true;
        _this = 2;
        BodySprite = images[_this];
        _health = 24;
        _loot = 24;
        _respawnTime = 39;
        _tempRespawnTime = 39;
        _lootRange0 = 1;
        _lootRange1 = 4;
    }
    public static void buyID2()
    {
        player.getInventoryPinata()[2] = true;
        _this = 3;
        BodySprite = images[_this];
        _health = 164;
        _loot = 61;
        _respawnTime = 100;
        _tempRespawnTime = 100;
        _lootRange0 = 1;
        _lootRange1 = 2;
    }
    public static void buyID3() 
    {
        player.getInventoryPinata()[3] = true;
        _this = 4;
        BodySprite = images[_this];
        _health = 200;
        _loot = 25;
        _respawnTime = 92;
        _tempRespawnTime = 92;
        _lootRange0 = 1;
        _lootRange1 = 5;
    }
    public static void buyID4()
    {
        player.getInventoryPinata()[4] = true;
        _this = 5;
        BodySprite = images[_this];
        _health = 120;
        _loot = 0;
        _respawnTime = 102;
        _tempRespawnTime = 102;
        _lootRange0 = 2;
        _lootRange1 = 6;
    }
    public static void buyID5()
    {
        player.getInventoryPinata()[5] = true;
        _this = 6;
        BodySprite = images[_this];
        _health = 60;
        _loot = 20;
        _respawnTime = 2.5f;
        _tempRespawnTime = 2.5f;
        _lootRange0 = 0;
        _lootRange1 = 1;
    }

    public static int getThis()
    {
        return _this;
    }
    public int getThis0() { return _this; }
    public void setLootPerClick(int lootperclick)
    {
        _lootPerClick = lootperclick;
    }
    public static void setLoot(int value)
    {
        _loot = value;
    }
    public static void setLootRange0(int value)
    {
        _lootRange0 = value;
    }
    public static void setLootRange1(int value)
    {
        _lootRange1 = value;
    }
    void PinataImage()
    {
        BodySprite = images[_this];
    }
    public static int GetLootRange(int range)
    {
        if (range > 0)
        {
            return _lootRange1;
        }
        else
        {
            return _lootRange0;
        }
    }

    public void TakeDamage()
    {
        double damage = Instance.i.Player.AttackDamage;
        if (damage >= health)
        {
            BodySprite = empty;
            Body.SetActive(false);
            health = 0;
            GameAssets.Instance.PinataDied.Play("CandyDrop");
            Instance.i.PinataObject.Die();
            AudioManager.PlaySound("pop");
            return;
        }
        health -= damage;
    }
    private void Die()
    {
        Body.SetActive(false);
    }

    public void Respawn()
    {
        Body.SetActive(true);
        BodySprite = images[Current];
        health = _health;
    }


}
