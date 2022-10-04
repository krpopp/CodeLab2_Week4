using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZHaoDealerHand : DealerHand
{
    protected override bool DealStay(int handVal)
    {
        BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();
        return handVal >= playerHand.handVals;
    }

    protected override void ShowValue()
    {
        base.ShowValue();
        
        BlackJackManager manager = GameObject.Find("Game Manager").GetComponent<BlackJackManager>();

        if (handVals > 21)
        {
            manager.DealerBusted();
            
            //sets bool for dealer busting true, not sure if needed but trying out
            GameObject.Find("Game Manager").GetComponent<ZHaoBlackJackManager>().dealerBust = true;
        }
        else if (!DealStay(handVals))
        {
            Invoke("HitMe", 1);
        }
        else
        {
            BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();

            if(handVals < playerHand.handVals)
            {
                manager.PlayerWin();
                //also sets bool for dealer busting true, not sure if needed but trying out
                GameObject.Find("Game Manager").GetComponent<ZHaoBlackJackManager>().dealerBust = true;
            } 
            else
            {
                manager.PlayerLose();
                //sets bool for dealer winning true, not sure if needed but trying out
                GameObject.Find("Game Manager").GetComponent<ZHaoBlackJackManager>().dealer21 = true;
            }
        }
    }
}
