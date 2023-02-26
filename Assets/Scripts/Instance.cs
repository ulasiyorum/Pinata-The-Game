using System.Collections;
using System.Collections.Generic;
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

    public AttackingPinata Player;
}
