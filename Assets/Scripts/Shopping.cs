using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

[Serializable]
public class Shopping : MonoBehaviour
{
    public float timer0 = 0;
    public float timer1 = 0;
    public float timer2 = 0;
    [SerializeField] private int toolEPA;
    [SerializeField] private int toolDurability;
    [SerializeField] private double attackDamage;
    [SerializeField] private double attackSpeed;
    [SerializeField] private int looting;
    [SerializeField] private float attackRange;
    [SerializeField] Player Player;
    [SerializeField] GameObject currentLevel;
    [SerializeField] private int tempEPA;
    private int tempLooting;
    [SerializeField] public Text currentLevelText;
    [SerializeField] GameObject[] petSkins;
    [SerializeField] GameObject[] toolSkins;
    [SerializeField] GameObject[] scenes;
    [SerializeField] GameObject startSkin;
    private double temporaryAttackDamage;
    private bool jackpot = false;
    [SerializeField] GameObject[] youHaves = new GameObject[25];
    public bool hasTool;
    public bool[] equipped = new bool[25];
    private bool[] inventoryScenes = new bool[25]; // locationx replaced by inventoryScenes on db
    int[] levels = new int[25];
    int[] Perks = new int[3];
    string s;
    private int id;
    public bool koalaCondition;
    public bool oncePet3;
    private bool oncePet32;
    private double healthPet3;
    private void Start()
    {
        
        equipped = new bool[petSkins.Length];
        currentLevelText.text = "";
        currentLevel.SetActive(false);
        foreach (var p in petSkins) { p.SetActive(false); }
        hasTool = false;
        for(int i = 0; i < equipped.Length; i++) { equipped[i] = false; levels[i] = 0; inventoryScenes[i] = false; }
        GameAssets.Instance.CandyError.SetActive(false);
        inventoryScenes[0] = true;
    }
    public void FixedUpdate()
    {
        for (int i = 0; i < youHaves.Length; i++) { if (youHaves[i] == null) { break; } youHaves[i].SetActive(Player.getInventoryPinata()[i]); }
        UpdateForPets();
        currentLevel.SetActive(ImageScript.isPets);
        if (ImageScript.isPets)
        {
            if(ImageScript.currentPage == 0)
            {
                currentLevel.SetActive(false);
            }
            else if(ImageScript.currentPage == 1) {
                if(levels[0] == 5) { s = "(max)"; }
                else { s = ""; }
                currentLevelText.text = "This Pet's Current Level" + "\n" + levels[0] + s; }

            else if(ImageScript.currentPage == 2) 
            {
                if (levels[1] == 5)
                {
                    s = "(max)";
                } else { s = ""; }
                currentLevelText.text = "This Pet's Current Level" + "\n" + levels[1] + s;
            }
            else if(ImageScript.currentPage == 3) {
                if (levels[2] == 5)
                {
                    s = "(max)";
                }
                else { s = ""; }
                currentLevelText.text = "This Pet's Current Level" + "\n" + levels[2] + s;
            }
            else if(ImageScript.currentPage == 4)
            {
                if (levels[3] == 5)
                {
                    s = "(max)";
                }
                else { s = ""; }
                currentLevelText.text = "This Pet's Current Level" + "\n" + levels[3] + s;
            } 
            else if(ImageScript.currentPage == 5)
            {
                if(levels[4] == 5)
                {
                    s = "(max)";
                }
                else { s = ""; }
                currentLevelText.text = "This Pet's Current Level" + "\n" + levels[4] + s;
            }
            
            
            else if(ImageScript.currentPage == 5) { currentLevelText.text = ""; }
        }


        if (Player.getToolID() == -1) { foreach (var t in toolSkins) { t.SetActive(false); } startSkin.SetActive(true); }

        else
        {
            startSkin.SetActive(false);
            for (int i = 0; i < toolSkins.Length; i++) { if (i == Player.getToolID()) { toolSkins[i].SetActive(true); } else { toolSkins[i].SetActive(false); } }
        }
        


    }
    public void buysceneID1() 
    {
        if(Player.getBalance() < 5000 && !inventoryScenes[1]) { StartCoroutine(CandyError()); }
        else if (!inventoryScenes[1])
        {
            Player.withdraw(5000);
            AudioManager.PlaySound("bought");
            foreach (var s in scenes) { s.SetActive(false); }
            scenes[1].SetActive(true);
            inventoryScenes[1] = true;
        }
        else
        {
            foreach (var s in scenes) { s.SetActive(false); }
            scenes[1].SetActive(true);
            AudioManager.PlaySound("pop");
        }
    }
    public void buysceneID2()
    {
        if (Player.getBalance() < 5000 && !inventoryScenes[2]) { StartCoroutine(CandyError()); }
        else if (!inventoryScenes[2])
        {
            Player.withdraw(5000);
            AudioManager.PlaySound("bought");
            foreach (var s in scenes) { s.SetActive(false); }
            scenes[2].SetActive(true);
            inventoryScenes[2] = true;
        }
        else
        {
            foreach (var s in scenes) { s.SetActive(false); }
            scenes[2].SetActive(true);
            AudioManager.PlaySound("pop");
        }
    }
    public void buysceneID3()
    {
        if (Player.getBalance() < 5000 && !inventoryScenes[3]) { StartCoroutine(CandyError()); }
        else if (!inventoryScenes[3])
        {
            Player.withdraw(5000);
            AudioManager.PlaySound("bought");
            foreach (var s in scenes) { s.SetActive(false); }
            scenes[3].SetActive(true);
            inventoryScenes[3] = true;
        }
        else
        {
            foreach (var s in scenes) { s.SetActive(false); }
            scenes[3].SetActive(true);
            AudioManager.PlaySound("pop");
        }
    }
    public void buytoolID0()
    {
        if(Instance.Player.IsInventoryFull())
        {
            // Inventory Full Alert
            return;
        }
        if (Player.getBalance() < 90) { StartCoroutine(CandyError()); }
        else
        {
            Instance.Player.withdraw(90);
            AudioManager.PlaySound("bought");
            Tool toolToBuy = new Tool(0,1,75,1,1);
            Player.PurchaseTool(toolToBuy);
        }
    }
    public void buytoolID1()
    {
        if (Instance.Player.IsInventoryFull())
        {
            // Inventory Full Alert
            return;
        }
        if (Player.getBalance() < 110) { StartCoroutine(CandyError()); }
        else
        {
            AudioManager.PlaySound("bought");
            Tool toolToBuy = new Tool(1, 2, 175, 3, 0);
            Instance.Player.withdraw(110);
            Instance.Player.PurchaseTool(toolToBuy);
        }
    }
    public void buytoolID2()
    {
        if (Instance.Player.IsInventoryFull())
        {
            // Inventory Full Alert
            return;
        }
        if (Player.getBalance() < 275) { StartCoroutine(CandyError()); }
        else
        {
            Instance.Player.withdraw(275);
            AudioManager.PlaySound("bought");
            Tool toolToBuy = new Tool(2, 0, 80, -2, 3, 2);
            Instance.Player.PurchaseTool(toolToBuy);
        }
    }

