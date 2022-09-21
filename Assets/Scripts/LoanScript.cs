using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoanScript : MonoBehaviour
{
    [SerializeField] AttackingPinata player;
    private int loanCount = 0;
    [SerializeField] GameObject[] loanButton;
    [SerializeField] public GameObject loanB;
    [SerializeField] GameObject loanC;
    [SerializeField] Text debt;
    [SerializeField] GameObject candyimage;
    private static double moneyToPay = 0;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        loanC.SetActive(false);
        loanB.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        debt.text = "CURRENT DEBT: " + (int)moneyToPay;
        if (moneyToPay > 0)
        {
            timer += Time.deltaTime;
            if(timer > 60) { timer = 0; }
        }
        if (loanCount < 2)
        {
            foreach (var p in loanButton) { p.SetActive(true); }
        }
        else
        {
            foreach (var p in loanButton) { p.SetActive(false); }
        }
        if(moneyToPay > 0 && timer > 5)
        {
            if (player.getBalance() > 0)
            {
                player.removeFromBalance();
                moneyToPay--;
            }
            else { moneyToPay++; }
            timer = 0;
        }
        if(loanCount == 2 && moneyToPay == 0)
        {
            loanB.SetActive(false);
        }
    }
    public void ImageAc()
    {
        candyimage.SetActive(false);
        loanC.SetActive(true);
 
    }
    public void GoBack()
    {
        candyimage.SetActive(true);
        loanC.SetActive(false);

    }
    public void getLoan0() 
    {
        player.Loan(1);
        moneyToPay += 600;
        loanCount++;
    }
    public void getLoan1()
    {
        loanCount++;

        player.Loan(2);
        moneyToPay += 1300;
    }
    public void getLoan2()
    {
        loanCount++;

        player.Loan(3);
        moneyToPay += 2500;
    }
    public int getLoanCount() { return loanCount; }
    public double getMoneyToPay() { return moneyToPay; }
    public void setLoanCount(int loanC) { loanCount = loanC;  }
    public void setMoneyToPay(double money) { moneyToPay = money; }
    public void payLoan()
    {
        if (moneyToPay < player.getBalance() && moneyToPay > 0)
        {

            player.removeFromBalance(moneyToPay);
            
            moneyToPay = 0;
        }
        else
        {
            Debug.Log("Not enough candy");
        }
    }

}
