using UnityEngine;

public class NMBlackJackHand : BlackJackHand
{
    
    public NMDealerHand dealerHand;
    protected override void ShowValue()
    {
        handVals = GetHandValue();
			
        total.text = "Player: " + handVals;

        if(handVals > 21)
        {
            GameObject.Find("Game Manager").GetComponent<BlackJackManager>().PlayerBusted();
            //Bug1: called when player bust(dealer show hands value)
            dealerHand.RevealCardWhenPlayerBust();
        }
        //Bug2&6: call blackjack when player hit 21 or natural Blackjack
        else if (handVals == 21)
        {
            GameObject.Find("Game Manager").GetComponent<BlackJackManager>().BlackJack();
        }
    }
    
    public void ReSetUpHand()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            Destroy(handBase.transform.GetChild(i).gameObject);
        }
        hand.Clear();
        SetupHand();
    }
    
}
