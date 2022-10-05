using UnityEngine;
using UnityEngine.UI;

public class NMBlackJackHand : BlackJackHand
{
    public NMBlackJackHand hitMe;
    public NMDealerHand dealerHand;

    public Button SwapButton;

    bool hold = false;
    bool playerBust = false;
    bool blackJack = false;

    protected override void ShowValue()
    {
        handVals = GetHandValue(); //get player hand value
			
        total.text = "Player: " + handVals; //player hand value text display

        if(handVals > 21) //if player busts, hand exceeds 21
        {
            GameObject.Find("Game Manager").GetComponent<BlackJackManager>().PlayerBusted();
            //Bug1: called when player bust(dealer show hands value)
            dealerHand.RevealCardWhenPlayerBust();
            playerBust = true;
        }
        //Bug2&6: call blackjack when player hit 21 or natural Blackjack
        else if (handVals == 21)
        {
            GameObject.Find("Game Manager").GetComponent<BlackJackManager>().BlackJack(); //call GM
            blackJack = true;
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

    public void HitMe()
    {
        if (!hold)
        {
            DeckOfCards.Card card = deck.DrawCard(); //draw new card

            GameObject cardObj = Instantiate(Resources.Load("prefab/Card")) as GameObject; //instantiate visuals

            ShowCard(card, cardObj, hand.Count); //reveal card

            hand.Add(card); //add to hand

            ShowValue(); //update value

            SwapButton.interactable = true; //make swap button interactable
        }
        else if (playerBust == true || blackJack == true) //if bust or blackjack
        {
            SwapButton.interactable = false; //set to false
        }
    }
}
