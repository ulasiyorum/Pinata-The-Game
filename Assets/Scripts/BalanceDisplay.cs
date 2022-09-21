using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceDisplay : MonoBehaviour
{
    [SerializeField] Text balanceText;
    [SerializeField] Text balanceShop;
    [SerializeField] GameObject Player;
    [SerializeField] Text energyText;
    [SerializeField] Text nextEnergyIn;
    [SerializeField] Text nextPinata;
    [SerializeField] Text coinsText;
    [SerializeField] Text coinsShop;
    [SerializeField] Text inShopNextPinata;
    // Start is called before the first frame update
    void Start()
    {
        coinsText.text = " " + Player.GetComponent<AttackingPinata>().getCoins();
        coinsShop.text = " " + Player.GetComponent<AttackingPinata>().getCoins();
        balanceText.text = " " + Player.GetComponent<AttackingPinata>().getBalance();
        balanceShop.text = " " + Player.GetComponent<AttackingPinata>().getBalance();
        energyText.text = " " + Player.GetComponent<AttackingPinata>().getEnergy();
        nextEnergyIn.text = " " + Player.GetComponent <AttackingPinata>().getTimer();
    }

    // Update is called once per frame
    void Update()
    {
        float nextTimer = Pinata.getRespawnTime() - Player.GetComponent<AttackingPinata>().getRespawnTimer();
        int timeLeft = Player.GetComponent<AttackingPinata>().getMaxEnergy() - Player.GetComponent<AttackingPinata>().getEnergy();
        if(timeLeft == 0)
        {
            nextEnergyIn.text = " Full!";
        }
        else
        {
            if(Player.GetComponent<AttackingPinata>().getShop().equipped[0])
            {
                if ((int)(Player.GetComponent<AttackingPinata>().getShop().getPerks()[1]
                        - Player.GetComponent<AttackingPinata>().getShop().timer1)
                        <= (11 - Player.GetComponent<AttackingPinata>().getTimer()))
                {
                    nextEnergyIn.text = "Next Energy: " + (int)(1 + Player.GetComponent<AttackingPinata>().getShop().getPerks()[1]
                            - Player.GetComponent<AttackingPinata>().getShop().timer1);
                }
                else
                {
                    nextEnergyIn.text = "Next Energy: " + (int)(11 - Player.GetComponent<AttackingPinata>().getTimer());
                }
                
            }
            else
            {
                nextEnergyIn.text = "Next Energy: " + (int)(11 - Player.GetComponent<AttackingPinata>().getTimer());
            }
            
        }
        if (Player.GetComponent<AttackingPinata>().getRespawnTimer() > 0 && Player.GetComponent<AttackingPinata>().getIsDead())
        {
            nextPinata.fontSize = 40;
            nextPinata.text = "" + string.Format("{0:0.00}",nextTimer);
            inShopNextPinata.text = "Pinata respawn time: " + string.Format("{0:0.00}", nextTimer);

        }
        else
        {
            nextPinata.fontSize = 24;
            nextPinata.text = "Hit Pinata For Candies!";
            inShopNextPinata.text = "Pinata Respawned!";
        }
        coinsText.text = " " + Player.GetComponent<AttackingPinata>().getCoins();
        coinsShop.text = " " + Player.GetComponent<AttackingPinata>().getCoins();
        energyText.text = "" + Player.GetComponent<AttackingPinata>().getEnergy(); 
        balanceText.text = "" + Player.GetComponent<AttackingPinata>().getBalance();
        balanceShop.text = " " + Player.GetComponent<AttackingPinata>().getBalance();
    }
}
