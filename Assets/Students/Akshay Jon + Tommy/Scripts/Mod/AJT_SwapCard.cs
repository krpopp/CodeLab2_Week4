using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enhanced card that allows you to swap it with the dealer's most recent card
public class AJT_SwapCard : AJT_Card {
    public AJT_SwapCard(Type cardNum, Suit suit) : base(cardNum, suit)
    {       //constructor to declare a variable of type Card
        this.cardNum = cardNum;
        this.suit = suit;
    }

    //called from BlackJackHand when a card is dealt into the hand
    public override void TriggerEnhancedCard()
    {
        base.TriggerEnhancedCard();

        manager.ShowActionButtonOne(this, "SWAP");
    }

    //called from the action button if the player chooses the enhanced option
    public override void ActionOne()
    {
        AJT_Card dealerCard = dealerHand.Hand[dealerHand.Hand.Count - 1] as AJT_Card;
      
        //swap cards in hand lists
        playerHand.Hand[playerHand.Hand.Count - 1] = dealerHand.Hand[dealerHand.Hand.Count - 1];
        dealerHand.Hand[dealerHand.Hand.Count - 1] = this;

        //swap card visuals
        playerHand.ShowCard(playerHand.Hand[playerHand.Hand.Count - 1], 
                            playerHand.handBase.transform.GetChild(playerHand.handBase.transform.childCount - 1).gameObject, 
                            0);                           
        dealerHand.ShowCard(dealerHand.Hand[dealerHand.Hand.Count - 1],
                            dealerHand.handBase.transform.GetChild(dealerHand.handBase.transform.childCount - 1).gameObject,
                            0);
        //Display new totals
        playerHand.GetValue();
        dealerHand.GetValue();

        usingValue = true;
    }
}
