using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_Card : DeckOfCards.Card {


	public bool choosing;
	public bool usingValue = false;
	protected AJT_BlackJackManager blackJackManager = GameObject.FindObjectOfType<AJT_BlackJackManager>();
	protected AJT_DealerHand dealerHand = GameObject.FindObjectOfType<AJT_DealerHand>();
    protected AJT_BlackJackHand playerHand = GameObject.FindObjectOfType<AJT_BlackJackHand>();
    protected AJT_Card dealerCard;

    public AJT_Card(Type cardNum, Suit suit) : base(cardNum, suit)
	{       //constructor to declare a variable of type Card
		this.cardNum = cardNum;
		this.suit = suit;
	}


	public override string ToString(){				//overrides the ToString function
		return "The " + cardNum + " of " + suit;
	}

	//BUG; write GetCardLowValue
	public virtual int GetCardValue(){					//Checks the face value on the card and in case of A,J,K or Q it assigns it a value
		int val;									//in case of it being a number it defaults to the number on it	
		if(usingValue)
		{
			switch(cardNum) {
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
		}
		else
		val = GetCardEnhancedValue();
		
		return val;
	}

	public virtual void TriggerEnhancedCard() {
		choosing = true;

        blackJackManager.HidePlayerButtons();
		blackJackManager.ShowValueButton(this);

}


	public virtual int GetCardEnhancedValue() {
		int val = 0;

		return val;
	}

	public virtual void UseValue()
	{
		usingValue = true;
		choosing = false;
		GameObject.Find("Player Hand").GetComponent<AJT_BlackJackHand>().GetValue();
	}

	public virtual void ActionOne()
	{
		
	}

	public virtual void ActionTwo()
	{

	}
}
