using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_DeckOfCards : DeckOfCards
{
	protected override void AddCardsToDeck()
    {
		for (int i = 0; i < 4; i++) {
			foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))){
				foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))){
					deck.Add(new Card(type, suit));
				}
			}
		}
    }

	
	public override Card DrawCard(){			//returns the next card from the deck
		Card nextCard = deck.Next();

		return nextCard;
	}
}
