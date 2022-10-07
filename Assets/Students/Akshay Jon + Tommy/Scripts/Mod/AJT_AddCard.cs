using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enhanced card that allows you to give the dealer this card
public class AJT_AddCard : AJT_Card {

    //constructor to declare a variable of type AJT_Card
    public AJT_AddCard(Type cardNum, Suit suit) : base(cardNum, suit) {       
        this.cardNum = cardNum;
        this.suit = suit;
    }

    public override void TriggerEnhancedCard() {
        base.TriggerEnhancedCard();

        manager.ShowActionButtonOne(this, "GIVE");
    }

    //called from the action button if the player chooses the enhanced option
    public override void ActionOne() {
        //remove card from player hand
        playerHand.Hand.Remove(playerHand.Hand[playerHand.Hand.Count - 1]);
        manager.DestroyCard(playerHand.handBase.transform.GetChild(playerHand.handBase.transform.childCount - 1).gameObject);
        playerHand.GetValue();

        //add it to the dealer's hand
        dealerHand.Hand.Add(this);
        GameObject cardObj = manager.CreateCard();
        dealerHand.ShowCard(dealerHand.Hand[dealerHand.Hand.Count - 1], cardObj, 0);
        dealerHand.GetValue();
        
        //use default value
        usingValue = true;
    }
}
