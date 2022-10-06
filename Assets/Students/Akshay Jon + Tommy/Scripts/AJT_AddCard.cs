using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_AddCard : AJT_Card
{

    public AJT_AddCard(Type cardNum, Suit suit) : base(cardNum, suit)
    {       //constructor to declare a variable of type Card
        this.cardNum = cardNum;
        this.suit = suit;
    }

    public override void TriggerEnhancedCard()
    {
        base.TriggerEnhancedCard();

        GameObject.Find("Game Manager").GetComponent<AJT_BlackJackManager>().ShowActionButtonOne(this, "GIVE");
    }

    public override void ActionOne()
    {
        choosing = false;

        playerHand.Hand.Remove(playerHand.Hand[playerHand.Hand.Count - 1]);
        blackJackManager.DestroyCard(playerHand.handBase.transform.GetChild(playerHand.handBase.transform.childCount - 1).gameObject);
        playerHand.GetValue();


        dealerHand.Hand.Add(this);
        dealerHand.GetValue();

        GameObject cardObj = blackJackManager.CreateCard();
        dealerHand.ShowCard(dealerHand.Hand[dealerHand.Hand.Count - 1], cardObj, 0);


        usingValue = true;
    }


}
