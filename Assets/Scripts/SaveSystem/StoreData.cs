using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StoreData
{
    private static AttackingPinata player;

    public static AttackingPinata getPlayer() { return player; }
    public static void setPlayer(AttackingPinata a) { player = a; }
}
