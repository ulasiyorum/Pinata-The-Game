using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PlayFab;
using PlayFab.ClientModels;
using System.Globalization;

[Serializable]
public class ActualPlayerData
{
    //
    private string playfabID;
    //
    private float timer;
    private double networth;
    private float respawntimer;
    private float attackRange;
    private double enemyHealth;
    private double balance;
    private double coins;
    private int looting;
    private double playerDamage;
    private double attackDamage;
    private double attackSpeed;
    private int toolDurability;
    private int energy;
    private int maxEnergy;
    private int EPA;
    private int toolEPA;
    private bool openShop;
    private bool isDead;
    private bool attacking;
    private float locationx;
    private float locationy;
    private float locationz;
    private int exitTime;
    ////////////////////////
    private int loot;
    private int lootrange0;
    private int lootrange1;
    private double health;
    private int _this;
    private float respawnTime;
    private float tempRespawnTime;
    ////////////////////////
    private bool hasTool;
    private int toolID;
    private double tempAD;
    ///////////////////////
    private bool[] inventoryPinata;
    private bool[] inventoryPet;
    private bool[] inventoryScenes;
    private int[] levels;
    public bool[] equipped;
    private bool oncePet3;
    //////////////////////
    private double moneyToPay;
    private int loanCount;
    private int adCount;
    public static bool _null = false;
    //////////////////////
    private int bonusCounter;
    private float bonusTimer;
    //////////////////////
    private float freeTimer;
    private int playForFree;
    private int dailyObj;
    private float objTimer;


    public ActualPlayerData(AttackingPinata a)
    {
        this.objTimer = Objectives.getObjTimer();
        this.dailyObj = Objectives.getDailyObj();
        this.playfabID = a.getMain().GetComponent<PlayfabManager>().getPlayFabID();
        this.playForFree = MiniGameManager.getPlayForFree();
        this.freeTimer = MiniGameManager.getFreeTimer();
        this.bonusCounter = BonusCoin.getBonusCounter();
        this.bonusTimer = BonusCoin.getBonusTimer();    
        this.networth = a.getNetworth();
        this.inventoryScenes = a.getShop().getInventoryScenes();
        this.equipped = a.getShop().equipped;
        this.oncePet3 = a.getShop().oncePet3;
        this.coins = a.getCoins();
        this.toolID = a.getToolID();
        this.moneyToPay = a.getLoanScript().getMoneyToPay();
        this.loanCount = a.getLoanScript().getLoanCount();
        tempAD = a.getShop().getTemporaryAttackDamage();
        tempRespawnTime = Pinata.getTempRespawnTime();
        inventoryPet = new bool[a.getInventoryPet().Length];
        inventoryPinata = new bool[a.getInventoryPinata().Length];
        levels = new int[a.getShop().getLevels().Length];
        this.isDead = a.getIsDead();
        this.levels = a.getShop().getLevels();
        this.hasTool = a.getShop().hasTool;
        this.exitTime = a.getExitTime();
        this.playerDamage = a.getPlayerDamage();
        this.respawntimer = a.getRespawnTimer();
        this.attackRange = a.getAttackRange();
        this.inventoryPinata = a.getInventoryPinata();
        this.inventoryPet = a.getInventoryPet();
        this.health = Pinata.getHealth();
        this._this = Pinata._this;
        this.respawnTime = Pinata.getRespawnTime();
        if (isDead) 
        {
            this.enemyHealth = 0;
            this.loot = a.getNewPinata().GetComponent<Pinata>().getLoot2();
            this.lootrange0 = a.getNewPinata().GetComponent<Pinata>().getLootRange0();
            this.lootrange1 = a.getNewPinata().GetComponent<Pinata>().getLootRange1();
        }
        else
        {
            this.enemyHealth = a.getEnemyHealth();
            this.loot = a.getPinata().GetComponent<Pinata>().getLoot2();
            this.lootrange0 = a.getPinata().GetComponent<Pinata>().getLootRange0();
            this.lootrange1 = a.getPinata().GetComponent<Pinata>().getLootRange1();
        }
        this.balance = a.getBalance();
        this.looting = a.getLooting();
        this.attackDamage = a.getAttackDamage();
        this.attackSpeed = a.getAttackSpeed();
        this.toolDurability = a.getToolDurability();
        this.energy = a.getEnergy();
        this.maxEnergy = a.getMaxEnergy();
        this.EPA = a.getEPA();
        this.toolEPA = a.getToolEPA();
        this.attacking = a.getAttacking();
        this.locationx = a.transform.position.x;
        this.locationy = a.transform.position.y;
        this.locationz = a.transform.position.z;
    }

