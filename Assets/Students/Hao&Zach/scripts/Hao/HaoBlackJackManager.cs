using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaoBlackJackManager : BlackJackManager
{
    public override int GetHandValue(List<DeckOfCards.Card> hand)
    {
        int handValue = 0;

        foreach (DeckOfCards.Card handCard in hand)
        {
            handValue += handCard.GetCardHighValue();

        }

        if (handValue > 21) //if busted, check if there are ace in hand
        {
                foreach (DeckOfCards.Card handCard in hand)
                {
                    if (handCard.cardNum == DeckOfCards.Card.Type.A)
                    {
                        handValue -= 10;
                             if (handValue < 21) { //if there is ace, hand balue minus 10 until the total is less than 21
                                break;
                              }
                    }
                }

            
        }

        return handValue;
    }

    public void TryAgainWithoutReloadingScene()
    {

    }



}
