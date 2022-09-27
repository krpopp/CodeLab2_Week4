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
            dealerHand.RevealCardWhenPlayerBust();
        }
        else if (handVals == 21)
        {
            GameObject.Find("Game Manager").GetComponent<BlackJackManager>().BlackJack();
        }
    }
}
