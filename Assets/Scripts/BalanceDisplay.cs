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
        coinsText.text = " " + Instance.i.Player.getCoins();
        coinsShop.text = " " + Instance.i.Player.getCoins();
        balanceText.text = " " + Instance.i.Player.getBalance();
        balanceShop.text = " " + Instance.i.Player.getBalance();
        energyText.text = " " + Instance.i.Player.getEnergy();
        nextEnergyIn.text = " " + Instance.i.Player.getTimer();
    }

    // Update is called once per frame
    void Update()
    {
        float nextTimer = Pinata.getRespawnTime() - Instance.i.Player.getRespawnTimer();
        int timeLeft = Instance.i.Player.getMaxEnergy() - Instance.i.Player.getEnergy();
        if(timeLeft == 0)
        {
            nextEnergyIn.text = " Full!";
        }
        else
        {
            if(Instance.i.Shop.equipped[0])
            {
                if ((int)(Instance.i.Player.getShop().getPerks()[1]
                        - Instance.i.Player.getShop().timer1)
                        <= (11 - Instance.i.Player.getTimer()))
                {
                    nextEnergyIn.text = "Next Energy: " + (int)(1 + Instance.i.Shop.getPerks()[1]
                            - Instance.i.Player.getShop().timer1);
                }
                else
                {
                    nextEnergyIn.text = "Next Energy: " + (int)(11 - Instance.i.Player.getTimer());
                }
                
            }
            else
            {
                nextEnergyIn.text = "Next Energy: " + (int)(11 - Instance.i.Player.getTimer());
            }
            
        }
        if (Instance.i.Player.getRespawnTimer() > 0 && Instance.i.Player.getIsDead())
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
        coinsText.text = " " + Instance.i.Player.getCoins();
        coinsShop.text = " " + Instance.i.Player.getCoins();
        energyText.text = "" + Instance.i.Player.getEnergy(); 
        balanceText.text = "" + Instance.i.Player.getBalance();
        balanceShop.text = " " + Instance.i.Player.getBalance();
    }
}
