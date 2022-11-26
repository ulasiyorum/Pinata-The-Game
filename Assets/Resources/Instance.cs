using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance : MonoBehaviour
{
    private static Player playerInstance;
    public static Player Player
    {
        get
        {
            if(playerInstance == null)
            {
                playerInstance = FindObjectOfType<Player>();
            }
            return playerInstance;
        }
    }
}
