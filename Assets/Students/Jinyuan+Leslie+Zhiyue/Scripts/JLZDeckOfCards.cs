using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class JLZDeckOfCards : DeckOfCards{
	
	public Text cardNumUI;
	public Image cardImageUI;
	public Sprite[] cardSuits;

	//make a class of card
	public class Card{

		//Set the enum for 4 suits in cards
		public enum Suit {
			SPADES, 	//0
			CLUBS,		//1
			DIAMONDS,   //2
			HEARTS      //3
		};

		//Set the enum for all types in cards
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

		//create a variable
		public Type cardNum;
		
		public Suit suit;

		//create a constructor for card class
		public Card(Type cardNum, Suit suit){
			this.cardNum = cardNum;
			this.suit = suit;
		}

		//override the original ToString function
		public override string ToString(){
			return "The " + cardNum + " of " + suit;
		}

        //Get the card's value
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


		//Create a new deck
		if(!IsValidDeck()){
			deck = new ShuffleBag<Card>();

			AddCardsToDeck();
		}

		Debug.Log("Cards in Deck: " + deck.Count);
	}

	//Check the deck is empty or not
	protected virtual bool IsValidDeck(){
		return deck != null; 
	}

	//Add all kinds of cards to the deck
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

	//Draw a card
	public virtual Card DrawCard(){
		Card nextCard = deck.Next();

		return nextCard;
	}

	//Get the string 
	public string GetNumberString(Card card){
		if(card.cardNum.GetHashCode() <= 10){
			return card.cardNum.GetHashCode() + "";
		} else {
			return card.cardNum + "";
		}
	}
	
	// get the sprite of the suit
	public Sprite GetSuitSprite(Card card){
		return cardSuits[card.suit.GetHashCode()];
	}
}
