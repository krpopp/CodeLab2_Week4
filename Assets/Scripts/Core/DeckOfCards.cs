using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DeckOfCards : MonoBehaviour {
	
	public Text cardNumUI;
	public Image cardImageUI;
	public Sprite[] cardSuits;

	public class Card{
		
		//enums to hold a suite
		public enum Suit {
			SPADES, 	//0
			CLUBS,		//1
			DIAMONDS,	//2
			HEARTS	 	//3
		};
		
		//enums to hold card face values
		public enum Type {
			TWO		= 2,
			THREE	= 3,
			FOUR	= 4,
			FIVE	= 5,
			SIX		= 6,
			SEVEN	= 7,
			EIGHT	= 8,
			NINE	= 9,
			TEN		= 10,
			J		= 11,
			Q		= 12,
			K		= 13,
			A		= 14
		};

		public Type cardNum;		//variable of type Type
		
		public Suit suit;			//variable of type Suit

		public Card(Type cardNum, Suit suit){		//constructor to declare a variable of type Card
			this.cardNum = cardNum;
			this.suit = suit;
		}

		public override string ToString(){				//overrides the ToString function
			return "The " + cardNum + " of " + suit;
		}

		//BUG; write GetCardLowValue
		public int GetCardHighValue(){					//Checks the face value on the card and in case of A,J,K or Q it assigns it a value
			int val;									//in case of it being a number it defaults to the number on it

			switch(cardNum){
			//BUG; ace should be 11 or 1
			case Type.A:
				val = 11;
				break;
			case Type.K:
			case Type.Q:
			case Type.J:
				val = 10;
				break;	
			default:
				val = (int)cardNum;
				break;
			}

			return val;
		}
	}

	public static ShuffleBag<Card> deck;			//creates a list of Cards called deck using the Shufflebag type

	// Use this for initialization
	//BUG; creates new deck every time scene loads, initialize deck every time its contents are less than 20 cards
	//BUG; deck is only made up of 52 cards, not 208
	void Awake () {

		if(!IsValidDeck()){							//if deck is invalid it reshuffles the deck
			deck = new ShuffleBag<Card>();			//default zero list

			AddCardsToDeck();						//initialises a full deck of 52 cards containing 4 suites with 13 unique cards for each suite ranging from A - K			
		}

		Debug.Log("Cards in Deck: " + deck.Count);	//debug for the count of cards in deck
	}

	protected virtual bool IsValidDeck(){			// function returns true if the object exists
		return deck != null; 
	}

	//BUG; only adds 52 cards
	protected virtual void AddCardsToDeck(){		//for each card of the type suit initialise cards ranging from A - K
		//for (int i = 0; i < 4; i++) {
		foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))){
			foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))){
				deck.Add(new Card(type, suit));
			}
		}
		//}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public virtual Card DrawCard(){			//returns the next card from the deck
		Card nextCard = deck.Next();

		return nextCard;
	}

	//returns the value associated with the enum index
	public string GetNumberString(Card card){
		if(card.cardNum.GetHashCode() <= 10){
			return card.cardNum.GetHashCode() + "";		
		} else {
			return card.cardNum + "";
		}
	}
		
	public Sprite GetSuitSprite(Card card){
		return cardSuits[card.suit.GetHashCode()];		//returns the suite sprite for the card
	}
}
