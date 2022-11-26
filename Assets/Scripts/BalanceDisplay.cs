using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceDisplay : MonoBehaviour
{
    [SerializeField] Text balanceText;
    [SerializeField] Text balanceShop;
    [SerializeField] Player Player;
    [SerializeField] Text energyText;
    [SerializeField] Text nextEnergyIn;
    [SerializeField] Text nextPinata;
    [SerializeField] Text coinsText;
    [SerializeField] Text coinsShop;
    [SerializeField] Text inShopNextPinata;
    // Start is called before the first frame update
    void Start()
    {
        coinsText.text = " " + Player.getCoins();
        coinsShop.text = " " + Player.getCoins();
        balanceText.text = " " + Player.getBalance();
        balanceShop.text = " " + Player.getBalance();
        energyText.text = " " + Player.getEnergy();
        nextEnergyIn.text = " " + Player.getTimer();
    }

    // Update is called once per frame
    void Update()
    {
        float nextTimer = Pinata.getRespawnTime() - Player.getRespawnTimer();
        int timeLeft = Player.getMaxEnergy() - Player.getEnergy();
        if(timeLeft == 0)
        {
            nextEnergyIn.text = " Full!";
        }
        else
        {
            if(Player.getShop().equipped[0])
            {
                if ((int)(Player.getShop().getPerks()[1]
                        - Player.getShop().timer1)
                        <= (11 - Player.getTimer()))
                {
                    nextEnergyIn.text = "Next Energy: " + (int)(1 + Player.getShop().getPerks()[1]
                            - Player.getShop().timer1);
                }
                else
                {
                    nextEnergyIn.text = "Next Energy: " + (int)(11 - Player.getTimer());
                }
                
            }
            else
            {
                nextEnergyIn.text = "Next Energy: " + (int)(11 - Player.getTimer());
            }
            
        }
        if (Player.getRespawnTimer() > 0 && Player.getIsDead())
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
        coinsText.text = " " + Player.getCoins();
        coinsShop.text = " " + Player.getCoins();
        energyText.text = "" + Player.getEnergy(); 
        balanceText.text = "" + Player.getBalance();
        balanceShop.text = " " + Player.getBalance();
    }
}
