using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance : MonoBehaviour
{
    private static Canvas canvas;
    public static Canvas GetCanvas
    {
        get
        {
            if(canvas == null)
            {
                var canvases = FindObjectsOfType<Canvas>();
                foreach(Canvas c in canvases)
                {
                    if (c.name.ToLower() == "canvas")
                    {
                        canvas = c;
                        break;
                    }
                }
            }
            return canvas;
        }
    }

    // There'll always be one pinata
    private static Pinata pinataObject;
    public static Pinata PinataObject
    {
        get
        {
            if (pinataObject == null && FindObjectOfType<Pinata>() != null)
            {
                pinataObject = FindObjectOfType<Pinata>(true);
            }
            if (pinataObject != null)
                return pinataObject;

            return null;
        }
        private set
        {
            pinataObject = value;
        }
    }

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
    public static Shopping Shop
    {
        get
        {
            return playerInstance.getShop();
        }
    }
}
