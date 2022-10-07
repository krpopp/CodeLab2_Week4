using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Enhanced card that allows you to subtract its value from your total
public class AJT_NegativeCard : AJT_Card {

    public AJT_NegativeCard(Type cardNum, Suit suit) : base(cardNum, suit)
    {       //constructor to declare a variable of type Card
        this.cardNum = cardNum;
        this.suit = suit;
    }

    public override void TriggerEnhancedCard()
    {
        base.TriggerEnhancedCard();

        manager.ShowActionButtonOne(this, "NEG");
    }

    public override int GetCardEnhancedValue()
    {
        usingValue = true;
        int neg = -GetCardValue();
        usingValue = false;
        return neg;
    }

    public override void ActionOne()
    {
        base.ActionOne();
        GameObject cardObj = playerHand.handBase.transform.GetChild(playerHand.handBase.transform.childCount - 1).gameObject;
        cardObj.GetComponentInChildren<Text>().text = GetCardEnhancedValue().ToString();
        cardObj.GetComponentInChildren<Text>().color = Color.red;
    }


}
