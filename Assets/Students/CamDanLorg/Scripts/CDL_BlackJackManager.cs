using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDL_BlackJackManager : BlackJackManager
{
    public override int GetHandValue(List<DeckOfCards.Card> hand) //we basically add a check that in case the handvalue is larger than 21 
    //we try to find the ace and subtract difference between 11 and 1 from the hand value
    {
        int handValue = 0;

        foreach(DeckOfCards.Card handCard in hand){
            handValue += handCard.GetCardHighValue();
        }
        if (handValue > 21) {
                foreach(DeckOfCards.Card handCard in hand) {
                    if (handCard.GetCardHighValue() == 11)
                    {
                        handValue -= 10;
                    }
                    if (handValue <= 21) {
                        break;
                    }
                }
        }
        return handValue;
    }
}
