using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDL_BlackJackHand : BlackJackHand
{
    //Bug 5: No BlackJack when player or dealer hits 21. - FIXED

    
    //when hand value is 21, black jack
    protected override void ShowValue()
    { 
        base.ShowValue();
        if (handVals == 21)
        {
            GameObject.Find("Game Manager").GetComponent<CDL_BlackJackManager>().BlackJack();
        }
    }
}