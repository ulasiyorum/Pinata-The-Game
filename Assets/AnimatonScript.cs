using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatonScript : StateMachineBehaviour
{
    public bool Walking = false;

    public void Walk() { Walking = true; }
    public void stopWalking() { Walking = false; }
}
