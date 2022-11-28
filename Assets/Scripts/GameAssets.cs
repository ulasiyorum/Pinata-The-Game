using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets assetsInstance;
    public static GameAssets Instance
    {
        get
        {
            if (assetsInstance == null)
                assetsInstance = FindObjectOfType<GameAssets>();

            return assetsInstance;
        }
    }


    [SerializeField] Sprite[] pinataImages;
    [SerializeField] Animator pinataDied;
    [SerializeField] Animator pinataShake;

    public Animator PinataDied { get => pinataDied; private set => pinataDied = value; }
    public Animator PinataShake { get => pinataShake; private set => pinataShake = value; }
    public Sprite[] PinataImages { get; private set; } 
}
