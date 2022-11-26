using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StoreData
{
    private static Player player;

    public static Player getPlayer() { return player; }
    public static void setPlayer(Player a) { player = a; }
}
