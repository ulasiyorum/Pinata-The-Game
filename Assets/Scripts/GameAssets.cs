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

    public Sprite empty;
    [SerializeField] Sprite[] pinataImages;
    [SerializeField] Animator pinataDied;
    [SerializeField] Animator pinataShake;
    [SerializeField] Animator playerHit;
    [SerializeField] Animator secretTreasure;
    [SerializeField] GameObject candyError;

    public Animator PinataDied { get => pinataDied; private set => pinataDied = value; }
    public Animator PinataShake { get => pinataShake; private set => pinataShake = value; }
    public Sprite[] PinataImages { get => pinataImages; private set => pinataImages = value; } 
    public Animator PlayerHit { get => playerHit; private set => playerHit = value; }
    public Animator SecretTreasure { get => secretTreasure; private set => secretTreasure = value; }
    public GameObject CandyError { get => candyError; }


}
