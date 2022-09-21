using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootingCap : MonoBehaviour
{
    [SerializeField] AttackingPinata player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "" + player.getLootingCap();
    }
}
