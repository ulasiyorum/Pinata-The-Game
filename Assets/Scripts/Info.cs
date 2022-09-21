using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    [SerializeField] AttackingPinata player;
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

    if(PlayfabManager.getLeaderboardPosition() == 0) { position = "Unranked"; }
    else { position = "" + PlayfabManager.getLeaderboardPosition(); }
        WholeText.text = "Attack Damage: " + player.getAttackDamage() + "\n" + "Attack Speed: " + (player.getAttackSpeed() + player.getExtraAs()) +
            "\n" + "Looting: " + player.getLooting() + "\n" + "Max Energy: " +
            player.getMaxEnergy() + "\n" + "Energy Consume per Hit: " + player.getEPA() + "\n" + "Tool Durability: " + player.getToolDurability()
            + "\n" + "Pinata Health: " + player.getEnemyHealth() + "\n" + "Candies per Hit: " +
            (player.getNewPinata().GetComponent<Pinata>().getLootRange0() +
            ((player.getNewPinata().GetComponent<Pinata>().getLootRange0() * player.GetComponent<AttackingPinata>().getLooting()) / 5) * player.getEfficiency()) + " - "
            + ((player.getNewPinata().GetComponent<Pinata>().getLootRange1() - 1) +
            (((player.getNewPinata().GetComponent<Pinata>().getLootRange1() - 1) * player.GetComponent<AttackingPinata>().getLooting()) / 5) * player.getEfficiency()) + "\n" + "Candies per Pinata: " +
            (player.getNewPinata().GetComponent<Pinata>().getLoot2()
            + (((player.getNewPinata().GetComponent<Pinata>().getLoot2() * player.GetComponent<AttackingPinata>().getLooting()) * player.getEfficiency()) / 10))
        +"\n" + "Your Rank: #" + position;
        if(player.getShop().equipped[1])
        {
            WholeText.text = "Attack Damage: " + player.getAttackDamage() + " + " + player.getLooting() +"\n" + "Attack Speed: " + (player.getAttackSpeed() + player.getExtraAs()) + "\n" + "Looting: " + player.getLooting() + "\n" + "Max Energy: " +
             player.getMaxEnergy() + "\n" + "Energy Consume per Hit: " + player.getEPA() + "\n" + "Tool Durability: " + player.getToolDurability()
             + "\n" + "Pinata Health: " + player.getEnemyHealth() + "\n" + "Candies per Hit: " +
             0 + "\n" + "Candies per Pinata: " +
             (player.getNewPinata().GetComponent<Pinata>().getLoot2()
             + (((player.getNewPinata().GetComponent<Pinata>().getLoot2() * player.GetComponent<AttackingPinata>().getLooting())  / 10) * player.getEfficiency())) + " + " + (int)Pinata.getLootFromPerk()
            +"\n" + "Your Rank: #" + position;
        }
        else if (player.getShop().equipped[2])
        {
            WholeText.text = "Attack Damage: " + player.getAttackDamage() + "\n" + "Attack Speed: " + (player.getAttackSpeed() + player.getExtraAs()) +
            "\n" + "Looting: " + player.getShop().getTempLooting() + "\n" + "Max Energy: " +
            player.getMaxEnergy() + "\n" + "Energy Consume per Hit: " + player.getShop().getTempEPA() + "\n" + "Tool Durability: " + player.getToolDurability()
            + "\n" + "Pinata Health: " + player.getEnemyHealth() + "\n" + "Candies per Hit: " +
            (player.getNewPinata().GetComponent<Pinata>().getLootRange0() +
            ((player.getNewPinata().GetComponent<Pinata>().getLootRange0() * player.getShop().getTempLooting()) / 5) * player.getEfficiency()) + " - "
            + ((player.getNewPinata().GetComponent<Pinata>().getLootRange1() - 1) +
            (((player.getNewPinata().GetComponent<Pinata>().getLootRange1() - 1) * player.getShop().getTempLooting()) / 5) * player.getEfficiency()) + "\n" + "Candies per Pinata: " +
            (player.getNewPinata().GetComponent<Pinata>().getLoot2()
            + (((player.getNewPinata().GetComponent<Pinata>().getLoot2() * player.getShop().getTempLooting()) / 10)) * player.getEfficiency()) + "\n" + "Your Rank: #" + position;
        }
        else if (player.GetComponent<AttackingPinata>().getShop().equipped[3] && player.GetComponent<AttackingPinata>().getShop().koalaCondition)
        {
            WholeText.text = "Attack Damage: " + player.getAttackDamage() + "\n" + "Attack Speed: " + (player.getAttackSpeed() + player.getExtraAs()) +
            "\n" + "Looting: " + player.getLooting() + "\n" + "Max Energy: " +
            player.getMaxEnergy() + "\n" + "Energy Consume per Hit: " + player.getShop().getTempEPA() + "\n" + "Tool Durability: " + player.getToolDurability()
            + "\n" + "Pinata Health: " + player.getEnemyHealth() + "\n" + "Candies per Hit: " +
            ((player.getNewPinata().GetComponent<Pinata>().getLootRange0() + 1) +
            (((player.getNewPinata().GetComponent<Pinata>().getLootRange0() + 1) * player.GetComponent<AttackingPinata>().getLooting()) / 5) *player.getEfficiency())+ " - "
            + ((player.getNewPinata().GetComponent<Pinata>().getLootRange1()) +
            ((player.getNewPinata().GetComponent<Pinata>().getLootRange1() * player.GetComponent<AttackingPinata>().getLooting()) / 5) * player.getEfficiency())+ "\n" + "Candies per Pinata: " +
            (player.getNewPinata().GetComponent<Pinata>().getLoot2()
            + (((player.getNewPinata().GetComponent<Pinata>().getLoot2() * player.GetComponent<AttackingPinata>().getLooting()) / 10)) *player.getEfficiency())+ "\n" + "Your Rank: #" + position;
        }
        else if (player.GetComponent<AttackingPinata>().getShop().equipped[3])
        {
            WholeText.text = "Attack Damage: " + player.getAttackDamage() + "\n" + "Attack Speed: " + (player.getAttackSpeed() + player.getExtraAs())+
            "\n" + "Looting: " + player.getLooting() + "\n" + "Max Energy: " +
            player.getMaxEnergy() + "\n" + "Energy Consume per Hit: " + player.getShop().getTempEPA() + "\n" + "Tool Durability: " + player.getToolDurability()
            + "\n" + "Pinata Health: " + player.getEnemyHealth() + "\n" + "Candies per Hit: " +
            ((player.getNewPinata().GetComponent<Pinata>().getLootRange0()) +
            (((player.getNewPinata().GetComponent<Pinata>().getLootRange0()) * player.GetComponent<AttackingPinata>().getLooting()) / 5) *player.getEfficiency())+ " - "
            + ((player.getNewPinata().GetComponent<Pinata>().getLootRange1() - 1) +
            (((player.getNewPinata().GetComponent<Pinata>().getLootRange1() - 1) * player.GetComponent<AttackingPinata>().getLooting()) / 5) * player.getEfficiency())+ "\n" + "Candies per Pinata: " +
            (player.getNewPinata().GetComponent<Pinata>().getLoot2()
            + ((player.getNewPinata().GetComponent<Pinata>().getLoot2() * player.GetComponent<AttackingPinata>().getLooting()) / 10) * player.getEfficiency()) + "\n" + "Your Rank: #" + position;

        }
        else if (player.GetComponent<AttackingPinata>().getShop().equipped[4])
        {
            WholeText.text = "Attack Damage: " + player.getShop().getTemporaryAttackDamage() + "\n" + "Attack Speed: " + (player.getAttackSpeed() + player.getExtraAs()) +
            "\n" + "Looting: " + player.getLooting() + "\n" + "Max Energy: " +
            player.getMaxEnergy() + "\n" + "Energy Consume per Hit: " + player.getEPA() + "\n" + "Tool Durability: " + player.getToolDurability()
            + "\n" + "Pinata Health: " + player.getEnemyHealth() + "\n" + "Candies per Hit: " +
            (player.getNewPinata().GetComponent<Pinata>().getLootRange0() +
            ((player.getNewPinata().GetComponent<Pinata>().getLootRange0() * player.GetComponent<AttackingPinata>().getLooting()) / 5) * player.getEfficiency()) + " - "
            + ((player.getNewPinata().GetComponent<Pinata>().getLootRange1() - 1) +
            (((player.getNewPinata().GetComponent<Pinata>().getLootRange1() - 1) * player.GetComponent<AttackingPinata>().getLooting()) / 5) *player.getEfficiency())+ "\n" + "Candies per Pinata: " +
            (player.getNewPinata().GetComponent<Pinata>().getLoot2()
            +(((player.getNewPinata().GetComponent<Pinata>().getLoot2() * player.GetComponent<AttackingPinata>().getLooting()) / 10) * player.getEfficiency()) + ((player.getNewPinata().GetComponent<Pinata>().getLoot2()
            + (((player.getNewPinata().GetComponent<Pinata>().getLoot2() * player.GetComponent<AttackingPinata>().getLooting()) / 10) *player.getEfficiency())* (player.getShop().getPerks()[1] / 100)))) + " / (" +
            (player.getNewPinata().GetComponent<Pinata>().getLoot2()
            + ((player.getNewPinata().GetComponent<Pinata>().getLoot2() * player.GetComponent<AttackingPinata>().getLooting()) / 10) * player.getEfficiency()) + ")"
        + "\n" + "Your Rank: #" + position;
        }
    }
}
