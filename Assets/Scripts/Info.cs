using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    [SerializeField] GameObject image;
    [SerializeField] GameObject button;
    private string position;
    bool isActive;
    bool isPressed = false;
    [SerializeField] Text WholeText;
    [SerializeField] GameObject lowDurability;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayFab.PlayFabClientAPI.IsClientLoggedIn()) { button.SetActive(true); }

    }
    private void Awake()
    {
        image.SetActive(false);
        button.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Instance.Player.openShop) { button.SetActive(false); image.SetActive(false); lowDurability.SetActive(false); }
        else { if (isPressed) { button.SetActive(true);  }  }
        UpdateStats();

        if (Instance.Player.getToolDurability() < 25 && !isActive && Instance.Player.getShop().hasTool) { 
            if (!Instance.Player.openShop) { lowDurability.SetActive(true); } }
        else { lowDurability.SetActive(false); }
    }

    public void InfoButtonClick()
    {
        if (!isActive) { image.SetActive(true); isActive = true; }
        else { image.SetActive(false); isActive = false; }
    }
    public void activateButton()
    {
        isPressed = true;
        button.SetActive(true);
    }

    public void UpdateStats()
    {
        ResetStats();
        UpdateAttackDamage();
        UpdateAttackSpeed();
        UpdateLooting();
        UpdateMaxEnergy();
        UpdateEPA();
        UpdateToolDurability();
        UpdatePinataHealth();
        UpdateLoots();
        UpdateRank();
    }
    private void ResetStats()
    {
        WholeText.text = "";
    }
    private void UpdateRank()
    {
        int p = PlayfabManager.getLeaderboardPosition();
        if (p == 0) 
        { 
            position = "Unranked"; 
        } else
        {
            position = p.ToString();
        }

        WholeText.text += "\n" + "Your Rank: " + position;
    }

    private void UpdateAttackDamage()
    {
        WholeText.text += "\n" + "Attack Damage: ";
        if (Instance.Player.getShop().equipped[4])
        {
            WholeText.text += Instance.Shop.getTemporaryAttackDamage();
        }
        else
        {
            WholeText.text += Instance.Player.getAttackDamage();
        }
    }

    private void UpdateAttackSpeed()
    {
        WholeText.text += "\n" + "Attack Speed: " + (Instance.Player.getAttackSpeed() + Instance.Player.getExtraAs());
    }

    private void UpdateLooting()
    {
        WholeText.text += "\n" + "Looting: ";

        if (Instance.Player.getShop().equipped[2])
        {
            WholeText.text += Instance.Shop.getTempLooting();
        }
        else
        {
            WholeText.text += Instance.Player.getLooting();
        }
    }
    private void UpdateMaxEnergy()
    {
        WholeText.text += "\n" + "Max Energy: " + Instance.Player.getMaxEnergy();
    }

    private void UpdateEPA()
    {
        WholeText.text += "\n" + "Energy Consume per Hit: ";
        if(Instance.Shop.equipped[3] || Instance.Shop.equipped[2])
        {
            WholeText.text += Instance.Shop.getTempEPA();
        }
        else
        {
            WholeText.text += Instance.Shop.getEPA();
        }
    }

    private void UpdateToolDurability()
    {
        WholeText.text += "\n" + "Tool Durability: ";
        if (Instance.Player.toolDurability != 0)
            WholeText.text += Instance.Player.toolDurability;
        else
            WholeText.text += "None";
    }

    private void UpdatePinataHealth()
    {
        WholeText.text += "\n" + "Pinata's Health: " + Instance.Player.getEnemyHealth();
    }

    private void UpdateLoots()
    {
        double input;
        if (Instance.Shop.equipped[2])
        {
            input = Instance.Shop.getTempLooting() / 10 * Instance.Player.getLootEfficiency();
        }
        else
        {
            input = Instance.Player.getLooting() / 10 * Instance.Player.getLootEfficiency();
        }


        UpdateLootPerHit(input * 2);
        UpdateFinalLoot(input);
    }

    private void UpdateLootPerHit(double input)
    {
        WholeText.text += "\n" + "Candies per Hit: ";
        if (Instance.Shop.equipped[3] && Instance.Shop.koalaCondition)
        {
            WholeText.text += (int)((Pinata.GetLootRange(0) + 1) + ((Pinata.GetLootRange(0) + 1) * input)) 
                + "-" + 
                (int)(Pinata.GetLootRange(1) + (Pinata.GetLootRange(1) * input));
        }
        else if (Instance.Shop.equipped[1])
        {
            WholeText.text += 0;
        }
        else
        {
            WholeText.text += (int)((Pinata.GetLootRange(0)) + (Pinata.GetLootRange(0) * input))
                + "-" + 
                (int)((Pinata.GetLootRange(1) - 1) + ((Pinata.GetLootRange(1) - 1) * input));
        }
    }
    private void UpdateFinalLoot(double input)
    {
        WholeText.text += "\n" + "Candies per Pinata: ";
        if(Instance.Shop.equipped[4] && Instance.Player.getAttackDamage() >= Pinata.getHealth())
        {
            int i = (int)(Pinata.getLoot() + (Pinata.getLoot() * input));
            WholeText.text += (i * 2) + "(" + i + ")";
        }
        else if (Instance.Shop.equipped[4])
        {
            int i = (int)(Pinata.getLoot() + (Pinata.getLoot() * input));
            WholeText.text += i + "(" + (i*2) + ")";
        }
        else if (Instance.Shop.equipped[1])
        {
            WholeText.text += (int)(Pinata.getLoot() + (Pinata.getLoot() * input) + (int)Pinata.getLootFromPerk());
        }
        else if (Instance.Shop.equipped[3]) 
        {
            WholeText.text += (int)(Pinata.getLoot() + (Pinata.getLoot() * input)) / 2;
        }
        else
        {
            WholeText.text += (int)(Pinata.getLoot() + (Pinata.getLoot() * input));
        }
    }
}
