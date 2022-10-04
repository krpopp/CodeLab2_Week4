using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZHaoBlackJackHand : BlackJackHand
{
    protected override void ShowValue()
    {
        base.ShowValue();

        //if there is a natural blackjack, call blackjack
        if (handVals == 21 && hand.Count == 2)
        {
            GameObject.Find("Game Manager").GetComponent<BlackJackManager>().BlackJack();
            
            //sets bool for player winning true, not sure if needed but trying out
            GameObject.Find("Game Manager").GetComponent<ZHaoBlackJackManager>().player21 = true;
        }

        if (handVals > 21)
        {
            GameObject.Find("Game Manager").GetComponent<BlackJackManager>().PlayerBusted();
            
            //sets bool for player busting true, not sure if needed but trying out
            GameObject.Find("Game Manager").GetComponent<ZHaoBlackJackManager>().playerBust = true;
        }
    }
}
