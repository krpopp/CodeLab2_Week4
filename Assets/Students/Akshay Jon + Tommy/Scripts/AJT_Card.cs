using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_Card : DeckOfCards.Card {

	public enum Enhanced {
		VALUE,
		ENHANCED
    }

	public Enhanced enhanced;

	public AJT_Card(Type cardNum, Suit suit){		//constructor to declare a variable of type Card
			this.cardNum = cardNum;
			this.suit = suit;
	}

	public override string ToString(){				//overrides the ToString function
		return "The " + num + " of " + suit;
	}

	//BUG; write GetCardLowValue
	public virtual int GetCardValue(){					//Checks the face value on the card and in case of A,J,K or Q it assigns it a value
		int val;									//in case of it being a number it defaults to the number on it	
		switch(num) {
			//BUG; ace should be 11 or 1
			case Num.A:
				val = 11;
				break;
			case Num.K:
			case Num.Q:
			case Num.J:
				val = 10;
				break;	
			default:
				val = (int)num;
				break;
		}
		
		return val;
	}

	public virtual int GetCardEnhancedValue() {
		int val = 0;

		return val;
	}
}