    public void buytoolID3()
    {
        if (Instance.Player.IsInventoryFull())
        {
            // Inventory Full Alert
            return;
        }
        if (Player.getBalance() < 450) { StartCoroutine(CandyError()); }
        else
        {
            Instance.Player.withdraw(450);
            AudioManager.PlaySound("bought");
            Tool toolToBuy = new Tool(3, 4, 250, 3, 2, .8);
            Instance.Player.PurchaseTool(toolToBuy);
        }
    }
    public void buytoolID4()
    {
        if (Instance.Player.IsInventoryFull())
        {
            // Inventory Full Alert
            return;
        }
        if (Player.getBalance() < 525) { StartCoroutine(CandyError()); }
        else
        {
            Tool toolToBuy = new Tool(4, 1, 300, 2, 4, .7);
            Player.withdraw(525);
            AudioManager.PlaySound("bought");
            Instance.Player.PurchaseTool(toolToBuy);
        }
    }
    public void buytoolID5()
    {
        if (Instance.Player.IsInventoryFull())
        {
            // Inventory Full Alert
            return;
        }
        if (Player.getBalance() < 1000) { StartCoroutine(CandyError()); }
        else
        {
            id = 5;
            AudioManager.PlaySound("bought");
            Tool toolToBuy = new Tool(5, 9, 240, 0, 4, 1.5);
            Instance.Player.withdraw(1000);
            Instance.Player.PurchaseTool(toolToBuy);
        }
    }
    public void buytoolID6()
    {
        if (Instance.Player.IsInventoryFull())
        {
            // Inventory Full Alert
            return;
        }
        if (Player.getBalance() < 1100) { GameAssets.Instance.CandyError.SetActive(true); }
        else
        {
            Tool toolToBuy = new Tool(6, 19, 100, 2, 5, .8);
            AudioManager.PlaySound("bought");
            Player.withdraw(1100);
            Instance.Player.PurchaseTool(toolToBuy);
        }
    }
    public void buytoolID7()
    {
        if (Instance.Player.IsInventoryFull())
        {
            // Inventory Full Alert
            return;
        }
        if (Player.getBalance() < 1200) { GameAssets.Instance.CandyError.SetActive(true); }
        else
        {
            Tool toolToBuy = new Tool(7, 14, 125, 0, 5);
            Player.withdraw(1200);
            AudioManager.PlaySound("bought");
            Instance.Player.PurchaseTool(toolToBuy);
        }
    }
    public void buytoolID8()
    {
        if (Instance.Player.IsInventoryFull())
        {
            // Inventory Full Alert
            return;
        }
        if (Player.getBalance() < 1550) { StartCoroutine(CandyError()); }
        else
        {
            Tool toolToBuy = new Tool(8, 2, 320, -1, 7, 1.5);
            Player.withdraw(1550);
            AudioManager.PlaySound("bought");
            Player.PurchaseTool(toolToBuy);
        }
    }
    public void buytoolID9()
    {
        if (Instance.Player.IsInventoryFull())
        {
            // Inventory Full Alert
            return;
        }
        if (Player.getBalance() < 1800) { StartCoroutine(CandyError()); }
        else
        {
            Tool toolToBuy = new Tool(9, 25, 500, 5, 4, .5, 2.2f);
            Player.withdraw(1800);
            AudioManager.PlaySound("bought");
            Player.PurchaseTool(toolToBuy);
        }
    }
    public void buytoolID10()
    {
        if (Instance.Player.IsInventoryFull())
        {
            // Inventory Full Alert
            return;
        }
        if (Player.getBalance() < 2025) { StartCoroutine(CandyError()); }
        else
        {
            Tool toolToBuy = new Tool(10, 23, 96, 0, 7, 1.25);
            Player.withdraw(2025);
            AudioManager.PlaySound("bought");
            Player.PurchaseTool(toolToBuy);
        }
    }
    public void buytoolID11()
    {
        if (Instance.Player.IsInventoryFull())
        {
            // Inventory Full Alert
            return;
        }
        if (Player.getBalance() < 2450) { StartCoroutine(CandyError()); }
        else
        {
            Tool toolToBuy = new Tool(11, 1, 175, 0, 9, 1, 1.8f);
            Player.withdraw(2450);
            AudioManager.PlaySound("bought");
            Player.PurchaseTool(toolToBuy);
        }
    }
    public void buytoolID12()
    {
        if (Instance.Player.IsInventoryFull())
        {
            // Inventory Full Alert
            return;
        }
        if (Player.getBalance() < 2700) { StartCoroutine(CandyError()); }
        else
        {
            Tool toolToBuy = new Tool(12, 24, 204, 2, 4, 1, 1.4f);
            Player.withdraw(2450);
            AudioManager.PlaySound("bought");
            Player.PurchaseTool(toolToBuy);
        }
    }
    public void buytoolID13()
    {
        if (Instance.Player.IsInventoryFull())
        {
            // Inventory Full Alert
            return;
        }
        if (Player.getBalance() < 3000) { StartCoroutine(CandyError()); }
        else
        {
            Tool toolToBuy = new Tool(13, 0, 424, -3, 5, 1.5, 1f);
            Player.withdraw(3000);
            AudioManager.PlaySound("bought");
            Instance.Player.PurchaseTool(toolToBuy);
        }
    }
    public void buytoolID14()
    {
        if (Instance.Player.IsInventoryFull())
        {
            // Inventory Full Alert
            return;
        }
        if (Player.getBalance() < 3200) { StartCoroutine(CandyError()); }
        else
        {
            Tool toolToBuy = new Tool(14, 4, 240, -2, 10, 2, 1);
            Player.withdraw(3200);
            AudioManager.PlaySound("bought");
            Instance.Player.PurchaseTool(toolToBuy);
        }
    }
    public void buytoolID15()
    {
        if (Instance.Player.IsInventoryFull())
        {
            // Inventory Full Alert
            return;
        }
        if (Player.getBalance() < 3500) { StartCoroutine(CandyError()); }
        else
        {
            Tool toolToBuy = new Tool(15,29,200,0,6);
            Player.withdraw(3500);
            AudioManager.PlaySound("bought");
            Instance.Player.PurchaseTool(toolToBuy);
        }
    }
    public void buyPinataDefault()
    {
        AudioManager.PlaySound("pop");
        if (Player.getPinata() != null)
        {
            Pinata.buyDefault();
            if (Pinata.getHealth() > Player.getEnemyHealth())
            {
                Player.setHealth(Player.getEnemyHealth());
            }
            else
            {
                Player.setHealth(Pinata.getHealth());
            }
        }
        else
        {
            Pinata.buyDefault();
            Player.setHealth(Pinata.getHealth());
        }

        if (equipped[3]) { oncePet32 = true; }
    }
    public void buypinataID0()
    {

        if (Player.getBalance() < 700 && !(Player.getInventoryPinata()[0])) { StartCoroutine(CandyError()); }
        else
        {
            if (!Player.getInventoryPinata()[0]) {
                AudioManager.PlaySound("bought");
                Player.withdraw(700);
                youHaves[0].SetActive(true);
            }
            if (Player.getPinata() != null)
            {
                Pinata.buyID0();
                AudioManager.PlaySound("pop");
                if (Pinata.getHealth() > Player.getEnemyHealth())
                {
                    Player.setHealth(Player.getEnemyHealth());
                }
                else
                {
                    Player.setHealth(Pinata.getHealth());
                }
            }
            else
            {
                Pinata.buyID0();
                AudioManager.PlaySound("pop");
                Player.setHealth(Pinata.getHealth());
            }
        }

        if (equipped[3]) { oncePet32 = true; }
    }
    public void buypinataID1()
    {
        if (Player.getBalance() < 500 && !(Player.getInventoryPinata()[1])) { StartCoroutine(CandyError()); }
        else
        {
            if (!Player.getInventoryPinata()[1])
            {
                AudioManager.PlaySound("bought");
                Player.withdraw(500);
                youHaves[1].SetActive(true);
            }
            if (Player.getPinata() != null)
            {
                Pinata.buyID1();
                AudioManager.PlaySound("pop");
                if (Pinata.getHealth() > Player.getEnemyHealth())
                {
                    Player.setHealth(Player.getEnemyHealth());
                }
                else
                {
                    Player.setHealth(Pinata.getHealth());
                }
            }
            else
            {
                Pinata.buyID1();
                AudioManager.PlaySound("pop");
                Player.setHealth(Pinata.getHealth());
            }
        }

        if (equipped[3]) { oncePet32 = true; }
    }
    public void buypinataID2()
    {
        if (Player.getBalance() < 1500 && !(Player.getInventoryPinata()[2])) { StartCoroutine(CandyError()); }
        else
        {
            if (!Player.getInventoryPinata()[2])
            {
                AudioManager.PlaySound("bought");
                Player.withdraw(1500);
                youHaves[2].SetActive(true);
            }
            if (Player.getPinata() != null)
            {
                Pinata.buyID2();
                AudioManager.PlaySound("pop");
                if (Pinata.getHealth() > Player.getEnemyHealth())
                {
                    Player.setHealth(Player.getEnemyHealth());
                }
                else
                {
                    Player.setHealth(Pinata.getHealth());
                }
            }
            else
            {
                Pinata.buyID2();
                AudioManager.PlaySound("pop");
                Player.setHealth(Pinata.getHealth());
            }
        }

        if (equipped[3]) { oncePet32 = true; }
    }
    public void buypinataID3()
    {
        if (Player.getBalance() < 2200 && !(Player.getInventoryPinata()[3])) { StartCoroutine(CandyError()); }
        else
        {
            if (!Player.getInventoryPinata()[3])
            {
                Player.withdraw(2200);
                AudioManager.PlaySound("bought");
                youHaves[3].SetActive(true);
            }
            if (Player.getPinata() != null)
            {
                Pinata.buyID3();
                AudioManager.PlaySound("pop");
                if (Pinata.getHealth() > Player.getEnemyHealth())
                {
                    Player.setHealth(Player.getEnemyHealth());
                }
                else
                {
                    Player.setHealth(Pinata.getHealth());
                }
            }
            else
            {
                Pinata.buyID3();
                AudioManager.PlaySound("pop");
                Player.setHealth(Pinata.getHealth());
            }
        }

        if (equipped[3]) { oncePet32 = true; }
    }
    public void buypinataID4()
    {
        if (Player.getBalance() < 3000 && !(Player.getInventoryPinata()[4])) { StartCoroutine(CandyError()); }
        else
        {
            if (!Player.getInventoryPinata()[4])
            {
                AudioManager.PlaySound("bought");
                Player.withdraw(3000);
                youHaves[4].SetActive(true);
            }
            if (Player.getPinata() != null)
            {
                Pinata.buyID4(); AudioManager.PlaySound("pop");
                if (Pinata.getHealth() > Player.getEnemyHealth())
                {
                    Player.setHealth(Player.getEnemyHealth());
                }
                else
                {
                    Player.setHealth(Pinata.getHealth());
                }
            }
            else
            {
                Pinata.buyID4(); AudioManager.PlaySound("pop");
                Player.setHealth(Pinata.getHealth());
            }
        }

        if (equipped[3]) { oncePet32 = true; }
    }
    public void buypinataID5()
    {
        if (Player.getBalance() < 3600 && !(Player.getInventoryPinata()[5])) { StartCoroutine(CandyError()); }
        else
        {
            if (!Player.getInventoryPinata()[5])
            {
                AudioManager.PlaySound("bought");
                Player.withdraw(3600);
                youHaves[5].SetActive(true);
            }
            if (Player.getPinata() != null)
            {
                Pinata.buyID5(); AudioManager.PlaySound("pop");
                if (Pinata.getHealth() > Player.getEnemyHealth())
                {
                    Player.setHealth(Player.getEnemyHealth());
                }
                else
                {
                    Player.setHealth(Pinata.getHealth());
                }
            }
            else
            {
                Pinata.buyID5(); AudioManager.PlaySound("pop");
                Player.setHealth(Pinata.getHealth());
            }
        }

        if (equipped[3]) { oncePet32 = true; }
    }
    public void buypetID0()
    {
        if(Player.getBalance() < 675 && !Player.getInventoryPet()[0]) { StartCoroutine(CandyError()); }
        else
        {
            if (!Player.getInventoryPet()[0])
            {
                Player.withdraw(675);
                AudioManager.PlaySound("bought");
                levels[0] = 1;
                Player.getInventoryPet()[0] = true;
                petSkins[0].SetActive(true);
                // level up buttons setactive true
                Perks[0] = 20; //(timer) Gives 1 Candy Every 20(-0,-1,-3,-3,-5) seconds [looting counts!]
                Perks[1] = 15; //(timer) Restores 1 Energy Every 15(-0,-2,-3,-5) seconds
                Perks[2] = 10; //(constant) Increases your Max Energy by 10(+0,+5,+5,+10,+15)
            }
            for (int i = 0; i < equipped.Length; i++) { equipped[i] = false; }
            for (int i = 0; i < petSkins.Length; i++) { petSkins[i].SetActive(equipped[i]); }
            equipped[0] = true;
            petSkins[0].SetActive(equipped[0]);
            AudioManager.PlaySound("summon");



        }
    }
    public void buypetID1() 
    {
        if(Player.getBalance() < 1050 && !Player.getInventoryPet()[1]) { StartCoroutine(CandyError()); }
        else { if (!Player.getInventoryPet()[1])
            {
                Player.withdraw(1050);
                AudioManager.PlaySound("bought");
                levels[1] = 1;
                Player.getInventoryPet()[1] = true;
                petSkins[1].SetActive(true);
                Perks[0] = 10; //Reduces Pinata Respawn Time by 10%(+0,+3,+3,+4,+5)
                Perks[1] = 15; //Sets Candies per Hit to 0. Increases your final loot by %15(+0,+5,+5,+10,+15)
                Perks[2] = 15; //Consumes 1 Candy every 15(+0,+0,+0,+0,+15) seconds but increases your Attack Damage by your looting
            }

            for (int i = 0; i < equipped.Length; i++) { equipped[i] = false; }
            for (int i = 0; i < petSkins.Length; i++) { petSkins[i].SetActive(equipped[i]); }
            equipped[1] = true;
            petSkins[1].SetActive(equipped[1]);
            temporaryAttackDamage = Player.AttackDamage;
            AudioManager.PlaySound("summon");


        }
    }
    public void buyPetID2()
    {
        if (Player.getBalance() < 900 && !Player.getInventoryPet()[2]) { StartCoroutine(CandyError()); }
        else
        {
            if (!Player.getInventoryPet()[2])
            {
                Player.withdraw(900);
                AudioManager.PlaySound("bought");
                levels[2] = 1;
                Player.getInventoryPet()[2] = true;
                petSkins[2].SetActive(true);
                Perks[0] = 2; //Durability consume is doubled but energy consume is 1/2.
                Perks[1] = 50; //Increases your looting by %50(+0,+25,+25,+50,+50) of your attack speed.
                Perks[2] = 1; //You have 1%(+0,+1,+1,+1,+1) chance to get super loot on hit!
            }

            for (int i = 0; i < equipped.Length; i++) { equipped[i] = false; }
            for (int i = 0; i < petSkins.Length; i++) { petSkins[i].SetActive(equipped[i]); }
            equipped[2] = true;
            petSkins[2].SetActive(equipped[2]);
            AudioManager.PlaySound("summon");

        }
    }
    public void buyPetID3()
    {
        if (Player.getBalance() < 850 && !Player.getInventoryPet()[3]) { StartCoroutine(CandyError()); }
        else
        {
            if (!Player.getInventoryPet()[3])
            {
                AudioManager.PlaySound("bought");
                Player.withdraw(850);
                levels[3] = 1;
                Player.getInventoryPet()[3] = true;
                petSkins[3].SetActive(true);
                Perks[0] = 20; // Increases your pinata stats by %20(+0,+5,+10,+10,+15) except Final Loot. Decreases final loot by %50.
                Perks[1] = 300; // Increases pinata's loot range by 1 if respawn time is above 300(-0,-25,-25,-50,-50)
                Perks[2] = 1; // Decreases energy per hit by 1(+0,+0,+0.0,+0.0,+1)x your attack speed
            }
            oncePet32 = true;
            for (int i = 0; i < equipped.Length; i++) { equipped[i] = false; }
            for (int i = 0; i < petSkins.Length; i++) { petSkins[i].SetActive(equipped[i]); }
            equipped[3] = true;
            
            petSkins[3].SetActive(equipped[3]);

            AudioManager.PlaySound("summon");
        }
    }
    public void buyPetID4()
    {
        if (Player.getBalance() < 2000 && !Player.getInventoryPet()[4]) { StartCoroutine(CandyError()); }
        else
        {
            if (!Player.getInventoryPet()[4])
            {
                AudioManager.PlaySound("bought");
                Player.withdraw(2000);
                levels[4] = 1;
                Player.getInventoryPet()[4] = true;
                petSkins[4].SetActive(true);
                Perks[0] = 25; // Increases your attack damage by %25(+0,+10,+15,+25,+25)
                Perks[1] = 25; // If you can one shot the pinata you'll receive %25(+0,+15,+20,+20,+20) more candy
                Perks[2] = 2; // It has %10(+10 per level) chance not to consume durability. But It will cost 2(+2 per level) candy every single time that it doesnt.
                // Perks 2 formül ==> levels[4] x 10 (1. için) --- Perks[2] 2. için
            }
            for (int i = 0; i < equipped.Length; i++) { equipped[i] = false; }
            for (int i = 0; i < petSkins.Length; i++) { petSkins[i].SetActive(equipped[i]); }
            equipped[4] = true;

            petSkins[4].SetActive(equipped[4]);

            AudioManager.PlaySound("summon");
        }
    }
    public void levelUpPetID0() {
        if (Player.getInventoryPet()[0]) {
            if (levels[0] == 1)
            {
                if (Player.getBalance() < 675) { StartCoroutine(CandyError()); }
                else
                {
                    Player.withdraw(675);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 19;
                    Perks[1] = 13;
                    Perks[2] = 15;
                    levels[0] = 2;
                }
            }
            else if(levels[0] == 2) 
            {
                if (Player.getBalance() < 675) { StartCoroutine(CandyError()); }
                else
                {
                    Player.withdraw(675);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 16;
                    Perks[1] = 10;
                    Perks[2] = 20;
                    levels[0] = 3;
                }
            }
           else if(levels[0] == 3)
            {
                if (Player.getBalance() < 675) { StartCoroutine(CandyError()); }
                else
                {
                    Player.withdraw(675);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 13;
                    Perks[1] = 7;
                    Perks[2] = 30;
                    levels[0] = 4;
                }
            }
            else if (levels[0] == 4) 
            
            {
                if (Player.getBalance() < 675) { StartCoroutine(CandyError()); }
                else
                {
                    Player.withdraw(675);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 8;
                    Perks[1] = 3;
                    Perks[2] = 50;
                    levels[0] = 5;
                }
            
            }
            if(levels[0] == 5) 
            { 
                // buy buttons setactive(false)
                                 
            }
            
            
        }
    }
    public void levelUpPetID1()
    {
        if (Player.getInventoryPet()[1])
        {
            if (levels[1] == 1)
            {
                if (Player.getBalance() < 1050) { StartCoroutine(CandyError()); }
                else
                {
                    Player.withdraw(1050);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 13;
                    Perks[1] = 20;
                    Perks[2] = 15;
                    levels[1] = 2;
                }
            }
            else if (levels[1] == 2)
            {
                if (Player.getBalance() < 1050) { StartCoroutine(CandyError()); }

                else
                {
                    Player.withdraw(1050);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 16;
                    Perks[1] = 25;
                    Perks[2] = 15;
                    levels[1] = 3;
                }
            }
            else if (levels[1] == 3)
            {
                if (Player.getBalance() < 1050) { StartCoroutine(CandyError()); }
                else
                {
                    Player.withdraw(1050);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 20;
                    Perks[1] = 35;
                    Perks[2] = 15;
                    levels[1] = 4;
                }
            }
            else if (levels[1] == 4)

            {
                if (Player.getBalance() < 1050) { StartCoroutine(CandyError()); }

                else
                {
                    Player.withdraw(1050);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 25;
                    Perks[1] = 50;
                    Perks[2] = 30;
                    levels[1] = 5;
                }

            }
            if (levels[1] == 5)
            {
                // buy buttons setactive(false)

            }


        }
    }
    public void levelUpPetID2()
    {
        if (Player.getInventoryPet()[2])
        {
            if (levels[2] == 1)
            {
                if (Player.getBalance() < 900) { StartCoroutine(CandyError()); }
                else
                {
                    Player.withdraw(900);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 2;
                    Perks[1] = 75;
                    Perks[2] = 2;
                    levels[2] = 2;
                }
            }
            else if (levels[2] == 2)
            {
                if (Player.getBalance() < 900) { StartCoroutine(CandyError()); }

                else
                {
                    Player.withdraw(900);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 2;
                    Perks[1] = 100;
                    Perks[2] = 3;
                    levels[2] = 3;
                }
            }
            else if (levels[2] == 3)
            {
                if (Player.getBalance() < 900) { StartCoroutine(CandyError()); }
                else
                {
                    Player.withdraw(900);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 2;
                    Perks[1] = 150;
                    Perks[2] = 4;
                    levels[2] = 4;
                }
            }
            else if (levels[2] == 4)

            {
                if (Player.getBalance() < 900) { StartCoroutine(CandyError()); }

                else
                {
                    Player.withdraw(900);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 2;
                    Perks[1] = 200;
                    Perks[2] = 5;
                    levels[2] = 5;
                }

            }
            if (levels[2] == 5)
            {
                // buy buttons setactive(false)

            }
            
            }
        }
    public void levelUpPetID3() 
    {
        if (Player.getInventoryPet()[3])
        {
            if (levels[3] == 1)
            {
                if (Player.getBalance() < 850) { StartCoroutine(CandyError()); }
                else
                {
                    Player.withdraw(850);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 25;
                    Perks[1] = 275;
                    Perks[2] = 1;
                    levels[3] = 2;
                }
            }
            else if (levels[3] == 2)
            {
                if (Player.getBalance() < 850) { StartCoroutine(CandyError()); }

                else
                {
                    Player.withdraw(850);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 30;
                    Perks[1] = 250;
                    Perks[2] = 1;
                    levels[3] = 3;
                }
            }
            else if (levels[3] == 3)
            {
                if (Player.getBalance() < 850) { StartCoroutine(CandyError()); }
                else
                {
                    Player.withdraw(850);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 35;
                    Perks[1] = 150;
                    Perks[2] = 1;
                    levels[3] = 4;
                }
            }
            else if (levels[3] == 4)

            {
                if (Player.getBalance() < 850) { StartCoroutine(CandyError()); }

                else
                {
                    Player.withdraw(850);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 50;
                    Perks[1] = 100;
                    Perks[2] = 2;
                    levels[3] = 5;
                }

            }
            if (levels[3] == 5)
            {
                // buy buttons setactive(false)

            }

        }
    }
    public void levelUpPetID4()
    {
        if (Player.getInventoryPet()[4])
        {
            if (levels[4] == 1)
            {
                if (Player.getBalance() < 2000) { StartCoroutine(CandyError()); }
                else
                {
                    Player.withdraw(2000);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 35;
                    Perks[1] = 40;
                    Perks[2] = 4;
                    levels[4] = 2;
                }
            }
            else if (levels[4] == 2)
            {
                if (Player.getBalance() < 2000) { StartCoroutine(CandyError()); }

                else
                {
                    Player.withdraw(2000);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 50;
                    Perks[1] = 60;
                    Perks[2] = 6;
                    levels[4] = 3;
                }
            }
            else if (levels[4] == 3)
            {
                if (Player.getBalance() < 2000) { StartCoroutine(CandyError()); }
                else
                {
                    Player.withdraw(2000);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 75;
                    Perks[1] = 80;
                    Perks[2] = 8;
                    levels[4] = 4;
                }
            }
            else if (levels[4] == 4)

            {
                if (Player.getBalance() < 2000) { StartCoroutine(CandyError()); }

                else
                {
                    Player.withdraw(2000);
                    AudioManager.PlaySound("bought");
                    Perks[0] = 100;
                    Perks[1] = 100;
                    Perks[2] = 10;
                    levels[4] = 5;
                }

            }
            if (levels[4] == 5)
            {
                // buy buttons setactive(false)

            }

        }
    }
    public int getEPA()
    {
        return toolEPA;
    }
    public int getDurability()
    {
        return toolDurability;
    }
    public double getDamage()
    {
        return attackDamage;
    }
    public double getSpeed()
    {
        return attackSpeed;
    }
    public int getLooting()
    {
        return looting;
    }
    public float getRange()
    {
        return attackRange;
    }
    public int[] getPerks() { return Perks; }

