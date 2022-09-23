using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DeckOfCards : MonoBehaviour {
	
	public Text cardNumUI;
	public Image cardImageUI;
	public Sprite[] cardSuits;

	public class Card{

		//custom type for suit
		public enum Suit {
			SPADES, 	//0
			CLUBS,		//1
			DIAMONDS,	//2
			HEARTS	 	//3
		};

		//custom type for type
		public enum Type {
			TWO		= 2,
			THREE	= 3,
			FOUR	= 4,
			FIVE  	= 5,
			SIX		= 6,
			SEVEN	= 7,
			EIGHT	= 8,
			NINE 	= 9,
			TEN		= 10,
			J		    = 11,
			Q		    = 12,
			K	    	= 13,
			A	    	= 14
		};

		public Type cardNum; //card number
		
		public Suit suit;//card suit

		//card class, takes card number and card suit
		public Card(Type cardNum, Suit suit){
			this.cardNum = cardNum;
			this.suit = suit;
		}

		//converting to string to declare card
		public override string ToString(){
			return "The " + cardNum + " of " + suit;
		}

		//?? not sure why only getting High Value, also Ace is worth 11 or 1
		public int GetCardHighValue(){
			int val;

			switch(cardNum){
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

	public static ShuffleBag<Card> deck;

	// Use this for initialization
	void Awake () {

		//if it is not a valid deck, create new shuffle bag
		if(!IsValidDeck()){
			deck = new ShuffleBag<Card>();

			AddCardsToDeck();
		}

		Debug.Log("Cards in Deck: " + deck.Count);
	}

	//checking if it a valid deck, if deck is not null, set to true
	protected virtual bool IsValidDeck(){
		return deck != null; 
	}

	//adding cards to deck, for each card suit and card type, add one to the list
	protected virtual void AddCardsToDeck(){
		foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))){
			foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))){
				deck.Add(new Card(type, suit));
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	//draws the next card in card deck
	public virtual Card DrawCard(){
		Card nextCard = deck.Next();

		return nextCard;
	}

	//gets the hash code number from the cardNum enum
	//?? maybe doesn't work for the Ace (being 11 or 1)
	public string GetNumberString(Card card){
		if(card.cardNum.GetHashCode() <= 10){
			return card.cardNum.GetHashCode() + "";
		} else {
			return card.cardNum + "";
		}
	}
		
	//gets the suit in number form
	public Sprite GetSuitSprite(Card card){
		return cardSuits[card.suit.GetHashCode()];
	}
}
