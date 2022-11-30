using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;
using MatchThreeEngine;
using CodeMonkey;
using System.Threading.Tasks;
using System.Globalization;
using UnityEngine.LowLevel;

[Serializable]
public class Player : MonoBehaviour // extraLooting Currently Does Not Work
{
    private const int playerDamage = 1;
    private const double attackSpeed = 1f;
    private const float attackRange = 1.2f;
    public int inventoryIndex = 0;
    private List<Tool> playerToolInventory;
    public bool HasTool { get => playerToolInventory.Count > 0; }


    public Tool PlayerTool
    {
        get
        {
            if (playerToolInventory.Count > 0)
                return playerToolInventory[inventoryIndex];

            return new Tool();
        }
    }
    public int EPA 
    {
        get
        {
            return epa + PlayerTool.EPA;
        }
        set
        {
            epa = value;
        }
    
    }


    public double AttackDamage
    {
        get
        {
            return playerDamage + PlayerTool.AttackDamage;
        }
    }
    public double AttackSpeed
    {
        get
        {
            return PlayerTool.AttackSpeed;
        }
    }
    public float AttackRange
    {
        get
        {
            return PlayerTool.AttackRange;
        }
    }

    [SerializeField] Text errorText;
    float errorTimer;
    private Player player;
    [SerializeField] LoanScript loans;
    private float timerforattack;
    private double networth;
    private float timer = 0f;
    private float respawntimer = 0f;
    [SerializeField] GameObject startMenu;
    [SerializeField] private Transform attackPoint;
    [SerializeField] Transform parent;

    [SerializeField] private double enemyHealth;
    [SerializeField] private double balance;
    private double coins;
    
    Vector2 respawnPosition;
    [SerializeField] private GameObject _pinata;
    [SerializeField] private GameObject _Player;
    
    [SerializeField] private int energy;
    private static int initialMaxEnergy = 100;
    [SerializeField] private int maxEnergy = 100;
    [SerializeField] private int epa = 5;

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
        string[] temporarySkills;


        private int cloverChance;
        private int trickOrTreat;
        private int trickOrTreatChance;
        private int trickOrTreatRandom;
        [SerializeField] Sprite chest;
        [SerializeField] Animator totAnimation;
        [SerializeField] Animator coinAnimation;
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
        temporarySkills = new string[100];
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
        networth = 0;
        coins = 0;
        respawntimer = 15;
        toolID = -1;
        enemyHealth = Pinata.getHealth();
        timerforattack = 0;
        for (int i = 0; i < inventoryPinata.Length; i++) { inventoryPinata[i] = false; inventoryPet[i] = false; }

