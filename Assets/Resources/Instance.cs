using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Instance : MonoBehaviour
{
    private static Instance instance;

    public static Instance i
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<Instance>();

            return instance;
        }
    }

    [SerializeField] Canvas canvas;
    public Canvas GetCanvas
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
    [SerializeField] Pinata pinataObject;
    public Pinata PinataObject
    {
        get
        {
            if (pinataObject == null)
            {
                pinataObject = FindObjectOfType<Pinata>(true);
            }

            return pinataObject;
        }
        private set
        {
            pinataObject = value;
        }
    }

    [SerializeField] Player playerInstance;
    public Player Player
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
    public Shopping Shop
    {
        get
        {
            return playerInstance.getShop();
        }
    }
}
