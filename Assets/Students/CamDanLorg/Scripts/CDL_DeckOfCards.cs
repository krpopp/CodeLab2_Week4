using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CDL_DeckOfCards : DeckOfCards
{

    //Bug 1: Ace only has one value. (should 1 or 11). 
    //Bug 2: No BlackJack End state. - FIXED
    //Used in BlackJackManager.GethandValue & DealerHand.Showvalue.
    /*public int CDL_GetCardHighValue()
    {
        int val;

        return;
    }*/

}