    void UpdateForPets()
    {
        if (equipped[0]) 
        {
            if (!petSkins[0].activeInHierarchy)
            {
                petSkins[0].SetActive(true);
            }
            if (levels[0] == 1)
            {
                Perks[0] = 20;
                Perks[1] = 15;
                Perks[2] = 10;
            }
            else if (levels[0] == 2)
            {
                Perks[0] = 19;
                Perks[1] = 13;
                Perks[2] = 15;
            }
            else if (levels[0] == 3)
            {
                Perks[0] = 16;
                Perks[1] = 10;
                Perks[2] = 20;
            }
            else if (levels[0] == 4)
            {
                Perks[0] = 13;
                Perks[1] = 7;
                Perks[2] = 30;
            }
            else if (levels[0] == 5)
            {
                Perks[0] = 8;
                Perks[1] = 3;
                Perks[2] = 50;
            }
            if (Player.getWrapGod()) { timer0 += CustomTime.deltaTime * 2; }
            else
            {
                timer0 += CustomTime.deltaTime;
            }
                
            if (Player.getEnergy() <= Player.getMaxEnergy() && Player.getWrapGod())
            {
                timer1 += CustomTime.deltaTime * 2;
            }
            else if(Player.getEnergy() <= Player.getMaxEnergy()){ timer1 += CustomTime.deltaTime; }
                if (timer0 > Perks[0]) { Player.addToBalance(); if (Player.PlayerTool.Looting > 3) { Player.addToBalance(); }
                if (Player.PlayerTool.Looting > 6) { Player.addToBalance(); }
                if (Player.PlayerTool.Looting > 9) { Player.addToBalance(); }
                timer0 = 0; }
            if(timer1 > Perks[1] && Player.getEnergy() <= Player.getMaxEnergy()) { Player.addToEnergy(); timer1 -= Perks[1]; }
            if (Player.getWrapGod()) { Player.setMaxEnergy(Player.getInitialMaxEnergy() + (2 * Perks[2])); }
            else { Player.setMaxEnergy(Player.getInitialMaxEnergy() + (2 * Perks[2])); }
        }
        else
        {
            if (Player.getInventoryPet()[0])
            {
                timer0 = 0;
                timer1 = 0;
                Player.setMaxEnergy(Player.getInitialMaxEnergy());
            }
            
        }
        if (equipped[1])
        {
            if (!petSkins[1].activeInHierarchy)
            {
                petSkins[1].SetActive(true);
            }
                if (levels[1] == 1)
                {
                    Perks[1] = 15;
                if (Player.getRheaGod())
                {
                    Perks[0] = (int)(10 * (double)Player.AttackSpeed);
                }
                else
                {
                    Perks[0] = 10;
                }
                    Perks[2] = 15;
                }
                else if (levels[1] == 2)
                {
                if (Player.getRheaGod())
                {
                    Perks[0] = (int)(13 * (double)Player.AttackSpeed);
                }
                else
                {
                    Perks[0] = 13;
                }
                Perks[1] = 20;
                    Perks[2] = 15;
                }
                else if (levels[1] == 3)
                {
                if (Player.getRheaGod())
                {
                    Perks[0] = (int)(16 * (double)Player.AttackSpeed);
                }
                else
                {
                    Perks[0] = 16;
                }
                Perks[1] = 25;
                    Perks[2] = 15;
                }
                else if (levels[1] == 4)
                {
                if (Player.getRheaGod())
                {
                    Perks[0] = (int)(20 * (double)Player.AttackSpeed);
                }
                else
                {
                    Perks[0] = 20;
                }
                Perks[1] = 35;
                    Perks[2] = 15;
                }
                else if (levels[1] == 5)
                {
                    
                if (Player.getRheaGod())
                {
                    Perks[0] = (int)(20 * (double)Player.AttackDamage);
                }
                else
                {
                    Perks[0] = 25;
                }
                    Perks[1] = 50;
                    Perks[2] = 30;
                }
            Pinata.setRespawnTime(Pinata.getTempRespawnTime() - (Pinata.getTempRespawnTime() * ((float)Perks[0] / 100)));
            Pinata.setLootFromPerk((int)((Pinata.getLoot() + (Pinata.getLoot() * Player.PlayerTool.Looting / 10))  * ((double)Perks[1] / 100)));
            timer2 += CustomTime.deltaTime;
            temporaryAttackDamage = Player.AttackDamage + Player.PlayerTool.Looting;
            if (timer2 > Perks[2]) { Player.removeFromBalance(); timer2 = 0; }

        }
        else
        {
            Pinata.setLootFromPerk(0);
            if (Player.getInventoryPet()[1])
            {
                Pinata.setRespawnTime(Pinata.getTempRespawnTime());
            }

        }
        if (equipped[2])
        {
            if (!petSkins[2].activeInHierarchy)
            {
                petSkins[2].SetActive(true);
            }
            Perks[0] = 2;
            if (levels[2] == 1)
            {
                Perks[1] = 50; 
                Perks[2] = 1; 
            }
            else if (levels[2] == 2)
            {
                Perks[1] = 75;
                Perks[2] = 2;
            }
            else if (levels[2] == 3)
            {
                Perks[1] = 100;
                Perks[2] = 3;
            }
            else if (levels[2] == 4)
            {
                Perks[1] = 150;
                Perks[2] = 4;
            }
            else if (levels[2] == 5)
            {
                Perks[1] = 200;
                Perks[2] = 5;
            }
            if (hasTool) { tempEPA = (int)((double)Player.getEPA() / Perks[0]); }
            else { tempEPA = Player.getEPA(); }
            int a = 0;
            if(tempLooting == 10) { a = 1; }
            int[] numbers = new int[Perks[2]+a];
            Debug.Log(numbers.Length);
                jackpot = false;
                for (int i = 0; i < numbers.Length; i++) { numbers[i] = UnityEngine.Random.Range(0, 101); }
                foreach(int i in numbers) { if(i == 27) { jackpot = true; break; } }
            
            tempLooting = Player.PlayerTool.Looting + (int)(Player.AttackSpeed * ((double)Perks[1]) / 100);
            if (tempLooting >= Player.getLootingCap()) { tempLooting = Player.getLootingCap(); }

        }
        else
        {
            if (Player.getInventoryPet()[2])
            {
            

            }
        }
        if (equipped[3])
        {
            if (!petSkins[3].activeInHierarchy)
            {
                petSkins[3].SetActive(true);
            }
            oncePet3 = true;

            if (levels[3] == 1)
            {
                Perks[0] = 20;
                Perks[1] = 300;
                Perks[2] = 1;
            }
            else if (levels[3] == 2)
            {
                Perks[0] = 25;
                Perks[1] = 275;
                Perks[2] = 1;
            }
            else if (levels[3] == 3)
            {
                Perks[0] = 30;
                Perks[1] = 250;
                Perks[2] = 1;
            }
            else if (levels[3] == 4)
            {
                Perks[0] = 35;
                Perks[1] = 200;
                Perks[2] = 1;
            }
            else if (levels[3] == 5)
            {
                Perks[0] = 50;
                Perks[1] = 150;
                Perks[2] = 2;
            }
            if (Pinata.getRespawnTime() > Perks[1]) { koalaCondition = true; }
            else { koalaCondition = false; }
            tempEPA = Player.getEPA() - (int)(Player.AttackSpeed * Perks[2]);
            if(tempEPA <= 3 && Player.getToolID() != 13) { tempEPA = 3; }
            else if(Player.getToolID() == 13) { tempEPA = 2; }
            if(oncePet32) { Pinata.buyPet3Perk(); oncePet32 = false; }
        }
        else
        {
            if (Player.getInventoryPet()[3] && oncePet3)
            {
                oncePet3 = false;
                healthPet3 = Player.getEnemyHealth();
                callPinataThis();
                if (healthPet3 > Pinata.getHealth())
                {
                    Player.setHealth(healthPet3);
                }

            }
        }
        if (equipped[4])
        {
            if (!petSkins[4].activeInHierarchy)
            {
                petSkins[4].SetActive(true);
            }

            if (levels[4] == 1)
            {
                Perks[0] = 25;
                Perks[1] = 25;
                Perks[2] = 2;
            }
            else if (levels[4] == 2)
            {
                Perks[0] = 35;
                Perks[1] = 40;
                Perks[2] = 4;
            }
            else if (levels[4] == 3)
            {
                Perks[0] = 50;
                Perks[1] = 60;
                Perks[2] = 6;
            }
            else if (levels[4] == 4)
            {
                Perks[0] = 75;
                Perks[1] = 80;
                Perks[2] = 8;
            }
            else if (levels[4] == 5)
            {
                Perks[0] = 100;
                Perks[1] = 100;
                Perks[2] = 10;
            }
            jackpot = false;
            int[] numbers = new int[(levels[4] * 10)];
            for(int i=0; i<numbers.Length; i++)
            {
                numbers[i] = UnityEngine.Random.Range(0,101);
                if(numbers[i] == 27) { jackpot = true; break; }
            }
            temporaryAttackDamage = (Player.AttackDamage) + (Player.AttackDamage * Perks[0] / 100);


        }
        else
        {
            if (Player.getInventoryPet()[4])
            {
             
            }
        }

    }
    public int getTempLooting() { return tempLooting; }
    public int[] getLevels()
    {
        return levels;
    }
    public void setLevels(int[] levels)
    {
        this.levels = levels;
    }
    public bool getJackpot() { return jackpot; }
    public int getTempEPA() { return tempEPA; }
    public int getToolDurability() { return toolDurability; }
    public double getTemporaryAttackDamage() { return temporaryAttackDamage; }
    public void setTemporaryAttackDamage(double temporaryAttackDamage) { this.temporaryAttackDamage = temporaryAttackDamage; }
    public int getID() { return id; }
    public bool[] getInventoryScenes() { return inventoryScenes; }
    public void callPinataThis()
    {
        if (Pinata.getThis() == 0)
        {
            buyPinataDefault();
        }
        else if (Pinata.getThis() == 1)
        {
            buypinataID0();
        }
        else if (Pinata.getThis() == 2)
        {
            buypinataID1();
        }
        else if (Pinata.getThis() == 3)
        {
            buypinataID2();
        }
        else if (Pinata.getThis() == 4)
        {
            buypinataID3();
        }
        else if(Pinata.getThis() == 5)
        {
            buypinataID4();
        }
    }
    public void callScenes(int i)
    {
        foreach(var t in scenes) { t.SetActive(false); }
        scenes[i].SetActive(true);
    }
    public void setInventoryScenes(bool[] s)
    {
        inventoryScenes = s;
    }
    
    private IEnumerator CandyError()
    {
        if (GameAssets.Instance.CandyError.activeInHierarchy)
            yield return new WaitForSeconds(0);

        GameAssets.Instance.CandyError.SetActive(true);
        yield return new WaitForSeconds(3);
        GameAssets.Instance.CandyError.SetActive(false);
    }
}
