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
        dealerCard = dealerHand.Hand[dealerHand.Hand.Count - 1] as AJT_Card;

        blackJackManager.DestroyCard(playerHand.handBase.transform.GetChild(playerHand.handBase.transform.childCount - 1).gameObject);
        playerHand.Hand.RemoveAt(playerHand.Hand.Count - 1);
        playerHand.Hand.Add(dealerCard);
        //playerHand.Hand[playerHand.Hand.Count - 1] = dealerCard;
        GameObject newCard1 = blackJackManager.CreateCard();
        //Debug.Log(playerHand.Hand.Count);
        //Debug.Log(playerHand.Hand[playerHand.Hand.Count - 1].suit);
        //Debug.Log(playerHand.Hand[playerHand.Hand.Count - 1] != null);
        playerHand.ShowCard(playerHand.Hand[playerHand.Hand.Count - 1], newCard1, 0);                
        playerHand.GetValue();      
        
        dealerHand.Hand.RemoveAt(dealerHand.Hand.Count - 1);
        blackJackManager.DestroyCard(dealerHand.handBase.transform.GetChild(dealerHand.handBase.transform.childCount - 1).gameObject);
        GameObject newCard2 = blackJackManager.CreateCard();
        dealerHand.Hand.Add(this);
        dealerHand.ShowCard(this, newCard2, 0);
        dealerHand.GetValue();


        dealerCard = null;
        usingValue = true;
    }
}
