using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CreateRandomChance
{
    public static bool Gamble(double chance, int range)
    {
        double random = Random.Range(0,range+1);
        return chance > random;
    }
}