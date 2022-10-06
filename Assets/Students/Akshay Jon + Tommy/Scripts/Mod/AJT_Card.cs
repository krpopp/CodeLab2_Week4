using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//aha! a bridge between the stubborn private Core Card class and our new inheritance structure
//we derive from DeckOfCards.Card to cooperate w/ Core code that has inaccessible references to it

public class AJT_Card : DeckOfCards.Card {


	public bool choosing;
	public bool usingValue = true;
	protected AJT_BlackJackManager manager = GameObject.FindObjectOfType<AJT_BlackJackManager>();
	protected AJT_DealerHand dealerHand = GameObject.FindObjectOfType<AJT_DealerHand>();
    protected AJT_BlackJackHand playerHand = GameObject.FindObjectOfType<AJT_BlackJackHand>();

	//constructor to declare a variable of type AJT_Card
	public AJT_Card(Type cardNum, Suit suit) : base(cardNum, suit) {       
		this.cardNum = cardNum;
		this.suit = suit;
	}

	//returns either the base value of a card or it's enhanced ability's modifier
	public virtual int GetCardValue(){
		int val;	
		//default value of card
		if(usingValue) {
			switch(cardNum) {				
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
		//enhanced value of card
		else
			val = GetCardEnhancedValue();
		
		return val;
	}
	//override to return the enhanced value of a card based on its class
	public virtual int GetCardEnhancedValue() {
		int val = 0;

		return val;
	}

	//called from BlackJackHand when a card is dealt into the hand
	public virtual void TriggerEnhancedCard() {
		//default clear player buttons, show value button and card tooltip
		manager.HideAllButtons();
		manager.ShowValueButton(this);
		
		manager.EnhancedCardTooltip(GetCardValue());
	}




	public virtual void UseValue()
	{
		usingValue = true;
		playerHand.choosing = false;
	}

	public virtual void ActionOne()	{
		usingValue = false;
		playerHand.choosing = false;
	}

	public virtual void ActionTwo()	{
		usingValue = false;
		playerHand.choosing = false;
	}
}
