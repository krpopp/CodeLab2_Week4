using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class JLZDeckOfCards : DeckOfCards{

	//Check the deck is empty or not
	protected virtual bool IsValidDeck(){
		return deck != null; 
	}

	//Add all kinds of cards to the deck
	protected override void AddCardsToDeck(){
		foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))){
			foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))){
				deck.Add(new Card(type, suit));
			}
		}
	}
	

	//Draw a card
	public virtual Card DrawCard(){
		Card nextCard = deck.Next();

		return nextCard;
	}
}
