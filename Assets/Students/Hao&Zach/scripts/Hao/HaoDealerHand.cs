using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaoDealerHand : DealerHand
{
    protected override bool DealStay(int handVal)
    {
        BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();
        return handVal >= playerHand.handVals;
    }

}
