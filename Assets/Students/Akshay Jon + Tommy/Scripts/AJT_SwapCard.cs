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
        choosing = false;


        playerHand.Hand.Remove(playerHand.Hand[playerHand.Hand.Count - 1]);
        playerHand.Hand.Add(dealerCard);
        playerHand.GetValue();

        dealerHand.Hand[dealerHand.Hand.Count - 1] = this;
        dealerHand.GetValue();

        dealerHand.ShowCard(dealerHand.Hand[dealerHand.Hand.Count -1], dealerHand.handBase.transform.GetChild(1).gameObject, 0);
        playerHand.ShowCard(playerHand.Hand[playerHand.Hand.Count - 1], playerHand.cardObj, 0);

        usingValue = true;
    }
}
