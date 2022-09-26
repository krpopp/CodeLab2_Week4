using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaoBlackJackHand : BlackJackHand
{
    protected override void ShowValue()
    {
        base.ShowValue();

        if( handVals==21 && hand.Count == 2)//natural black jack
        {
            GameObject.Find("Game Manager").GetComponent<BlackJackManager>().BlackJack();
        }

    }
}
