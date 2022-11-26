using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    [SerializeField] Player player;
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
        if(player.openShop) { button.SetActive(false); image.SetActive(false); lowDurability.SetActive(false); }
        else { if (isPressed) { button.SetActive(true);  }  }
        UpdateStats();

        if (player.getToolDurability() < 25 && !isActive && player.getShop().hasTool) { if (!player.openShop) { lowDurability.SetActive(true); } }
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
        if (player.getShop().equipped[4])
        {
            WholeText.text += player.getShop().getTemporaryAttackDamage();
        }
        else
        {
            WholeText.text += player.getAttackDamage();
        }
    }

    private void UpdateAttackSpeed()
    {
        WholeText.text += "\n" + "Attack Speed: " + (player.getAttackSpeed() + player.getExtraAs());
    }

    private void UpdateLooting()
    {
        WholeText.text += "\n" + "Looting: ";

        if (player.getShop().equipped[2])
        {
            WholeText.text += player.getShop().getTempLooting();
        }
        else
        {
            WholeText.text += player.getLooting();
        }
    }
    private void UpdateMaxEnergy()
    {
        WholeText.text += "\n" + "Max Energy: " + player.getMaxEnergy();
    }

    private void UpdateEPA()
    {
        WholeText.text += "\n" + "Energy Consume per Hit: ";
        if(player.getShop().equipped[3] || player.getShop().equipped[2])
        {
            WholeText.text += player.getShop().getTempEPA();
        }
        else
        {
            WholeText.text += player.getEPA();
        }
    }

    private void UpdateToolDurability()
    {
        WholeText.text += "\n" + "Tool Durability: ";
        if (player.toolDurability != 0)
            WholeText.text += player.toolDurability;
        else
            WholeText.text += "None";
    }

    private void UpdatePinataHealth()
    {
        WholeText.text += "\n" + "Pinata's Health: " + player.getEnemyHealth();
    }

    private void UpdateLoots()
    {
        double input;
        if (player.getShop().equipped[2])
        {
            input = player.getShop().getTempLooting() / 10 * player.getLootEfficiency();
        }
        else
        {
            input = player.getLooting() / 10 * player.getLootEfficiency();
        }


        UpdateLootPerHit(input * 2);
        UpdateFinalLoot(input);
    }

    private void UpdateLootPerHit(double input)
    {
        WholeText.text += "\n" + "Candies per Hit: ";
        if (player.getShop().equipped[3] && player.getShop().koalaCondition)
        {
            WholeText.text += (int)((Pinata.GetLootRange(0) + 1) + ((Pinata.GetLootRange(0) + 1) * input)) 
                + "-" + 
                (int)(Pinata.GetLootRange(1) + (Pinata.GetLootRange(1) * input));
        }
        else if (player.getShop().equipped[1])
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
        if(player.getShop().equipped[4] && player.getAttackDamage() >= Pinata.getHealth())
        {
            int i = (int)(Pinata.getLoot() + (Pinata.getLoot() * input));
            WholeText.text += (i * 2) + "(" + i + ")";
        }
        else if (player.getShop().equipped[4])
        {
            int i = (int)(Pinata.getLoot() + (Pinata.getLoot() * input));
            WholeText.text += i + "(" + (i*2) + ")";
        }
        else if (player.getShop().equipped[1])
        {
            WholeText.text += (int)(Pinata.getLoot() + (Pinata.getLoot() * input) + (int)Pinata.getLootFromPerk());
        }
        else if (player.getShop().equipped[3]) 
        {
            WholeText.text += (int)(Pinata.getLoot() + (Pinata.getLoot() * input)) / 2;
        }
        else
        {
            WholeText.text += (int)(Pinata.getLoot() + (Pinata.getLoot() * input));
        }
    }
}
