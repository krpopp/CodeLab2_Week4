using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDL_BlackJackHand : BlackJackHand
{
    //Bug 5: No BlackJack when player or dealer hits 21. - FIXED

    
    protected override void ShowValue()
    { 
        base.ShowValue();
        if (handVals == 21)
        {
            GameObject.Find("Game Manager").GetComponent<CDL_BlackJackManager>().BlackJack();
        }
    }

    public int CDL_GetHandValue()
    {
        CDL_BlackJackManager manager = GameObject.Find("Game Manager").GetComponent<CDL_BlackJackManager>();

        return manager.GetHandValue(hand);
    }
}
