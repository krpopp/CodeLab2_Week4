using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_BlackJackManager : BlackJackManager
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override int GetHandValue(List<DeckOfCards.Card> hand)
    {
        int handValue = 0;

        foreach (DeckOfCards.Card handCard in hand)
        {
            if (handValue >= 11)
            {
                handValue += handCard.GetCardHighValue();
            }
            else
            {
                handValue += handCard.GetCardLowValue();
            }
        }
        return handValue;
    }

}