    public ActualPlayerData LoadGame()
    {
        return this;
    }


    //////////////////////////////////////////////////////
    public double getHealth()
    {
        return this.health;
    }
    public float getFreeTimer() { return this.freeTimer; }
    public int getPlayForFree() { return this.playForFree; }
    public int getAdCount() { return this.adCount; }
    public void equippedFalse()
    {
        for (int i=0; i<equipped.Length; i++) { equipped[i] = false; }
    }
    public bool getOncePet3() { return oncePet3; }
    public double getCoins() { return coins; }
    public int getToolID() { return toolID; }
    public double getMoneyToPay() { return moneyToPay; }
    public int getLoanCount() { return loanCount; }
    public double getTempAD() { return this.tempAD; }
    public float getRespawnTime()
    {
        return respawnTime;
    }
    public float getTempRespawnTime()
    {
        return tempRespawnTime;
    }
    public int getThis()
    {
        return this._this;
    }
    public int[] getLevels() { return this.levels; }
    public bool[] getInventoryPinata() { return this.inventoryPinata; }
    public int getLoot() { return this.loot; }
    public int getLootRange0() { return this.lootrange0; }
    public int getLootRange1() { return this.lootrange1; }
    public float getTimer()
    {
        return this.timer;
    }
    public float getRespawnTimer()
    {
        return this.respawntimer;
    }
    public float getAttackRange() { return this.attackRange; }
    public double getEnemyHealth() { return this.enemyHealth; }
    public double getBalance() { return this.balance; }
    public int getLooting() { return this.looting; }
    public double getPlayerDamage() { return this.playerDamage; }
    public double getAttackDamage() { return this.attackDamage; }
    public double getAttackSpeed() { return this.attackSpeed; }
    public int getToolDurability() { return this.toolDurability; }
    public int getEnergy() { return this.energy; }
    public int getMaxEnergy() { return this.maxEnergy; }
    public int getEPA() { return this.EPA; }
    public int getToolEPA() { return this.toolEPA; }
    public bool getOpenShop() { return this.openShop; }
    public bool[] getInventoryPet() { return this.inventoryPet; }
    public bool getAttacking() { return this.attacking; }
    public bool getIsDead() { return this.isDead; }
    public int getBonusCounter() { return bonusCounter; }
    public float getBonusTimer() { return bonusTimer; }
    public bool[] getInventoryScenes() { return inventoryScenes; }
    public float[] getLocation()
    {
        float[] location = new float[3];
        location[0] = locationx;
        location[1] = locationy;
        location[2] = locationz;
        return location;
    }
    public int getExitTime()
    {
        return this.exitTime;
    }
    public string getPlayFabID()
    {
        return this.playfabID;
    }
    public bool getHasTool() { return this.hasTool; }
    public string getInventoryScenesString()
    {
        string s = "";
        foreach (var b in inventoryScenes)
        {
            s += b + ",";
        }
        return s;
    }
    public string getInventoryPinataString()
    {
        string s = "";
        foreach(var b in inventoryPinata)
        {
            s += b + ",";
        }
        return s;
    }
    public string getInventoryPetString()
    {
        string s = "";
        foreach (var b in inventoryPet)
        {
            s += b + ",";
        }
        return s;
    }
    public string getLevelsString()
    {
        string s = "";
        foreach (var b in levels)
        {
            s += b + ",";
        }
        return s;
    }
    public double getNetworth() { return this.networth; }
    public string getEquippedString()
    {
        string s = "";
        foreach (var b in equipped)
        {
            s += b + ",";
        }
        return s;
    }
    public int getDailyObj() { return dailyObj; }
    public float getObjTimer() { return objTimer; }
    public string createData()
    {
        string data = "";
        data += "," + getTimer();
        data += "," + getRespawnTimer();
        data += "," + getAttackRange();
        data += "," + getEnemyHealth();
        data += "," + getBalance();
        data += "," + getCoins();
        data += "," + getLooting();
        data += "," + + getPlayerDamage();
        data += "," + getAttackDamage();
        data += "," + getAttackSpeed();
        data += "," + getToolDurability();
        data += "," + getEnergy();
        data += "," + getMaxEnergy();
        data += "," + getEPA();
        data += "," + getToolEPA();
        data += "," + getIsDead();
        data += "," + getLocation()[0];
        data += "," + getLocation()[1];
        data += "," + getLocation()[2];
        data += "," + getExitTime();
        data += "," + getLoot();
        data += "," + getLootRange0();
        data += "," + getLootRange1();
        data += "," + getHealth();
        data += "," + getThis();
        data += "," + getRespawnTime();
        data += "," + getTempRespawnTime();
        data += "," + getHasTool();
        data += "," + getToolID();
        data += "," + getTempAD();
        data += "," + getInventoryPinataString();
        data += "," + getInventoryPetString();
        data += "," + getLevelsString();
        data += "," + getEquippedString();
        data += "," + getOncePet3();
        data += "," + getMoneyToPay();
        data += "," + getLoanCount();

        return data;
    }

