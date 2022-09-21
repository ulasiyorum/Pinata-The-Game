using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;
using MatchThreeEngine;
using CodeMonkey;
[Serializable]
public class AttackingPinata : MonoBehaviour
{
    [SerializeField] Text errorText;
    float errorTimer;
    [SerializeField] Canvas canvas;
    System.DateTimeOffset timeThen;
    System.DateTimeOffset timeNow;
    private AttackingPinata player;
    [SerializeField] LoanScript loans;
    [SerializeField] private float timerforattack;
    private double networth;
    private float timer = 0f;
    private float respawntimer = 0f;
    [SerializeField] GameObject startMenu;
    [SerializeField] private Transform attackPoint;
    [SerializeField] Transform parent;
    [SerializeField] private float attackRange;
    [SerializeField] private double enemyHealth;
    [SerializeField] private double balance;
    private double coins;
    [SerializeField] private int looting;
    Vector2 respawnPosition;
    [SerializeField] private GameObject _pinata;
    [SerializeField] private GameObject Player;
    [SerializeField] private double playerDamage = 1;
    [SerializeField] private double attackDamage;
    [SerializeField] private double attackSpeed = 1;
    [SerializeField] public int toolDurability;
    [SerializeField] private int energy;
    private static int initialMaxEnergy = 100;
    [SerializeField] private int maxEnergy = 100;
    [SerializeField] private int EPA = 5;
    [SerializeField] private int toolEPA = 0;
    [SerializeField] private GameObject respawnPoint;
    [SerializeField] private Vector2 pinataPoint;
    public bool openShop = false;
    [SerializeField] private bool isDead;
    [SerializeField] Shopping shop;
    [SerializeField] GameObject newPinata;
    [SerializeField] GameObject main;
    [SerializeField] bool attacking;
    [SerializeField] GameObject attackButton;
    [SerializeField] int exitTime;
    [SerializeField] GameObject anim;
    [SerializeField] Animator pinataDied;
    [SerializeField] Animator playerHit;
    [SerializeField] Animator st;
    [SerializeField] Animator pinataShake;
    
    
        private int energyAttribute;
        private float energyTimer;
        private int makeitworth;
        private bool wrapgod;
        public int wrapShot;
        private int miniGameMultiplier;


        private bool[] skills;
        private float skillTimer;


        private int lootingCap;
        private double lootEfficiency;
        private int extraLooting;
        private float extraAttackSpeed;
        private bool rheagod;
        private int popped;
        private float popTimer;
        private double miniGameMultiplier2;
        private bool skill2Active;
        private float skillTimer2;
        object[] temporarySkills;


        private int cloverChance;
        private int trickOrTreat;
        private int trickOrTreatChance;
        private int trickOrTreatRandom;
        [SerializeField] Sprite chest;
        private int totCount;
        private int coinChance;
        private bool godOfBugs;
        private int treasureCount;
        private double miniGameMultiplier3;
        private int bossLevel;

        private bool[] treeBonus;
    // 1'in bonusu: Oyuna tekrar girdiðinde yenilenen enerji, %0.1 þeker olarak eklenir.
    // 2'nin bonusu: Skiller'in cooldown'u %20 azalýr
    // 3'ün bonusu: Günlük bonus coin cap'i 3 olur.

