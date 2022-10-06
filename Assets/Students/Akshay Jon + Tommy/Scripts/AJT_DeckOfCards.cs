using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_DeckOfCards : DeckOfCards {

    //for hands to instantiate when drawing cards
    public GameObject cardPrefab;

    //refs to avoid duplicate card shuffling
    [SerializeField] GameObject dealerHandGO, playerHandGO;
    AJT_DealerHand dealerHand;
    AJT_BlackJackHand playerHand;

    [SerializeField] int deckCount;

    private void Start() {
        //Get refs to hands
        dealerHand = dealerHandGO.GetComponent<AJT_DealerHand>();
        playerHand = playerHandGO.GetComponent<AJT_BlackJackHand>();

        AddCardsToDeck();
    }

    //initialize a shuffled deck of cards, removing duplicates of cards in hands
    protected override void AddCardsToDeck()
    {
        deck = new ShuffleBag<Card>();

        //Create a new list of cards to skip from the player and dealer's hands
        List<Card> cardsToSkip = new List<Card>();
        //All these checks are bc AddCardsToDeck is called on Awake not Start...
        if (dealerHand && playerHand) {
            if (dealerHand.Hand != null && playerHand.Hand != null) {
                if (dealerHand.Hand.Count > 0 || dealerHand.Hand.Count > 0) {
                    cardsToSkip = new List<Card>(dealerHand.Hand.Count + playerHand.Hand.Count);
        
                    cardsToSkip.AddRange(dealerHand.Hand);
                    cardsToSkip.AddRange(playerHand.Hand);
                }
            }
        }
        //BUG FIX
        //loop 4 times through the 52 combinations of card number and suit
        for (int i = 0; i < 1; i++) {
            foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))){
                foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))){
                    //BUGFIX BUGFIX; skip duplicate cards in reshuffle -- still troubleshooting
                    //remove card from cardsToSkip if it contains a card w/ suit and number
                    if (cardsToSkip.Find(c => c.cardNum == type && c.suit == suit) != null) {
                        cardsToSkip.Remove(cardsToSkip.Find(c => c.cardNum == type && c.suit == suit));
                        //Debug.Log("Removed the " + type + " of " + suit);
                    }
                    //add card if it isn't already in a hand
                    else {
                        //add enhanced cards based on number, default normal cards
                        switch ((int)type) {
                            case 2:
                                // deck.Add(new AJT_SubtractCard(type, suit));
                                break;
                            case 3:
                                deck.Add(new AJT_SwapCard(type, suit));
                                break;
                            case 4:
                                deck.Add(new AJT_AddCard(type, suit));
                                break;
                            case 5:
                                break;
                            default:
                                deck.Add(new Card(type, suit));
                                deckCount = deck.Count;
                                break;
                        }                                  
                    }
                }
            }
        }
    }

	//returns and removes the next card from the deck
	public override Card DrawCard() {
		var nextCard = deck.Next();
        

        //BUGFIX; remove the card from the shufflebag
        deck.Remove(nextCard);
        deckCount = deck.Count;
        //BUGFIX; shuffle the deck at 20 cards remaining
        if (deck.Count < 21) {
            AddCardsToDeck();
        }
        
		return nextCard;
	}
}
