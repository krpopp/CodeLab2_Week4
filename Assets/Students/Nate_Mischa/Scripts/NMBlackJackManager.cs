using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class NMBlackJackManager : BlackJackManager
{
    public override int GetHandValue(List<DeckOfCards.Card> hand)
    {
        int handValue = 0;
        bool weHaveAnAce = false;

        foreach(DeckOfCards.Card handCard in hand){
            handValue += handCard.GetCardHighValue();
            if (handCard.cardNum == DeckOfCards.Card.Type.A)
            {
                weHaveAnAce = true;
            }
        }

        if (weHaveAnAce)
        {
            if (handValue - 11 <= 10)
            {

            }
            else
            {
                handValue -= 11;
                handValue += 1;
            }
        }
        Debug.Log("new get hand value");
        return handValue;
    }
}