    private int toolID;
    double tempAttackDamage;
    float saveTime = 0f;
    int energyRefilled;
    float timerRefilled;
    public bool loggedIn;
    public static float deltaTime;
    TimeSpan interval;
    [SerializeField] GameObject refillButton;
    bool[] inventoryPinata = new bool[100];
    bool[] inventoryPet = new bool[100];
    private bool game;
    private static ActualPlayerData _player;
    private void Start()
    {
        treeBonus = new bool[3];
        miniGameMultiplier3 = 0;
        miniGameMultiplier2 = 0;
        bossLevel = 0;
        treasureCount = 4;
        coinChance = 0;
        totCount = 4;
        trickOrTreatChance = 0;
        skills = new bool[5];
        temporarySkills = new object[100];
        cloverChance = 0;
        extraAttackSpeed = 0;
        extraLooting = 0;
        lootEfficiency = 1;
        lootingCap = 10;
        wrapShot = 0;
        miniGameMultiplier = 0;
        wrapgod = false;
        makeitworth = 0;
        energyAttribute = 1;
        energyTimer = 10;
        shop.callScenes(0);
        timeNow = DateTimeOffset.Now;
        timeThen = DateTimeOffset.Now;
        networth = 0;
        coins = 0;
        respawntimer = 15;
        toolID = -1;
        enemyHealth = Pinata.getHealth();
        timerforattack = 0;
        attackSpeed = 1;
        toolDurability = 0;
        for (int i = 0; i < inventoryPinata.Length; i++) { inventoryPinata[i] = false; inventoryPet[i] = false; }
        pinataDied = anim.GetComponent<Animator>();

        attacking = false;
        balance = 0;
        attackDamage += playerDamage;
        respawnPosition = respawnPoint.transform.position;
        pinataPoint = _pinata.transform.position;
        newPinata = (GameObject)Instantiate(_pinata, parent, true);
        newPinata.transform.position = respawnPoint.transform.position;
        EPA += toolEPA;
        energy = maxEnergy;
        if (PlayFab.PlayFabClientAPI.IsClientLoggedIn()) {
            game = true;
            main.GetComponent<PlayfabManager>().Load();
        }  
    }
    private void OnEnable()
    {
        errorTimer = 3;
    }
    void FixedUpdate()
    {
        errorTimer += Time.deltaTime;
        if(errorTimer >= 2f) { errorText.text = ""; }
        if(skill2Active && skillTimer2 <= 12) 
        { 
            skillTimer2 += deltaTime; skillTimer = 0; attacking = true;
            attackDamage = 50;
            attackRange = 5;
            looting = 15;
            attackSpeed = 3;
        }
        else if(skillTimer2 > 12) { skill2Active = false; skillTimer2 = 0;
            attackDamage = (double)temporarySkills[0];
            attackSpeed = (float)temporarySkills[1];
            attackRange = (float)temporarySkills[2];
            looting = (int)temporarySkills[3];
            toolDurability = (int)temporarySkills[4];
        }
        if(rheagod && popTimer <= 6) { popTimer += deltaTime; }
        else if(rheagod && popTimer > 6) { popTimer = 0; looting -= popped; popped = 0; }
        if(looting > lootingCap) { looting = lootingCap; }
        timeNow = DateTime.Now;
        interval = timeNow - timeThen;
        deltaTime = (float)interval.TotalSeconds;
        
        if(openShop) { attackButton.SetActive(false); refillButton.SetActive(false); }
        else { attackButton.SetActive(true); refillButton.SetActive(true);  }
        refillEnergy();
        attackPinata();
        respawnPinata();

        if (loggedIn) { saveTime += Time.deltaTime; }
        if (saveTime > 35f) {
            main.GetComponent<PlayfabManager>().Save(); 
            saveTime = 0f;                                                   
        }
        if (skills[0] || skills[1] || skills[2])
        {
            skillTimer += deltaTime;
        }
        timeThen = DateTime.Now;
    }
    private void Awake()
    {
        
        shop = this.GetComponent<Shopping>();
        player = this.GetComponent<AttackingPinata>();
        energyRefilled = 0;
        timerRefilled = 0;
    }

    void refillEnergy()
    {
        if (energy < 0)
        {
            energy = 0;
        }
        if (energy >= maxEnergy) { energy = maxEnergy; }
        else
        {
            if (timer < energyTimer + 1 && energy <= maxEnergy)
                timer += deltaTime;
            if (timer > energyTimer && energy <= maxEnergy)
            {
                energy += 1 + UnityEngine.Random.Range(0,energyAttribute);
                timer -= 10;
            }
        }
    }

