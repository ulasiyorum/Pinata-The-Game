using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceDisplay : MonoBehaviour
{
    [SerializeField] Text balanceText;
    [SerializeField] Text balanceShop;
    //[SerializeField] Player Player;
    [SerializeField] Text energyText;
    [SerializeField] Text nextEnergyIn;
    [SerializeField] Text nextPinata;
    [SerializeField] Text coinsText;
    [SerializeField] Text coinsShop;
    [SerializeField] Text inShopNextPinata;
    // Start is called before the first frame update
    void Start()
    {
        coinsText.text = " " + Instance.Player.getCoins();
        coinsShop.text = " " + Instance.Player.getCoins();
        balanceText.text = " " + Instance.Player.getBalance();
        balanceShop.text = " " + Instance.Player.getBalance();
        energyText.text = " " + Instance.Player.getEnergy();
        nextEnergyIn.text = " " + Instance.Player.getTimer();
    }

    // Update is called once per frame
    void Update()
    {
        float nextTimer = Pinata.getRespawnTime() - Instance.Player.getRespawnTimer();
        int timeLeft = Instance.Player.getMaxEnergy() - Instance.Player.getEnergy();
        if(timeLeft == 0)
        {
            nextEnergyIn.text = " Full!";
        }
        else
        {
            if(Instance.Player.getShop().equipped[0])
            {
                if ((int)(Instance.Player.getShop().getPerks()[1]
                        - Instance.Player.getShop().timer1)
                        <= (11 - Instance.Player.getTimer()))
                {
                    nextEnergyIn.text = "Next Energy: " + (int)(1 + Instance.Player.getShop().getPerks()[1]
                            - Instance.Player.getShop().timer1);
                }
                else
                {
                    nextEnergyIn.text = "Next Energy: " + (int)(11 - Instance.Player.getTimer());
                }
                
            }
            else
            {
                nextEnergyIn.text = "Next Energy: " + (int)(11 - Instance.Player.getTimer());
            }
            
        }
        if (Instance.Player.getRespawnTimer() > 0 && Instance.Player.getIsDead())
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
        coinsText.text = " " + Instance.Player.getCoins();
        coinsShop.text = " " + Instance.Player.getCoins();
        energyText.text = "" + Instance.Player.getEnergy(); 
        balanceText.text = "" + Instance.Player.getBalance();
        balanceShop.text = " " + Instance.Player.getBalance();
    }
}
