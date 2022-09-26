using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDL_DealerHand : DealerHand
{
    
    //Bug 3: If the dealer has higher card value than the player, the dealer still hits. 
    protected override bool DealStay(int handVal)
    {
        BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();
        return handVal >= playerHand.handVals;
        /*if (handVal >= playerHand.handVals)
        {
            return true;
        } else return false;*/
    }
    
    //if the dealer has BlackJack. 
    protected override void ShowValue()
    { 
        base.ShowValue();
        if (handVals == 21)
        {
            GameObject.Find("Game Manager").GetComponent<BlackJackManager>().BlackJack();
        }
    }
}
