using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_BlackJackManager : BlackJackManager
{
	//function to return the total from the cards in hand
	public int GetHandValue(List<AJT_DeckOfCards.Card> hand){
		int handValue = 0;

		foreach(AJT_DeckOfCards.Card handCard in hand){
			handValue += handCard.GetCardHighValue();
		}
		if (handValue > 21) {
			foreach(AJT_DeckOfCards.Card handCard in hand) {
				if (handCard.GetCardHighValue() == 1) handValue -= 10;
				if (handValue <= 21) break;
			}
		}
		return handValue;
	}

}
