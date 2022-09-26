using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class JLZBlackJackManager : BlackJackManager
{

	public override int GetHandValue(List<DeckOfCards.Card> hand){
		// get the sum of card value
		int handValue = 0;
		foreach(DeckOfCards.Card handCard in hand){
			handValue += handCard.GetCardHighValue();
		}

        // BUG FIX (Ace): Ace can either be 1 or 11, whichever is more advantagous to the player
		if (handValue > 21)
		{
            foreach (DeckOfCards.Card handCard in hand)
            {
                if(handCard.GetCardHighValue() == 11)
				{
					handValue -= 10;
				}
            }
        }
        return handValue;
	}
}
