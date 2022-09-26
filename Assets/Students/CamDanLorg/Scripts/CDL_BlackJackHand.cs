using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDL_BlackJackHand : BlackJackHand
{
    protected override void ShowValue()
    { 
        base.ShowValue();
        if (handVals == 21)
        {
            GameObject.Find("Game Manager").GetComponent<BlackJackManager>().BlackJack();
        }
    }
}
