using UnityEngine;

public class NMBlackJackHand : BlackJackHand
{
    
    public NMDealerHand dealerHand;
    protected override void ShowValue()
    {
        handVals = GetHandValue(); //get player hand value
			
        total.text = "Player: " + handVals; //player hand value text display

        if(handVals > 21) //if player busts, hand exceeds 21
        {
            GameObject.Find("Game Manager").GetComponent<BlackJackManager>().PlayerBusted();
            //Bug1: called when player bust(dealer show hands value)
            dealerHand.RevealCardWhenPlayerBust();
        }
        //Bug2&6: call blackjack when player hit 21 or natural Blackjack
        else if (handVals == 21)
        {
            GameObject.Find("Game Manager").GetComponent<BlackJackManager>().BlackJack(); //call GM
        }
    }
    
    public void ReSetUpHand()
    {
        for (int i = 0; i < hand.Count; i++) //check all card obj in hand
        {
            Destroy(handBase.transform.GetChild(i).gameObject); //destroy cards in hand
        }
        hand.Clear(); //clear hand
        SetupHand(); //reset hand
    }
}
