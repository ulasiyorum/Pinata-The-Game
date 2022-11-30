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
    ///////////////////// SkillTree - Player Script
    private int energyAttribute;
    private float energyTimer;
    private int makeitworth;
    private bool wrapgod;
    private int miniGameMultiplier; private bool[] skills; private float skillTimer;
    private int lootingCap; private double lootEfficiency; private int extraLooting;
    private float extraAttackSpeed;
    private bool rheagod;
    private double miniGameMultiplier2;
    private int cloverChance;
    private int trickOrTreat;
    private int trickOrTreatChance;
    private int trickOrTreatRandom;
    private int totCount;
    private int coinChance;
    private bool godOfBugs;
    private int treasureCount;
    private double miniGameMultiplier3;
    private int bossLevel;
    private bool[] treeBonus;
    private int popped;


    private int treeType;
    private int[] levels1;
    private int[] levels2;
    private int[] levels3;
    public int getPopped() { return this.popped; }
    public bool[] getTreeBonus() { return this.treeBonus; }
    public int getLootingCap() { return this.lootingCap; }
    public int getTreeType() { return treeType; }
    public int[] getLevels1() { return levels1; }
    public int[] getLevels2() { return levels2; }
    public int[] getLevels3() { return levels3; }
    public ActualPlayerData(Player a)
    {
        this.popped = a.getPopped();
        this.treeType = a.getMain().GetComponent<SkillTree>().getTreeType();
        this.levels1 = a.getMain().GetComponent<SkillTree>().getLevels1();
        this.levels2 = a.getMain().GetComponent<SkillTree>().getLevels2();
        this.levels3 = a.getMain().GetComponent<SkillTree>().getLevels3();

        this.energyAttribute = a.getEnergyAttribute();
        this.energyTimer = a.getEnergyTimer();
        this.makeitworth = a.getMakeItWorth();
        this.wrapgod = a.getWrapGod();
        this.miniGameMultiplier = a.getMiniGameMultiplier();
        this.skills = a.getSkills();
        this.skillTimer = a.getSkillTimer();
        this.lootingCap = a.getLootingCap();
        this.lootEfficiency = a.getLootEfficiency(); 
        this.extraLooting = a.getExtraLooting();
        this.extraAttackSpeed = a.getExtraAs();
        this.rheagod = a.getRheaGod();
        this.miniGameMultiplier2 = a.getMiniGameMultiplier2();
        this.cloverChance = a.getCloverChance();
        this.trickOrTreat = a.getTrickOrTreat();
        this.trickOrTreatChance = a.getTrickOrTreatChance();
        this.trickOrTreatRandom = a.getTrickOrTreatRandom();
        this.totCount = a.getTotCount();
        this.coinChance = a.getCoinChance();
        this.godOfBugs = a.getGodOfBugs();
        this.treasureCount = a.getTreasureCount();
        this.miniGameMultiplier3 = a.getMiniGameMultiplier3();
        this.bossLevel = a.getBossLevel();
        this.treeBonus = a.getTreeBonus();

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
        this.exitTime = a.getExitTime();
        this.respawntimer = a.getRespawnTimer();
        this.attackRange = a.PlayerTool.AttackRange;
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
        this.looting = a.PlayerTool.Looting;
        this.attackDamage = a.AttackDamage;
        this.attackSpeed = a.AttackSpeed;
        this.toolDurability = a.PlayerTool.Durability;
        this.energy = a.getEnergy();
        this.maxEnergy = a.getMaxEnergy();
        this.EPA = a.getEPA();
        this.toolEPA = a.PlayerTool.EPA;
        this.locationx = a.transform.position.x;
        this.locationy = a.transform.position.y;
        this.locationz = a.transform.position.z;
    }
    public int getEnergyAttribute() { return this.energyAttribute; }
    public float getEnergyTimer() { return this.energyTimer; }
    public int getMakeItWorth() { return this.makeitworth; }
    public bool getWrapGod() { return this.wrapgod; }
    public int getMiniGameMultiplier() { return this.miniGameMultiplier; }
    public double getEfficiency()
    {
        return lootEfficiency;
    }
    public int getExtraLooting()
    {
        return extraLooting;
    }
    public float getExtraAs()
    {
        return extraAttackSpeed;
    }
    public bool getRheaGod()
    {
        return rheagod;
    }
    public double getMiniGameMultiplier2() { return this.miniGameMultiplier2; }
    public double getMiniGameMultiplier3() { return this.miniGameMultiplier3; }
    public bool[] getSkills() { return this.skills; }
    public float getSkillTimer() { return skillTimer; }
    public double getLootEfficiency() { return lootEfficiency; }
    public int getCloverChance() { return this.cloverChance; }
    public int getTrickOrTreat() { return this.trickOrTreat; }
    public int getTrickOrTreatChance() { return this.trickOrTreatChance; }
    public int getTrickOrTreatRandom() { return this.trickOrTreatRandom; }
    public int getTotCount() { return this.totCount; }
    public int getCoinChance() { return this.coinChance; }
    public bool getGodOfBugs() { return godOfBugs; }
    public int getTreasureCount() { return treasureCount; }
    public int getBossLevel() { return this.bossLevel; }
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
    public string getLevels1String()
    {
        string s = "";
        foreach (var b in levels1)
        {
            s += b + ",";
        }
        return s;
    }
    public string getLevels2String()
    {
        string s = "";
        foreach (var b in levels2)
        {
            s += b + ",";
        }
        return s;
    }
    public string getSkillsString()
    {
        string s = "";
        foreach (var b in skills)
        {
            s += b + ",";
        }
        return s;
    }
    public string getLevels3String()
    {
        string s = "";
        foreach (var b in levels3)
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
        if (result.Data.ContainsKey("epa")) { EPA = int.Parse(result.Data["epa"].Value); }
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
        if (result.Data.ContainsKey("objTimer")) { objTimer = float.Parse(result.Data["objTimer"].Value.Replace(',', '.'), CultureInfo.InvariantCulture); }
        else { objTimer = 0; }
        if (result.Data.ContainsKey("miniGameMultiplier")) { miniGameMultiplier = int.Parse(result.Data["miniGameMultiplier"].Value); }
        else {
            miniGameMultiplier = 0;   
        }
        if (result.Data.ContainsKey("miniGameMultiplier2")) { miniGameMultiplier2 = double.Parse(result.Data["miniGameMultiplier2"].Value.Replace(',','.'),CultureInfo.InvariantCulture); }
        else
        {
            miniGameMultiplier2 = 0;
        }
        if (result.Data.ContainsKey("miniGameMultiplier3")) { miniGameMultiplier3 = double.Parse(result.Data["miniGameMultiplier3"].Value.Replace(',','.'),CultureInfo.InvariantCulture); }
        else
        {
            miniGameMultiplier3 = 0;
        }
        if (result.Data.ContainsKey("energyAttribute")) { energyAttribute = int.Parse(result.Data["energyAttribute"].Value); }
        else { energyAttribute = 1; }
        if (result.Data.ContainsKey("energyTimer")) { energyTimer = float.Parse(result.Data["energyTimer"].Value.Replace(',', '.'), CultureInfo.InvariantCulture); }
        else { energyTimer = 10; }
        if(result.Data.ContainsKey("makeitworth")) { makeitworth = int.Parse(result.Data["makeitworth"].Value); }
        else { makeitworth = 0; }
        if (result.Data.ContainsKey("wrapgod"))
        {
            if (result.Data["wrapgod"].Value.ToLower() == "true")
            {
                wrapgod = true;
            }
            else { wrapgod = false; }
        }
        else { wrapgod = false; }
        if (result.Data.ContainsKey("skills"))
        {
            
            string[] skillsString = result.Data["skills"].Value.Split(',');
            skills = new bool[skillsString.Length];
            for (int i=0; i<skillsString.Length; i++)
            {
                if (skillsString[i].ToLower() == "true") { skills[i] = true; }
                else { skills[i] = false; }
            }
        }
        else
        {
            skills = new bool[5];
            for(int i=0; i<skills.Length; i++) { skills[i] = false; }
        }
        if (result.Data.ContainsKey("skillTimer")) { skillTimer = float.Parse(result.Data["skillTimer"].Value.Replace(',','.'),CultureInfo.InvariantCulture); }
        else { skillTimer = 0; }
        if (result.Data.ContainsKey("lootingCap")) { lootingCap = int.Parse(result.Data["lootingCap"].Value); }
        else { lootingCap = 10; }
        if (result.Data.ContainsKey("lootEfficiency")) { lootEfficiency = double.Parse(result.Data["lootEfficiency"].Value.Replace(',', '.'), CultureInfo.InvariantCulture); }
        else { lootEfficiency = 1; }
        if (result.Data.ContainsKey("extraLooting")) { extraLooting = int.Parse(result.Data["extraLooting"].Value); }
        else { extraLooting = 0; }
        if (result.Data.ContainsKey("extraAttackSpeed")) { extraAttackSpeed = float.Parse(result.Data["extraAttackSpeed"].Value.Replace(',', '.'), CultureInfo.InvariantCulture); }
        else { extraAttackSpeed = 0; }
        if (result.Data.ContainsKey("bossLevel"))
        {
            bossLevel = int.Parse(result.Data["bossLevel"].Value);
        }
        else {
            bossLevel = 0;
        }
        if (result.Data.ContainsKey("treeBonus"))
        {
            treeBonus = new bool[3];
            string[] treeBonusString = result.Data["treeBonus"].Value.Split(',');
            for(int i=0; i<treeBonusString.Length; i++)
            {
                if (treeBonusString[i].ToLower() == "true")
                {
                    treeBonus[i] = true;
                }
                else
                {
                    treeBonus[i] = false;
                }
            }
        }
        else
        {
            treeBonus = new bool[3];
            for (int i = 0; i < treeBonus.Length; i++) { treeBonus[i] = false; }
        }
        if (result.Data.ContainsKey("rheagod"))
        {
            if (result.Data["rheagod"].Value.ToLower() == "true")
            {
                rheagod = true;
            }
            else { rheagod = false; }
        }
        else { rheagod = false; }
        if (result.Data.ContainsKey("godOfBugs"))
        {
            if (result.Data["godOfBugs"].Value.ToLower() == "true")
            {
                godOfBugs = true;
            }
            else { godOfBugs = false; }
        }
        else { godOfBugs = false; }
        if (result.Data.ContainsKey("cloverChance"))
        {
            cloverChance = int.Parse(result.Data["cloverChance"].Value);
        }
        else { cloverChance = 0; }
        if (result.Data.ContainsKey("trickOrTreat"))
        {
            trickOrTreat = int.Parse(result.Data["trickOrTreat"].Value);
        } else { trickOrTreat = 40; }
        if (result.Data.ContainsKey("trickOrTreatChance"))
        {
            trickOrTreatChance = int.Parse(result.Data["trickOrTreatChance"].Value);
        }
        else { trickOrTreatChance = 0; }
        if (result.Data.ContainsKey("trickOrTreatRandom"))
        {
            trickOrTreatRandom = int.Parse(result.Data["trickOrTreatRandom"].Value);
        }
        else { trickOrTreatRandom = 0; }
        if (result.Data.ContainsKey("totCount"))
        {
            totCount = int.Parse(result.Data["totCount"].Value);
        }
        else { totCount = 4; }
        if (result.Data.ContainsKey("treasureCount"))
        {
            treasureCount = int.Parse(result.Data["treasureCount"].Value);
        }
        else { treasureCount = 0; }
        if (result.Data.ContainsKey("treeType"))
        {
            treeType = int.Parse(result.Data["treeType"].Value);
        }
        else { treeType = 0; }
        if (result.Data.ContainsKey("levels1"))
        {
            levels1 = new int[6];
            string[] levels1String = result.Data["levels1"].Value.Split(',');
            for(int i=0; i<levels1String.Length-1; i++)
            {
                levels1[i] = int.Parse(levels1String[i]);
            }
        } else { levels1 = new int[6];
            for (int i = 0; i < levels1.Length; i++) { levels1[i] = 0; }
        }
        if (result.Data.ContainsKey("levels2"))
        {
            levels2 = new int[6];
            string[] levels2String = result.Data["levels2"].Value.Split(',');
            for (int i = 0; i < levels2String.Length-1; i++)
            {
                levels2[i] = int.Parse(levels2String[i]);
            }
        }
        else
        {
            levels2 = new int[6];
            for (int i = 0; i < levels2.Length; i++) { levels2[i] = 0; }
        }
        if (result.Data.ContainsKey("levels3"))
        {
            levels3 = new int[6];
            string[] levels3String = result.Data["levels3"].Value.Split(',');
            for (int i = 0; i < levels3String.Length-1; i++)
            {
                levels3[i] = int.Parse(levels3String[i]);
            }
        }
        else
        {
            levels3 = new int[6];
            for (int i = 0; i < levels3.Length; i++) { levels3[i] = 0; }
        }
        if (result.Data.ContainsKey("popped"))
        {
            popped = int.Parse(result.Data["popped"].Value);
        }
        else
        {
            popped = 0;
        }
        if(result.Data.ContainsKey("coinChance"))
        {
            coinChance = int.Parse(result.Data["coinChance"].Value);
        }
        _null = false;
    }
}
