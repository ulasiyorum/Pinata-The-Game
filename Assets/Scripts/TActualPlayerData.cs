using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TActualPlayerData {
    public double balance;

    public TActualPlayerData(AttackingPinata player) 
    { 
    this.balance = player.getBalance();
    
    }
    public TActualPlayerData LoadGame()
    {
        return this;
    }
}
