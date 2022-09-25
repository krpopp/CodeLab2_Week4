using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_DeckOfCards : DeckOfCards
{
	new public static ShuffleBag<Card> deck;

	new public class Card{
		
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

		public Type cardNum;

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

		public int GetCardLowValue()
		{   

			GetCardHighValue();
							//Checks the face value on the card and in case of A,J,K or Q it assigns it a value
			int val;                                    //in case of it being a number it defaults to the number on it

			switch (cardNum)
			{
				//BUG; ace should be 11 or 1
				case Type.A:
					val = 1;
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
}
