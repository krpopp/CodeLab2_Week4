using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_SwapCard : AJT_Card
{

    
    public AJT_SwapCard(Type cardNum, Suit suit) : base(cardNum, suit)
    {       //constructor to declare a variable of type Card
        this.cardNum = cardNum;
        this.suit = suit;
    }

    public override void TriggerEnhancedCard()
    {
        base.TriggerEnhancedCard();

        GameObject.Find("Game Manager").GetComponent<AJT_BlackJackManager>().ShowActionButtonOne(this, "SWAP");
    }

    public override int GetCardEnhancedValue()
    {
        return base.GetCardEnhancedValue();


    }

    public override void ActionOne()
    {
        AJT_BlackJackHand dealerHand = GameObject.Find("Dealer Hand").GetComponent<AJT_DealerHand>();
        AJT_BlackJackHand playerHand = GameObject.Find("Player Hand").GetComponent<AJT_BlackJackHand>();
        DeckOfCards.Card dealerCard = dealerHand.Hand[1];

        playerHand.Hand.Remove(this);
        playerHand.Hand.Add(dealerCard);
        playerHand.GetValue();

        dealerHand.Hand[1] = this;
        dealerHand.GetValue();
    }
}
