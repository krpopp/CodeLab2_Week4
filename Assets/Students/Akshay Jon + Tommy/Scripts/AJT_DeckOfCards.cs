using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_DeckOfCards : DeckOfCards
{
	//refs to avoid duplicate card shuffling
	[SerializeField] GameObject dealerHandGO, playerHandGO;
	AJT_DealerHand dealerHand;
	AJT_BlackJackHand playerHand;

	//initialize a shuffled deck of cards, removing duplicates of cards in hands
	protected override void AddCardsToDeck()
    {
		//Get refs to hands
		dealerHand = dealerHandGO.GetComponent<AJT_DealerHand>();
		playerHand = playerHandGO.GetComponent<AJT_BlackJackHand>();

		//Create a new list of cards to skip from the player and dealer's hands
		List<Card> cardsToSkip = new List<Card>(dealerHand.Hand.Count + playerHand.Hand.Count);
		cardsToSkip.AddRange(dealerHand.Hand);
		cardsToSkip.AddRange(playerHand.Hand);

		//loop 4 times through the 52 combinations of card number and suit
		for (int i = 0; i < 4; i++) {
			foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))){
				foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))){
					//BUGFIX BUGFIX; skip duplicate cards in reshuffle
					//add card if it isn't already in a hand
					if (cardsToSkip.Find(c => c.cardNum == type && c.suit == suit) == null)
						deck.Add(new Card(type, suit));
					//else remove that card from cardsToSkip
					else
						cardsToSkip.Remove(cardsToSkip.Find(c => c.cardNum == type && c.suit == suit));
				}
			}
		}
    }

	//returns and removes the next card from the deck
	public override Card DrawCard(){			
		Card nextCard = deck.Next();
		if (deck.Count > 21) {
			deck.Remove(nextCard);
		} else {
			deck = new ShuffleBag<Card>();
			AddCardsToDeck();
		}
		

		return nextCard;
	}
}