    public int getLootingCap() { return lootingCap; }
    void attackPinata()
    {
        if(timerforattack <= 1.0 / attackSpeed)
        {
            timerforattack += Time.deltaTime;
        }
        else {
            if (attacking && !openShop)
            {
                if (energy < EPA && !shop.equipped[2] && !shop.equipped[3])
                {
                    print("Not Enough Energy");
                }
                else if (((shop.equipped[2] || shop.equipped[3]) && energy >= shop.getTempEPA()))
                {
                    if ((shop.hasTool && toolDurability > 0) || !shop.hasTool)
                    {
                        attacking = false;
                        damagePinata();
                        if (!shop.equipped[2] && !shop.equipped[3])
                        {
                            if (!skill2Active) { energy -= EPA; }
                        }
                        else
                        {
                            if (!skill2Active) { energy -= shop.getTempEPA(); }
                        }

                        playerHit.Play("playerAttack", 0);
                        AudioManager.PlaySound("woosh");


                    }
                }
                else if (((shop.equipped[2]) && energy < shop.getTempEPA()))
                {
                    print("Not Enough Energy");
                }
                else if (((shop.equipped[3]) && energy < shop.getTempEPA()))
                {
                    print("Not Enough Energy");
                }
                else
                {
                    if ((shop.hasTool && toolDurability > 0) || !shop.hasTool)
                    {
                        attacking = false;
                        damagePinata();
                        if (!shop.equipped[2])
                        {
                            if (!skill2Active) { energy -= EPA; }
                        }
                        else
                        {
                            if (!skill2Active) { energy -= shop.getTempEPA(); }
                        }
                        
                        playerHit.Play("playerAttack",0);
                        AudioManager.PlaySound("woosh");


                    }
                }
                timerforattack = 0f;
            }
        }
    }
    void damagePinata()
    {
        if(trickOrTreatChance > 0 && UnityEngine.Random.Range(0,10000) <= trickOrTreatChance)
        {
            //animasyon
            int added = UnityEngine.Random.Range((-1 * (trickOrTreat + 1)), trickOrTreat);
            balance += added;
            networth += added;
            totCount++;
            trickOrTreat = 40 + (int)(4 * (Math.Log(0.00000000217 * (Math.Pow(totCount,15)))));
        }
        if(coinChance > 0 && UnityEngine.Random.Range(0,10000) <= coinChance)
        {
            //animasyon
            coins++;
        }
        int random = UnityEngine.Random.Range(0,6);
            if(_pinata == null) { return; }
        if (Vector2.Distance(_pinata.transform.position, attackPoint.position) < attackRange + 1.6f)
        {
            if (shop.equipped[1] || shop.equipped[4]) { enemyHealth -= shop.getTemporaryAttackDamage(); }
            else
            {
                enemyHealth -= attackDamage;
                if (shop.equipped[0] && wrapShot < 3) { wrapShot++; }
                else if (shop.equipped[0] && wrapShot == 3)
                {
                    wrapShot = 0; addToBalance(); if (looting > 3) { addToBalance(); }
                    if (looting > 6) { addToBalance(); }
                    if (looting > 9) { addToBalance(); }
                }
                if (!shop.equipped[2] && !shop.equipped[3])
                {
                    balance += (makeitworth * EPA) / 20;
                    networth += (makeitworth * EPA) / 20;
                    PopUpMessage.StartPopUpMessageCandy((makeitworth*EPA/20), canvas);
                }
                
                else
                {
                    balance += (makeitworth * shop.getTempEPA()) / 20;
                    networth += (makeitworth * shop.getTempEPA()) / 20;
                    PopUpMessage.StartPopUpMessageCandy((makeitworth * shop.getTempEPA() / 20), canvas);
                }
            }
                String s = "CandyHit" + (int)UnityEngine.Random.Range(0,4);
            Debug.Log(s);
                pinataDied.Play(s);
            AudioManager.PlaySound("hit");
            pinataShake.Play("pinataShake");
            if(shop.hasTool && shop.equipped[4] && shop.getJackpot()) { balance -= shop.getPerks()[2]; }
            else if (shop.hasTool) { toolDurability--; if (shop.equipped[2] && !godOfBugs) { toolDurability--; }
                if (toolDurability <= 0) { toolDurability = 0; shop.hasTool = false; toolBroken(); } }
            if (shop.equipped[2])
            {
                if (cloverChance > 0 && cloverChance >= random)
                {
                    int inclusive = newPinata.GetComponent<Pinata>().getLootRange0()
                          + ((cloverChance * newPinata.GetComponent<Pinata>().getLootRange1()) / 16);
                    if(inclusive > newPinata.GetComponent<Pinata>().getLootRange1())
                    {
                        inclusive = newPinata.GetComponent<Pinata>().getLootRange1();
                    }
                  int cloverLoot = (int)UnityEngine.Random.Range(inclusive, newPinata.GetComponent<Pinata>().getLootRange1() + 1);
                    networth += cloverLoot + (((cloverLoot * shop.getTempLooting()) / 5) * lootEfficiency);
                    balance += cloverLoot + (((cloverLoot * shop.getTempLooting()) / 5) * lootEfficiency);
                    PopUpMessage.StartPopUpMessageCandy((int)(cloverLoot + (((cloverLoot * shop.getTempLooting()) / 5) * lootEfficiency)), canvas);
                }
                else 
                { 
                networth += Pinata.getLootPerClick() + (((Pinata.getLootPerClick() * shop.getTempLooting()) / 5) * lootEfficiency);
                balance += Pinata.getLootPerClick() + (((Pinata.getLootPerClick() * shop.getTempLooting()) / 5) * lootEfficiency);
                PopUpMessage.StartPopUpMessageCandy((int)(Pinata.getLootPerClick() + (((Pinata.getLootPerClick() * shop.getTempLooting()) / 5) * lootEfficiency)) , canvas);
                }
            }
            else
            {
                if (cloverChance > 0 && cloverChance >= random)
                {
                    int inclusive = newPinata.GetComponent<Pinata>().getLootRange0()
                          + ((cloverChance * newPinata.GetComponent<Pinata>().getLootRange1()) / 16);
                    if (inclusive > newPinata.GetComponent<Pinata>().getLootRange1())
                    {
                        inclusive = newPinata.GetComponent<Pinata>().getLootRange1();
                    }
                    int cloverLoot = (int)UnityEngine.Random.Range(inclusive, newPinata.GetComponent<Pinata>().getLootRange1() + 1);
                    networth += cloverLoot + (((cloverLoot * shop.getTempLooting()) / 5) * lootEfficiency);
                    balance += cloverLoot + (((cloverLoot * shop.getTempLooting()) / 5) * lootEfficiency);
                    PopUpMessage.StartPopUpMessageCandy((int)(cloverLoot + (((cloverLoot * shop.getTempLooting()) / 5) * lootEfficiency)),canvas);
                }
                else
                {
                    networth += Pinata.getLootPerClick() + (((Pinata.getLootPerClick() * looting) / 5) * lootEfficiency);
                    balance += Pinata.getLootPerClick() + (((Pinata.getLootPerClick() * looting) / 5) * lootEfficiency);
                    PopUpMessage.StartPopUpMessageCandy((int)(Pinata.getLootPerClick() + (((Pinata.getLootPerClick() * looting) / 5) * lootEfficiency)), canvas);
                }
                
            }
                if (shop.getJackpot() && shop.equipped[2] && !godOfBugs) { balance += 100; st.Play("SecretTreasure");
                PopUpMessage.StartPopUpMessageCandy(100, canvas);
                }
                else if(shop.getJackpot() && shop.equipped[2] && godOfBugs) 
                {
                int added = 100 + (int)(4 * (Math.Log(0.00000000217 * (Math.Pow(treasureCount, 15)))));
                balance += added;
                networth += added;
                PopUpMessage.StartPopUpMessageCandy(added, canvas);
                treasureCount++;
            }
            
        }

        if (enemyHealth <= 0)
        {
            if (rheagod)
            {
                popTimer = 0;
                if (looting < lootingCap)
                {
                    popped++;
                    looting++;
                }
            }
            respawntimer = 0f;
            enemyHealth = 0;
            AudioManager.PlaySound("pop");
            pinataDied.Play("CandyDrop");
            Destroy(_pinata);
            _pinata.GetComponent<Pinata>().getImages()[Pinata._this].SetActive(false);
            isDead = true;
            if (shop.equipped[2])
            {
                networth += (Pinata.getLoot() + (((Pinata.getLoot() * shop.getTempLooting()) / 10)) * lootEfficiency);
                balance += (Pinata.getLoot() + (((Pinata.getLoot() * shop.getTempLooting()) / 10)) * lootEfficiency);
                PopUpMessage.StartPopUpMessageCandy((int)(Pinata.getLoot() + (((Pinata.getLoot() * shop.getTempLooting()) / 10)) * lootEfficiency) , canvas);
            }
            else if ((shop.equipped[4] || (shop.equipped[1] && rheagod && inventoryPet[4] && shop.getLevels()[4] == 5)) && shop.getTemporaryAttackDamage() >= newPinata.GetComponent<Pinata>().getHealth2())
            {
                networth += (Pinata.getLoot() + (((Pinata.getLoot() * looting) / 10)) * lootEfficiency) + ((Pinata.getLoot() + (((Pinata.getLoot() * shop.getTempLooting()) / 10) * lootEfficiency)) * (shop.getPerks()[1]/100));
                balance += (Pinata.getLoot() + (((Pinata.getLoot() * looting) / 10)) * lootEfficiency) + ((Pinata.getLoot() + (((Pinata.getLoot() * shop.getTempLooting()) / 10) * lootEfficiency)) * (shop.getPerks()[1] / 100));
                PopUpMessage.StartPopUpMessageCandy((int)((Pinata.getLoot() + (((Pinata.getLoot() * looting) / 10)) * lootEfficiency) + ((Pinata.getLoot() + (((Pinata.getLoot() * shop.getTempLooting()) / 10) * lootEfficiency)) * (shop.getPerks()[1] / 100))), canvas);
            }
            else
            {
                networth += (Pinata.getLoot() + (((Pinata.getLoot() * looting) / 10)) * lootEfficiency) + Pinata.getLootFromPerk();
                balance += (Pinata.getLoot() + (((Pinata.getLoot() * looting) / 10)) * lootEfficiency) + Pinata.getLootFromPerk();
                PopUpMessage.StartPopUpMessageCandy((int)((Pinata.getLoot() + (((Pinata.getLoot() * looting) / 10)) * lootEfficiency) + Pinata.getLootFromPerk()), canvas);
            }

            if (loggedIn)
            {
                main.GetComponent<PlayfabManager>().Save();
                saveTime = 0f;
                Debug.Log("Game Saved");
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) { return; }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    void respawnPinata()
    {
        if (respawntimer <= Pinata.getRespawnTime() + 4)
        {
            respawntimer += deltaTime;
        }
        if (respawntimer > Pinata.getRespawnTime() && isDead)
        {

            newPinata.transform.position = pinataPoint;
                _pinata = newPinata;
                newPinata = (GameObject)Instantiate(_pinata, parent, true);
                newPinata.transform.position = respawnPoint.transform.position;
                _pinata.GetComponent<Pinata>().getImages()[Pinata._this].SetActive(true);
                _pinata.SetActive(true);
                enemyHealth = Pinata.getHealth();
                isDead = false;
            
        }
        else if(respawntimer < Pinata.getRespawnTime() && !isDead)
        {
            enemyHealth = 0;
            _pinata.GetComponent<Pinata>().getImages()[Pinata._this].SetActive(false);
            Destroy(_pinata);
            isDead = true;
        }
    }
    public double getBalance()
    {
        return balance;
    }
    public void withdraw(double amount)
    {
            balance -= amount;

    }
    public void deposit(double amount)
    {
        balance += amount;
    }
    public void updateTools()
    {
        toolID = shop.getID();
        EPA = shop.getEPA();
        toolDurability = shop.getDurability();
        attackSpeed = shop.getSpeed() + extraAttackSpeed;
        attackDamage = shop.getDamage() + playerDamage;
        looting = shop.getLooting() + extraLooting;
        attackRange = shop.getRange();
    }
    public void toolBroken()
    {
        toolID = -1;
        attackDamage = 1;
        attackSpeed = 1 + extraAttackSpeed;
        attackRange = 1.2f;
        looting = 0 + extraLooting;
        EPA = 5;
        toolDurability = 0;
        shop.hasTool = false;
    }
    public AttackingPinata getPlayer()
    {
        return player;
    }
    public int getEnergy()
    {
        return this.energy;
    }
    public int getMaxEnergy()
    {
        return this.maxEnergy;
    }
    public float getRespawnTimer()
    {
        return this.respawntimer;
    }
    public void shopOpen()
    {
        openShop = true;
        
    }
    public void shopClose()
    {
        openShop = false;
    }
    public GameObject getPinata()
    {
        return _pinata;
    }
    public bool getIsDead()
    {
        return isDead;
    }
    public void setHealth(double amount)
    {
        enemyHealth = amount;
    }
    public void attackingBool(bool x)
    {
        attacking = x;
    }
    public void reset()
    {
       // Application.LoadLevel(0);
    }
    
    public void Load()
    {
        // TActualPlayerData balanceOfPlayer;
        // if (File.Exists(Application.persistentDataPath + "/temporary.bin"))
        // { balanceOfPlayer = TemporarySave.Load(); }
        // else { TemporarySave.Save(this);
        //    balanceOfPlayer = new TActualPlayerData(this);
        // }

        // balance = balanceOfPlayer.balance;

        ActualPlayerData _player = main.GetComponent<PlayfabManager>().getPlayerData();
        System.DateTimeOffset startTime = System.DateTimeOffset.Now;
        this.exitTime = _player.getExitTime();
        this.isDead = _player.getIsDead();
        if (isDead)
        {
            Destroy(_pinata);
            this.enemyHealth = _player.getEnemyHealth();
            isDead = true;
        }
        int diff = (startTime.Year * 365 * 24 * 60 * 60 + startTime.Month * 30 * 24 * 60 * 60
           + startTime.Day * 24 * 60 * 60 + startTime.Hour * 60 * 60 + startTime.Minute * 60 + startTime.Second)
           - exitTime;
        energyRefilled = (diff / (int)energyTimer) * ((energyAttribute+1)/2);
        int addB = 0;
        if(energyRefilled > maxEnergy && treeBonus[0]) { addB = (energyRefilled - maxEnergy) / 1000; }
        AdsManager.adCount = _player.getAdCount();
        AdsManager.adTimer = diff;
        BonusCoin.setBonusCounter(_player.getBonusCounter());
        MiniGameManager.setPlayForFree(_player.getPlayForFree());
        MiniGameManager.setFreeTimer(_player.getFreeTimer() + diff);
        float bonusTimerAdd = _player.getBonusTimer() + diff;
        BonusCoin.setBonusTimer(bonusTimerAdd);
        Objectives.setDailyObj(_player.getDailyObj());
        Objectives.setObjTimer(_player.getObjTimer());
        if (AdsManager.adCount > 5.0001) { AdsManager.adCount = 0; }
        if (isDead)
        {
            timerRefilled = diff;
        }
        else
        {
            timerRefilled = 0;
        }
        Debug.Log("Timer refilled: " + timerRefilled);
        this.attackSpeed = _player.getAttackSpeed();
        this.attackRange = _player.getAttackRange();
        this.attacking = _player.getAttacking();
        this.attackDamage = _player.getAttackDamage();
        this.balance = _player.getBalance();
        balance += addB;
        this.energy = _player.getEnergy() + energyRefilled;
        this.maxEnergy = _player.getMaxEnergy();
        if (energy > maxEnergy) { energy = maxEnergy; }
        this.EPA = _player.getEPA();
        this.looting = _player.getLooting();
        this.playerDamage = _player.getPlayerDamage();
        this.respawntimer = _player.getRespawnTimer() + timerRefilled;
        this.timer = _player.getTimer();
        this.toolDurability = _player.getToolDurability();
        this.toolEPA = _player.getToolEPA();
        this.shop.hasTool = _player.getHasTool();
        this.inventoryPinata = _player.getInventoryPinata();
        this.inventoryPet = _player.getInventoryPet();
        if (_player.equipped == null)
        {
            _player.equipped = new bool[shop.equipped.Length];
            _player.equippedFalse();
        }
        else
        {
            this.shop.equipped = _player.equipped;
        }
        int b = 0;
        shop.setTemporaryAttackDamage(_player.getTempAD());
        Pinata._this = _player.getThis();
        Pinata.setRespawnTime(_player.getRespawnTime());
        Pinata.setHealth(_player.getHealth());
        Pinata.setLoot(_player.getLoot());
        Pinata.setLootRange0(_player.getLootRange0());
        loans.setLoanCount(_player.getLoanCount());
        loans.setMoneyToPay(_player.getMoneyToPay());
        Pinata.setLootRange1(_player.getLootRange1());
        Pinata.setTempRespawnTime(_player.getTempRespawnTime());
        this.enemyHealth = _player.getEnemyHealth();
        shop.oncePet3 = _player.getOncePet3();
        this.toolID = _player.getToolID();
        coins = _player.getCoins();
        networth = _player.getNetworth();
        if (skills[0] || skills[1] || skills[2])
        {
            skillTimer += diff;
        }
        if (respawntimer > Pinata.getRespawnTime() && isDead)
        {
            newPinata.transform.position = new Vector2(pinataPoint.x, pinataPoint.y);
            _pinata = newPinata;
            newPinata = (GameObject)Instantiate(_pinata, parent, true);
            newPinata.transform.position = respawnPoint.transform.position;
            _pinata.GetComponent<Pinata>().getImages()[Pinata._this].SetActive(true);
            enemyHealth = _pinata.GetComponent<Pinata>().getHealth2();
            isDead = false;
        }
        else if (!isDead && _pinata == null)
        {
            newPinata.transform.position = pinataPoint * canvas.scaleFactor;
            _pinata = newPinata;
            newPinata = (GameObject)Instantiate(_pinata, parent, true);
            newPinata.transform.position = respawnPoint.transform.position;
            _pinata.GetComponent<Pinata>().getImages()[Pinata._this].SetActive(true);
            _pinata.SetActive(true);
            enemyHealth = _player.getEnemyHealth();
            isDead = false;
        }
        shop.setLevels(_player.getLevels());
        shop.setInventoryScenes(_player.getInventoryScenes());
        for (int i = 0; i < shop.getInventoryScenes().Length; i++)
        {

            if (shop.getInventoryScenes()[i]) { b = i; }

        }
        shop.callScenes(b);
        //player.transform.position = new Vector3(_player.getLocation()[0],_player.getLocation()[1], _player.getLocation()[2]);
        loggedIn = true;
        if (game)
        {
            balance += Board.getCandy();
            networth += Board.getCandy() + (int)(Board.getCandy() * (miniGameMultiplier2/100));
            energy += Board.getEnergy() + (int)((Board.getEnergy() * ((double)miniGameMultiplier/100)));
            if (MiniGameManager.play())
            { }
            else { coins -= 3; }
            Board.resetScore();
            game = false;
        }

    }

    public void LoginMenuFunction()
    {
        main.GetComponent<PlayfabManager>().Save();
        startMenu.SetActive(false);
        loans.loanB.SetActive(true);
        loggedIn = true;

    }
    public void StartMenuFunction()
    {
        if (ActualPlayerData._null) { LoginMenuFunction(); return; }
        Load();
            startMenu.SetActive(false);
            loans.loanB.SetActive(true);
    }
    public void Demo()
    {
        startMenu.SetActive(false);
        loans.loanB.SetActive(true);
    }

    ////////////////////// GET METHODS FOR SAVEFILE
    
    public float getTimer()
    {
        return timer;
    }
    public double getEnemyHealth() { return enemyHealth; }
    public double getPlayerDamage()
    {
        return this.playerDamage;
    }
    public Transform getAttackPoint()
    {
        return this.attackPoint;
    }
    public float getTimerForAttack()
    {
        return this.timerforattack;
    }
    public GameObject getStartMenu()
    {
        return this.startMenu;
    }
    public Transform getParent()
    {
        return this.parent;
    }
    public float getAttackRange()
    {
        return this.attackRange;
    }
    public int getLooting()
    {
        return this.looting;
    }
    public Vector2 getRespawnPosition()
    {
        return this.respawnPosition;
    }
    public GameObject getOPlayer()
    {
        return this.Player;
    }
    public double getAttackDamage()
    {
        return this.attackDamage;
    }
    public double getAttackSpeed()
    {
        return this.attackSpeed;
    }
    public int getToolDurability()
    {
        return this.toolDurability;
    }
    public int getEPA()
    {
        return this.EPA;
    }
    public int getToolEPA() 
    { 
        return this.toolEPA; 
    }
    public GameObject getRespawnPoint()
    {
        return this.respawnPoint;
    }
    public Vector2 getPinataPoint()
    {
        return this.pinataPoint;
    }
    public Shopping getShop()
    {
        return this.shop;
    }
    public GameObject getNewPinata()
    {
        return this.newPinata;
    }
    public GameObject getMain()
    {
        return this.main;
    }
    public bool getAttacking()
    {
        return this.attacking;
    }
    public GameObject getAttackButton()
    {
        return this.attackButton;
    }
    public int getExitTime()
    {
        return this.exitTime;
    }
    
    public void adReward()
    {
        energy += 100;
    }
    public double getNetworth()
    {
        return this.networth;
    }
    public bool[] getInventoryPinata()
    {
        return inventoryPinata;
    }
    public bool[] getInventoryPet() { return inventoryPet; }
    public void setMaxEnergy(int maxEnergy)
    {
        this.maxEnergy = maxEnergy;
    }
    public void addToBalance()
    {
        balance++;
    }
    public void removeFromBalance()
    {
        balance--;
    }
    public void removeFromBalance(double a)
    {
        balance -= a;
    }
    public void addToEnergy()
    {
        energy += 1 + UnityEngine.Random.Range(0, energyAttribute);
    }
    public void addToEnergy(int en)
    {
        if (coins >= 1 && en == 25 && energy != 100)
        {
            energy += en;
            coins--;
        }
        else if(en == 3)
        {
            energy += en;
        }
    }
    public int getInitialMaxEnergy()
    {

        return initialMaxEnergy;
    }
    public void setAD(double ad)
    {
        this.attackDamage = ad;
    }
    public void Loan(int loan) {
        if (loan == 1) { balance += 250; }
        else if(loan == 2) { balance += 500; }
        else if(loan == 3) { balance += 1000; }
    }
    public LoanScript getLoanScript()
    {
        return this.loans;
    }

    IEnumerator cd(float sec)
    {
        yield return new WaitForSeconds(sec);
    }
    public int getToolID() { return toolID; }
    public void rTimer(float time) {

        if (coins >= 1 && time == 30 && isDead)
        {
            respawntimer += time;
            coins--;
        }
        else if (coins >= 5 && time == 300 && isDead)
        {
            respawntimer += time;
            coins -= 5;
        }

    }
    public void rTimer()
    {
        respawntimer += 30;
    }
    public void setToolID(int id) { toolID = id; }
    public double getCoins() { return coins; }
    public void addToCoins(double amount) { coins += amount; }
    public void removeFromCoins(double amount) { coins -= amount; } 
    public void setEnemyHealth(double health) { enemyHealth = health; }
    public bool[] getTreeBonus() { return treeBonus; }
    public void PurchaseSkill(string skill, int level)
    {
        if(level >= 5) { return; }
        if (skill.ToLower() == "energytime")
        {
            level++;
            if (level <= 3) { energyTimer--; }
            else
            {
                energyTimer -= 1 * (level / 2);
            }
        }
        else if (skill.ToLower() == "energymultiplier")
        {
            level++;
            if (level <= 3) { energyAttribute++; }
            else
            {
                energyAttribute += 1 * (level / 2);
            }
        }
        else if (skill.ToLower() == "makeitworth")
        {
            level++;
            if (level <= 2) { makeitworth += 1; }
            else if (level <= 4) { makeitworth += 2; }
            else
            {
                makeitworth += 4;
            }
        }
        else if (skill.ToLower() == "wrapgod")
        {
            wrapgod = true;
        }
        else if (skill.ToLower() == "minigamer")
        {
            level++;
            if (level <= 3) { miniGameMultiplier += 5; }
            else if (level == 4) { miniGameMultiplier += 10; }
            else { miniGameMultiplier += 15; }
        }
        else if (skill.ToLower() == "laststand")
        {
            for (int i = 0; i < skills.Length; i++) { skills[i] = false; }
            skills[0] = true;
        }
        else if (skill.ToLower() == "lootcap")
        {
            lootingCap++;
        }
        else if (skill.ToLower() == "efficiency")
        {
            if(level == 4) { looting++; extraLooting = 1; }
            lootEfficiency += 0.1;
        }
        else if(skill.ToLower() == "attackspeed")
        {
            level++;
            extraAttackSpeed += level * (looting/105);

        }
        else if(skill.ToLower() == "rheagod")
        {
            rheagod = true;
        }
        else if(skill.ToLower() == "minigamer2")
        {
            level++;
            if (level <= 3) { miniGameMultiplier2 += 5; }
            else if (level == 4) { miniGameMultiplier2 += 10; }
            else { miniGameMultiplier2 += 15; }
        }
        else if(skill.ToLower() == "rush")
        {
            for (int i = 0; i < skills.Length; i++) { skills[i] = false; }
            skills[1] = true;
        }
        else if(skill.ToLower() == "clover")
        {
            cloverChance++;
        }
        else if(skill.ToLower() == "trickortreat")
        {
            level++;
            if (level == 1) { trickOrTreatChance = 10; }
            else { trickOrTreatChance += (int)(10 / ((double)level / 2)); }
        }
        else if(skill.ToLower() == "treasure")
        {
            level++;
            coinChance = 4 * level;
        }
        else if(skill.ToLower() == "minigamer3")
        {
            level++;
            if (level <= 3) { miniGameMultiplier3 += 5; }
            else if (level == 4) { miniGameMultiplier3 += 10; }
            else { miniGameMultiplier3 += 15; }
        }
        else if(skill.ToLower() == "arachno")
        {
            for (int i = 0; i < skills.Length; i++) { skills[i] = false; }
            skills[2] = true;
        }
    }
    public bool getWrapGod()
    {
        return wrapgod;
    }
    public void useSkill()
    {
        if ((skills[0] && skillTimer >= 560) || (skills[0] && treeBonus[1] && skillTimer >= 448))
        {
            energy = maxEnergy;
            // animasyon oynasýn
            skillTimer = 0;
        }
        else if ((skills[1] && skillTimer >= 700) || (skills[1] && treeBonus[1] && skillTimer >= 560))
        {
            skill2Active = true;
            temporarySkills[0] = attackDamage;
            temporarySkills[1] = attackSpeed;
            temporarySkills[2] = attackRange;
            temporarySkills[3] = looting;
            temporarySkills[4] = toolDurability;
            skillTimer = 0;
        }
        else if((skills[2] && skillTimer >= 300) || (skills[2] && treeBonus[1] && skillTimer >= 240))
        {
            //Instantiate
            //bossHealth = 20 + (25 * ln(bossLevel))
            //boss öldürmek = 67ln(bossLevel) 
            //despawn = X saniye
            bossLevel++;
            skillTimer = 0;
        }
        else
        {
            errorText.text = "Skill is not available";
            errorTimer = 0;
        }
    }
    public void resetTree()
    {
        wrapShot = 0;
        miniGameMultiplier = 0;
        wrapgod = false;
        makeitworth = 0;
        energyAttribute = 1;
        energyTimer = 10;
        skills[0] = false;

        lootEfficiency = 1;
        lootingCap = 10;
        extraLooting = 0;
        extraAttackSpeed = 0;
        rheagod = false;
        miniGameMultiplier2 = 0;
        skills[1] = false;

        cloverChance = 0;
        trickOrTreat = 40;
        trickOrTreatChance = 0;
        totCount = 4;
        coinChance = 0;
        treasureCount = 4;
        godOfBugs = false;
        miniGameMultiplier3 = 0;
        bossLevel = 0;
        skills[2] = false;
    }
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
}
