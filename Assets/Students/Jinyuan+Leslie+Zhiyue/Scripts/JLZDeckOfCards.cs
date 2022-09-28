using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class JLZDeckOfCards : DeckOfCards{


	//Bug Fixed: Use 4 decks of cards instead of 1 deck
	protected override void AddCardsToDeck(){
		for(int i = 0; i < 4; i++)
        {
			foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit)))
			{
				foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type)))
				{
					deck.Add(new Card(type, suit));
				}
			}
		}
		
	}

	// BUG FIX: reshuffle deck when it has less than 20 cards
    protected override bool IsValidDeck()
    {
        return deck != null && deck.Count >= 20;
    }


    // Bug Fixed: Remove a card each time drawing a card
    public override Card DrawCard()
    {
		Card nextCard = deck.Next();

		deck.Remove(nextCard);

		Debug.Log("Cards in Deck: " + deck.Count);

		return nextCard;
	}
}
