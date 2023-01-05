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
    public int Edition { get; set; }
    public float AttackRange { get; set; }


    public static int EditionCounter { get; set; }

    public Tool()
    {
        ToolID = -1;
        AttackDamage = 0;
        Durability = 0;
        EPA = 0;
        Looting = 0;
        AttackSpeed = 1;
        AttackRange = 1.2f;
        Edition = 0;
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
        Edition = EditionCounter;
        EditionCounter++;
    }

    public Tool(int id, int attackDamage, int edition, int durability, int EPA, int looting, double attackSpeed = 1, float attackRange = 1.2f)
    {
        this.ToolID = id;
        this.AttackDamage = attackDamage;
        this.Durability = durability;
        this.EPA = EPA;
        this.Looting = looting;
        this.AttackSpeed = attackSpeed;
        this.AttackRange = attackRange;
        Edition = edition;
    }

    public bool IsSame(Tool tool)
    {
        if (tool.Edition == Edition)
            return true;

        return false;
    }

    public static Tool Parse(string obj)
    {
        string[] elements = obj.Split(',');
        int id = int.Parse(elements[0]);
        int ad = int.Parse(elements[1]);
        double ats = double.Parse(elements[2]);
        int dur = int.Parse(elements[3]);
        int epa = int.Parse(elements[4]);
        int looting = int.Parse(elements[5]);
        int edition = int.Parse(elements[6]);
        float range = float.Parse(elements[7]);

        return new Tool(id, ad, edition, dur, looting, ats, range);
    } 

    public override string ToString()
    {
        return ToolID + "," + AttackDamage + "," + AttackSpeed + "," + Durability + "," + EPA + "," + Looting + "," + Edition + "," + AttackRange;
    }
}