    public void readData(GetUserDataResult result)
    {
        if (result.Data.ContainsKey("_this")) { _this = int.Parse(result.Data["_this"].Value); }
        else { _this = 0; }
        if (result.Data.ContainsKey("attackDamage"))
        {
            attackDamage = double.Parse(result.Data["attackDamage"].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
        }
        else { attackDamage = 1; }
        if (result.Data.ContainsKey("attackSpeed"))
        {
            attackSpeed = double.Parse(result.Data["attackSpeed"].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
        }
        else { attackSpeed = 1; }
        if (result.Data.ContainsKey("attackRange"))
        {
            attackRange = float.Parse(result.Data["attackrange"].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
        }
        else { attackRange = 1.2f; }
        if (result.Data.ContainsKey("balance"))
        {
            balance = double.Parse(result.Data["balance"].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
        }
        else { balance = 0; }
        if (result.Data.ContainsKey("coins"))
        {
            coins = double.Parse(result.Data["coins"].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
        }
        else { coins = 0; }
        if (result.Data.ContainsKey("enemyhealth"))
        {
            enemyHealth = double.Parse(result.Data["enemyhealth"].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
        }
        else { enemyHealth = 10; }
        if (result.Data.ContainsKey("energy"))
        {
            energy = int.Parse(result.Data["energy"].Value);
        } else { energy = 100; }
        if (result.Data.ContainsKey("")) { EPA = int.Parse(result.Data["epa"].Value); }
        else { EPA = 5; }
        if (result.Data.ContainsKey("looting")) { looting = int.Parse(result.Data["looting"].Value); }
        else { looting = 0; }
        if (result.Data.ContainsKey("equipped"))
        {
            string[] equippedString = result.Data["equipped"].Value.Split(',');
            equipped = new bool[10];
            //for(int i=0; i<equipped.Length; i++) { equipped[i] = false; }
            for (int i = 0; i < equippedString.Length - 1; i++)
            {
                if (equippedString[i].ToLower() == "true")
                {
                    equipped[i] = true;
                }
                else { equipped[i] = false; }
            }
        }
        else { equipped = new bool[10]; for (int i = 0; i < equipped.Length - 1; i++) { equipped[i] = false; } }
        System.DateTime nowTime = System.DateTime.Now;
        if (result.Data.ContainsKey("exitTime"))
        {
            
            if (System.DateTime.TryParse(result.Data["exitTime"].Value, out nowTime))
            {
                exitTime = (nowTime.Year
                * 365 * 24 * 60 * 60 + nowTime.Month * 30 * 24 * 60 * 60 + nowTime.Day * 24 * 60 * 60 + nowTime.Hour
                * 60 * 60 + nowTime.Minute * 60 + nowTime.Second);
            }
            else
            {
                exitTime = int.Parse(result.Data["exitTime"].Value);
            }
        }
        else { exitTime = (nowTime.Year
                * 365 * 24 * 60 * 60 + nowTime.Month * 30 * 24 * 60 * 60 + nowTime.Day * 24 * 60 * 60 + nowTime.Hour
                * 60 * 60 + nowTime.Minute * 60 + nowTime.Second);
        }
        if (result.Data.ContainsKey("hasTool"))
        {
            if (result.Data["hasTool"].Value.ToLower() == "true")
            {
                hasTool = true;
            }
            else { hasTool = false; }
        } else { hasTool = false; }
        if (result.Data.ContainsKey("health"))
        {
            health = double.Parse(result.Data["health"].Value);
        } else { health = 10; }
        if (result.Data.ContainsKey("inventoryPet"))
        {
            string[] inventoryPetString = result.Data["inventoryPet"].Value.Split(',');
            inventoryPet = new bool[inventoryPetString.Length - 1];
            for (int i = 0; i < inventoryPetString.Length - 1; i++)
            {
                if (inventoryPetString[i].ToLower() == "true")
                {
                    inventoryPet[i] = true;
                }
                else { inventoryPet[i] = false; }
            }
        } else { inventoryPet = new bool[100]; for (int i = 0; i < inventoryPet.Length; i++) { inventoryPet[i] = false; } }
        if (result.Data.ContainsKey("inventoryPinata"))
        {
            string[] inventoryPinataString = result.Data["inventoryPinata"].Value.Split(',');
            inventoryPinata = new bool[inventoryPinataString.Length - 1];
            for (int i = 0; i < inventoryPinataString.Length - 1; i++)
            {
                if (inventoryPinataString[i].ToLower() == "true")
                {
                    inventoryPinata[i] = true;
                }
                else { inventoryPinata[i] = false; }
            }
        } else { inventoryPinata = new bool[100]; for (int i = 0; i < inventoryPet.Length; i++) { inventoryPet[i] = false; } }
        if (result.Data.ContainsKey("locationx"))
        {
            string[] inventoryScenesString;
            if (result.Data["locationx"].Value.ToLower().Contains("false"))
            {
                inventoryScenesString = result.Data["locationx"].Value.Split(',');
            }
            else
            {
                inventoryScenesString = new string[25];
                for (int i = 0; i < inventoryScenesString.Length; i++) { inventoryScenesString[i] = "false"; }
            }
            inventoryScenes = new bool[inventoryScenesString.Length - 1];
            inventoryScenesString[0] = "true";
            inventoryScenes = new bool[25];
            for (int i = 0; i < inventoryScenesString.Length - 1; i++)
            {
                if (inventoryScenesString[i].ToLower() == "true")
                {
                    inventoryScenes[i] = true;
                }
                else { inventoryScenes[i] = false; }
            }
            inventoryScenes[0] = true;
        } else { inventoryScenes = new bool[25];
            for(int i=0; i<inventoryScenes.Length; i++) { inventoryScenes[i] = false; }
            inventoryScenes[0] = true; }
        if (result.Data.ContainsKey("isDead"))
        {
            if (result.Data["isDead"].Value.ToLower() == "true")
            {
                isDead = true;
            }
            else { isDead = false; }
        } else { isDead = false; }
        if (result.Data.ContainsKey("levels"))
        {
            string[] levelsString = result.Data["levels"].Value.Split(',');
            levels = new int[levelsString.Length - 1];
            for (int i = 0; i < levelsString.Length - 1; i++) { levels[i] = int.Parse(levelsString[i]); }
        }
        else { levels = new int[inventoryPet.Length - 1]; }
        if (result.Data.ContainsKey("loanCount"))
        {
            loanCount = int.Parse(result.Data["loanCount"].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
        } else { loanCount = 0; }
        if (result.Data.ContainsKey("locationy"))
        {
            networth = double.Parse(result.Data["locationy"].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
        }
        if (result.Data.ContainsKey("locationz"))
        {
            adCount = int.Parse("" + result.Data["locationz"].Value.Replace('-', '0')[0], CultureInfo.InvariantCulture);
        } else { adCount = 0; }
        if (result.Data.ContainsKey("loot"))
        {
            loot = int.Parse(result.Data["loot"].Value);
        }
        else { loot = 5; }
        if (result.Data.ContainsKey("lootrange0"))
        {
            lootrange0 = int.Parse(result.Data["lootrange0"].Value);
        }else { lootrange0 = 0; }
        if (result.Data.ContainsKey("lootrange1"))
        {
            lootrange1 = int.Parse(result.Data["lootrange1"].Value);
        } else { lootrange1 = 3; }
        if (result.Data.ContainsKey("maxEnergy"))
        {
            maxEnergy = int.Parse(result.Data["maxEnergy"].Value);
        } else { maxEnergy = 100; }
        if (result.Data.ContainsKey("moneyToPay"))
        {
            moneyToPay = double.Parse(result.Data["moneyToPay"].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
        } else { moneyToPay = 0; }
        if (result.Data.ContainsKey("respawntimer"))
        {
            respawntimer = float.Parse(result.Data["respawntimer"].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
        } else { respawntimer = 0; }
        if (result.Data.ContainsKey("oncePet3"))
        {
            if (result.Data["oncePet3"].Value.ToLower() == "true")
            {
                oncePet3 = true;
            }
            else { oncePet3 = false; }
        } else { oncePet3 = false; }
        if (result.Data.ContainsKey("playerDamage"))
        {
            playerDamage = double.Parse(result.Data["playerDamage"].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
        } else { playerDamage = 1; }
        if (result.Data.ContainsKey("respawnTime"))
        {
            respawnTime = float.Parse(result.Data["respawnTime"].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
        } else { respawnTime = 15; }
        if (result.Data.ContainsKey("tempRespawnTime"))
        {
            tempRespawnTime = float.Parse(result.Data["tempRespawnTime"].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
        } else { tempRespawnTime = 15; }
        if (result.Data.ContainsKey("timer"))
        {
            timer = float.Parse(result.Data["timer"].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
        }else { timer = 0; }
        if (result.Data.ContainsKey("toolDurability"))
        {
            toolDurability = int.Parse(result.Data["toolDurability"].Value);
        } else { toolDurability = 0; }
        if (result.Data.ContainsKey("toolEPA"))
        {
            toolEPA = int.Parse(result.Data["toolEPA"].Value);
        } else { toolEPA = 0; }
        if (result.Data.ContainsKey("toolID"))
        {
            toolID = int.Parse(result.Data["toolID"].Value);
        } else { toolID = -1; }
        if (result.Data.ContainsKey("bonusTimer"))
        {
            bonusTimer = float.Parse(result.Data["bonusTimer"].Value.Replace(',', '.'), CultureInfo.InvariantCulture);
        } else { }
        if (result.Data.ContainsKey("bonusCounter"))
        {
            int.Parse(result.Data["bonusCounter"].Value);
        } else { bonusCounter = 0; }
        if (result.Data.ContainsKey("playForFree"))
        {
            playForFree = int.Parse(result.Data["playForFree"].Value);
        }
        else
        {
            playForFree= 0;
        }
        if (result.Data.ContainsKey("freeTimer"))
        {
            freeTimer = float.Parse(result.Data["freeTimer"].Value.Replace(',','.'),CultureInfo.InvariantCulture);
        }
        if (result.Data.ContainsKey("playfabID"))
        {
            playfabID = result.Data["playfabID"].Value;
        } else { playfabID = ""; }
        if (result.Data.ContainsKey("dailyObj")) { dailyObj = int.Parse(result.Data["dailyObj"].Value); }
        else { dailyObj = 0; }
        if (result.Data.ContainsKey("objTimer")) { objTimer = float.Parse(result.Data["objTimer"].Value.Replace(',','.'),CultureInfo.InvariantCulture); }
        else { objTimer = 0; }
        _null = false;
    }
}
