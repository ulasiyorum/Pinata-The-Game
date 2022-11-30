using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

#nullable enable
public class Tool
{
    public int ToolID { get; set; }
    public int AttackDamage { get; set; }
    public double AttackSpeed { get; set; }
    public int Durability { get; set; }
    public int EPA { get; set; }
    public int Looting { get; set; }

    public float AttackRange { get; set; }
    

    public Tool()
    {
        ToolID = -1;
        AttackDamage = 0;
        Durability = 0;
        EPA = 0;
        Looting = 0;
        AttackSpeed = 1;
        AttackRange = 1.2f;

    }
    public Tool(int id,int attackDamage, int durability, int EPA, int looting, double attackSpeed = 1, float attackRange=1.2f)
    {
        this.ToolID = id;
        this.AttackDamage = attackDamage;
        this.Durability = durability;
        this.EPA = EPA;
        this.Looting = looting;
        this.AttackSpeed = attackSpeed;
        this.AttackRange = attackRange;
    }
}
