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

	//function to return the total from the cards in hand
	public override int GetHandValue(List<DeckOfCards.Card> hand){
		int handValue = 0;

		foreach(DeckOfCards.Card handCard in hand){
			handValue += handCard.GetCardHighValue();
		}
		if (handValue > 21) {
			foreach(DeckOfCards.Card handCard in hand) {
				if (handCard.GetCardLowValue() == 1) handValue -= 10;
				if (handValue <= 21) break;
			}
		}
		return handValue;
	}

}