        attacking = false;
        balance = 0;
        respawnPosition = respawnPoint.transform.position;
        pinataPoint = _pinata.transform.position;
        newPinata = (GameObject)Instantiate(_pinata, parent, true);
        newPinata.transform.position = respawnPoint.transform.position;
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
            skillTimer2 += CustomTime.deltaTime; skillTimer = 0; 
            attacking = true;
            PlayerTool.AttackDamage = 50;
            PlayerTool.AttackRange = 5;
            PlayerTool.Looting = 15;
            PlayerTool.AttackSpeed = 3;
        }
        else if(skillTimer2 > 12) { 
            skill2Active = false; skillTimer2 = 0;
            PlayerTool.AttackDamage = (int)double.Parse(temporarySkills[0].Replace(',','.'),CultureInfo.InvariantCulture);
            PlayerTool.AttackSpeed = float.Parse(temporarySkills[1].Replace(',', '.'), CultureInfo.InvariantCulture);
            PlayerTool.AttackRange = float.Parse(temporarySkills[2].Replace(',', '.'), CultureInfo.InvariantCulture);
            PlayerTool.Looting = int.Parse(temporarySkills[3]);
            PlayerTool.Durability = int.Parse(temporarySkills[4]);
            main.GetComponent<PlayfabManager>().Save();
        }
        if(rheagod && popTimer <= 6) { popTimer += CustomTime.deltaTime; }
        else if(rheagod && popTimer > 6) { popTimer = 0; PlayerTool.Looting -= popped; popped = 0; }
        if(PlayerTool.Looting > lootingCap) { PlayerTool.Looting = lootingCap; }
        
        if(openShop) { attackButton.SetActive(false); refillButton.SetActive(false); }
        else { attackButton.SetActive(true); refillButton.SetActive(true);  }

        if (loggedIn) { saveTime += Time.deltaTime; }
        if (saveTime > 35f) {
            main.GetComponent<PlayfabManager>().Save(); 
            saveTime = 0f;                                                   
        }
        if (skills[0] || skills[1] || skills[2])
        {
            skillTimer += CustomTime.deltaTime;
        }
    }
    private void Awake()
    {
        shop = this.GetComponent<Shopping>();
        player = this;
        energyRefilled = 0;
        timerRefilled = 0;
    }

    

    public int getLootingCap() { return lootingCap; }
    
    public int getPopped()
    {
        return this.popped;
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) { return; }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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
    public void PurchaseTool(Tool tool)
    {
        playerToolInventory.Add(tool);
        inventoryIndex = playerToolInventory.Count - 1;
    }
    public bool IsInventoryFull()
    {
        return playerToolInventory.Count >= 10;
    }
    public void ToolBroken()
    {
        playerToolInventory.Remove(playerToolInventory[inventoryIndex]);
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

        miniGameMultiplier = _player.getMiniGameMultiplier();
        miniGameMultiplier2 = _player.getMiniGameMultiplier2();
        miniGameMultiplier3 = _player.getMiniGameMultiplier3();
        bossLevel = _player.getBossLevel();
        treeBonus = _player.getTreeBonus();
        energyAttribute = _player.getEnergyAttribute();
        energyTimer = _player.getEnergyTimer();
        makeitworth = _player.getMakeItWorth();
        wrapgod = _player.getWrapGod();
        skills = _player.getSkills();
        skillTimer = _player.getSkillTimer() + diff;
        lootingCap = _player.getLootingCap();
        lootEfficiency = _player.getLootEfficiency();
        extraLooting = _player.getExtraLooting();
        extraAttackSpeed = _player.getExtraAs();
        rheagod = _player.getRheaGod();
        cloverChance = _player.getCloverChance();
        trickOrTreat = _player.getTrickOrTreat();
        trickOrTreatChance = _player.getTrickOrTreatChance();
        trickOrTreatRandom = _player.getTrickOrTreatRandom();
        totCount = _player.getTotCount();
        godOfBugs = _player.getGodOfBugs();
        treasureCount = _player.getTreasureCount();
        main.GetComponent<SkillTree>().setTreeType(_player.getTreeType());
        for (int i = 0; i < main.GetComponent<SkillTree>().getLevels1().Length; i++) {
            main.GetComponent<SkillTree>().getLevels1()[i] = _player.getLevels1()[i];
            main.GetComponent<SkillTree>().getLevels2()[i] = _player.getLevels2()[i];
            main.GetComponent<SkillTree>().getLevels3()[i] = _player.getLevels3()[i];
        }
        if (main.GetComponent<SkillTree>().getTreeType() != 0)
        {
          foreach (var k in main.GetComponent<SkillTree>().getBackgrounds()) { k.color = Color.gray; }
            main.GetComponent<SkillTree>().getBackgrounds()[main.GetComponent<SkillTree>().getTreeType() - 1].color = Color.green;
        }

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
        this.PlayerTool.AttackSpeed = _player.getAttackSpeed();
        this.PlayerTool.AttackRange = _player.getAttackRange();
        this.PlayerTool.AttackDamage = (int)_player.getAttackDamage();
        this.balance = _player.getBalance();
        balance += addB;
        this.energy = _player.getEnergy() + energyRefilled;
        this.maxEnergy = _player.getMaxEnergy();
        if (energy > maxEnergy) { energy = maxEnergy; }
        this.EPA = player.getEPA();
        this.PlayerTool.EPA = _player.getToolEPA();
        this.PlayerTool.Looting = _player.getLooting();
        this.respawntimer = _player.getRespawnTimer() + timerRefilled;
        this.timer = _player.getTimer();
        this.PlayerTool.Durability = _player.getToolDurability();
        this.shop.hasTool = _player.getHasTool();
        this.inventoryPinata = _player.getInventoryPinata();
        this.inventoryPet = _player.getInventoryPet();
        this.coinChance = _player.getCoinChance();
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
        popped = _player.getPopped();
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
            newPinata.transform.position = pinataPoint * Instance.GetCanvas.scaleFactor;
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
    public Vector2 getRespawnPosition()
    {
        return this.respawnPosition;
    }
    public GameObject getOPlayer()
    {
        return this._Player;
    }

    public int getEPA()
    {
        return this.EPA;
    }
    public GameObject getRespawnPoint()
    {
        return this.respawnPoint;
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
    
    public void Loan(int loan) {
        if (loan == 1) { balance += 250; }
        else if(loan == 2) { balance += 500; }
        else if(loan == 3) { balance += 1000; }
    }
    public LoanScript getLoanScript()
    {
        return this.loans;
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

    public void setToolID(int id) { toolID = id; }
    public double getCoins() { return coins; }
    public void addToCoins(double amount) { coins += amount; }
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
            if(level == 4) { extraLooting = 1; }
            lootEfficiency += 0.1;
        }
        else if(skill.ToLower() == "attackspeed")
        {
            level++;
            extraAttackSpeed += level * (PlayerTool.Looting / 105);

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
        else if (skill.ToLower() == "godofbugs")
        {
            godOfBugs = true;
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
            temporarySkills[0] = "" + AttackDamage;
            temporarySkills[1] = "" + AttackSpeed;
            temporarySkills[2] = "" + AttackRange;
            temporarySkills[3] = "" + PlayerTool.Looting;
            temporarySkills[4] = "" + PlayerTool.Durability;
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
        trickOrTreatRandom = 0;
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
    public void Debugg()
    {
        Debug.Log(coinChance);
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
    public int getEnergyAttribute() { return energyAttribute; }
    public float getEnergyTimer() { return this.energyTimer; }
    public int getMakeItWorth() { return this.makeitworth;}
    public int getMiniGameMultiplier() { return this.miniGameMultiplier; }
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

    public async void HitPinata()
    {
        if (timerforattack < 1.0 / attackSpeed)
            return;

        int energyConsume = DetermineEnergyConsume();

        if (energyConsume > energy)
            return;

        GameAssets.Instance.PlayerHit.Play("playerHit");

        await Task.Delay((int)GameAssets.Instance.PlayerHit.GetCurrentAnimatorStateInfo(0).length * 999);

        if (!IsInRage())
            return;

        Instance.PinataObject.TakeDamage();
        timerforattack = 0;

        energy -= energyConsume;

        int add = DetermineLootPerHit();
        balance += add;
        networth += add;
        PopUpMessage.StartPopUpMessageCandy(add, Instance.GetCanvas);


        UsingSkillsOnHit();
    }
    public IEnumerator PinataPopp()
    {
        isDead = true;
        int add = DetermineLoot();
        balance += add;
        networth += add;
        PopUpMessage.StartPopUpMessageCandy(add, Instance.GetCanvas);
        yield return new WaitForSeconds(Instance.PinataObject.RespawnTime);
        Instance.PinataObject.Respawn();
        isDead = false;
    }
    private bool IsInRage()
    {
        return Vector2.Distance(Instance.PinataObject.transform.position, attackPoint.position) < attackRange + 1.6f;
    }

    private int DetermineLoot()
    {
        int add;
        if (Instance.Shop.equipped[1])
        {
            add = (int)((Pinata.getLoot() + (Pinata.getLoot() * PlayerTool.Looting / 10) + Pinata.getLootFromPerk()) * lootEfficiency);
        }
        else if (Instance.Shop.equipped[3])
        {
            add = (int)((Pinata.getLoot() + (Pinata.getLoot() * PlayerTool.Looting / 10) + Pinata.getLootFromPerk()) * lootEfficiency) / 2;
        }
        else if (Instance.Shop.equipped[4] && AttackDamage >= Pinata.getHealth())
        {
            add = (int)((Pinata.getLoot() + (Pinata.getLoot() * PlayerTool.Looting / 10) + Pinata.getLootFromPerk()) * lootEfficiency) * ((Instance.Shop.getPerks()[1] / 100) + 1);
        }
        else
        {
            add = (int)((Pinata.getLoot() + (Pinata.getLoot() * PlayerTool.Looting / 10)) * lootEfficiency);
        }
        return add;
    }
    private int DetermineLootPerHit()
    {
        int add = 0;
        if (Instance.Shop.equipped[1])
            return add;
        else if (Instance.Shop.equipped[3])
        {
            int i = Pinata.getLootPerClick() + 1;
            add = (int)((i + (i * PlayerTool.Looting / 5)) * lootEfficiency);
        }
        else if (Instance.Shop.equipped[4])
        {
            if (CreateRandomChance.Gamble(Instance.Shop.getLevels()[4] * 10, 100))
            {
                PlayerTool.Durability++;
                balance -= Instance.Shop.getPerks()[4];
                //Popup Message
            }
            add = (int)((Pinata.getLootPerClick() + (Pinata.getLootPerClick() * PlayerTool.Looting / 5)) * lootEfficiency);
        }
        else
        {
            add = (int)((Pinata.getLootPerClick() + (Pinata.getLootPerClick() * PlayerTool.Looting / 5)) * lootEfficiency);
        }
        PlayerTool.Durability--;
        return add;
    }
    private int DetermineEnergyConsume()
    {
        int energyConsume;
        if (Instance.Shop.equipped[2])
            energyConsume = EPA / 2;
        else if (Instance.Shop.equipped[3])
        {
            energyConsume = EPA - (int)(shop.getPerks()[2] * (extraAttackSpeed + attackSpeed));
            energyConsume = energyConsume < 3 ? 3 : energyConsume;
        }
        else
            return EPA;


        return energyConsume;
    }


    private void UsingSkillsOnHit()
    {
        if (makeitworth > 0)
        {
            balance += (makeitworth * EPA) / 20;
            networth += (makeitworth * EPA) / 20;
            PopUpMessage.StartPopUpMessageCandy((makeitworth * EPA / 20), Instance.GetCanvas);
        }

        int i = PlayerTool.Looting == 10 ? 1 : 0;
        if (shop.equipped[2] && CreateRandomChance.Gamble(shop.getLevels()[2] + i, 100))
        {
            balance += 100 + (4 * Math.Log(0.00000000217 * Math.Pow(treasureCount, 15))); 
            if (godOfBugs)
                treasureCount++;
            PopUpMessage.StartPopUpMessageCandy(100, Instance.GetCanvas);
            GameAssets.Instance.SecretTreasure.Play("secretTreasure");
        }

        if (trickOrTreatChance > 0 && CreateRandomChance.Gamble(trickOrTreatChance, 10000))
        {
            totAnimation.Play("totAnimation");
            int added = UnityEngine.Random.Range((-1 * (trickOrTreat + 1)), trickOrTreat);
            balance += added;
            networth += added;
            totCount++;
            trickOrTreat = 40 + (int)(4 * Math.Log(0.00000000217 * Math.Pow(totCount, 15)));
            PopUpMessage.StartPopUpMessageCandy(added, Instance.GetCanvas);
            AudioManager.PlaySound("pop");
        }

        if (coinChance > 0 && CreateRandomChance.Gamble(coinChance, 10000))
        {
            coinAnimation.Play("coinDrop");
            //playsound
            coins++;
        }


        //  Clover chance is determined where Loot Per Click is determined
    }

}
