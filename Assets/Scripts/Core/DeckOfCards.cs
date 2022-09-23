using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DeckOfCards : MonoBehaviour {
	
	public Text cardNumUI;	//display the number on the card.
	public Image cardImageUI;	//displays the card image. 
	public Sprite[] cardSuits;	//array of sprites for each suit. 

	public class Card{ //declares the suit of a card, and the number the card. 

		//declaring enum variables for each suits of the cards. 
		public enum Suit {
			SPADES, 	//0
			CLUBS,		//1
			DIAMONDS,	//2
			HEARTS	 	//3
		};

		//declares enum types and assigns them a value from 2 - 14.
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

		public Type cardNum; //declares new variable of Type type enum. lol
		
		public Suit suit;	//declares new variable of Suit type enum. 

		//constructor that creates a card and assigns a value and a suit to it. 
		public Card(Type cardNum, Suit suit){
			this.cardNum = cardNum;
			this.suit = suit;
		}

		//String that displays the cardnum and the suit of the card. 
		public override string ToString(){
			return "The " + cardNum + " of " + suit;
		}

		//Compares cards and finds the
		public int GetCardHighValue(){
			int val; //declares new int variable. 

			//switch break
			switch(cardNum){
			case Type.A: //if card number Type type is assigned value of A
				val = 11; //card value will be set to 11.
				break;
			case Type.K: //you already know >.>
			case Type.Q: //BUUUUUUUUUUGS
			case Type.J:
				val = 10;
				break;	
			default: //For every other card in Type enum, 
				val = (int)cardNum; //we set its value to its given cardnum. 
				break;
			}

			return val; //returns the value
		}
	}
		      //(ShuffleBag<Card> is a declared class variable)
	public static ShuffleBag<Card> deck; //declaring a shufflebag type variable,
										//that is given card class objects in it,
										//names it deck.

										// Use this for initialization
	void Awake () {

		//if we don't have a deck, we create a new deck.
		if(!IsValidDeck()){ //"if isvalid deck false"
			deck = new ShuffleBag<Card>(); //Fills the deck variable with new
										  //instance of type shufflebag

			AddCardsToDeck(); //populates the deck.
		}

		Debug.Log("Cards in Deck: " + deck.Count);
	}

	//checks if we have a deck. 
	protected virtual bool IsValidDeck(){
		return deck != null; 
	}

	//this is how we populate the deck. 
	protected virtual void AddCardsToDeck(){
		//for each type of suit in enum, and each card type existing within that suit,
		//we create a card with the type and suit that we are looping for now and add it to the deck. 
		foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))){
			foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))){
				deck.Add(new Card(type, suit));
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public virtual Card DrawCard(){
		Card nextCard = deck.Next();//return the next card in the deck, start from ghe end.

		return nextCard;
	}

//is for getting the number on the card and convert it in a string
	public string GetNumberString(Card card)
	{
		//if the card is 2~10
		if(card.cardNum.GetHashCode() <= 10)
		{
			return card.cardNum.GetHashCode() + "";
		} 
		else//if the card is JQKA 
		{
			return card.cardNum + "";
		}
	}
		
	//check the suit value and find the correct sprite in cardSuits array.
	public Sprite GetSuitSprite(Card card){
		return cardSuits[card.suit.GetHashCode()];
	}
}
