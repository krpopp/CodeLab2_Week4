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
        GameObject.Find("Game Manager").GetComponent<AJT_BlackJackManager>().ShowActionButtonOne(this, "SWAP");

        base.TriggerEnhancedCard();

    }

    public override int GetCardEnhancedValue()
    {
        return base.GetCardEnhancedValue();


    }

    public override void ActionOne()
    {
        choosing = false;

        AJT_BlackJackHand dealerHand = GameObject.Find("Dealer Hand").GetComponent<AJT_DealerHand>();
        AJT_BlackJackHand playerHand = GameObject.Find("Player Hand").GetComponent<AJT_BlackJackHand>();
        DeckOfCards.Card dealerCard = dealerHand.Hand[1];

        playerHand.Hand.Remove(this);
        playerHand.Hand.Add(dealerCard);
        playerHand.GetValue();

        dealerHand.Hand[1] = this;
        dealerHand.GetValue();

        dealerHand.ShowCard(dealerHand.Hand[1], dealerHand.handBase.transform.GetChild(1).gameObject, 0);
        playerHand.ShowCard(playerHand.Hand[playerHand.Hand.Count - 1], playerHand.cardObj, 0);

        usingValue = true;
    }
}
