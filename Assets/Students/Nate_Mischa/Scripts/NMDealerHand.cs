using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 1, dealer only show hands when player bust - dealer should show hands whenever players loses (lose or bust)
 * 2, when player hit 21, 'BlackJack' is not called - 'BlackJack' should be called when player hit 21
 * 3, the starting deck only have 52 cards and constantly reshuffle when player tries Again
 *		- starting deck made up of 4 decks and only reshuffles after deck count reaches 20
 * 4, when dealer hit 21, 'BlackJack' is not called - 'BlackJack' should be called when dealer hit 21
 * 5, Ace only has one value 11 - Ace should alternate between 1 and 11
 * 6, no natural blackjack - there should be natural blackjack win or lose
 * 7, ui bug(?), drawing more than 5 cards will result in the fifth being cut out
 */

public class NMDealerHand : DealerHand
{
	// the replacement of the private 'reveal'
	bool newReveal;
	// if the face down card is swapped
	public bool isSwapped;

	public NMBlackJackHand blackJackHand;
	
	//Bug1: called when player bust
	//Called when player bust, show dealer's hand and update text
	public void RevealCardWhenPlayerBust()
	{
		GameObject cardOne = transform.GetChild(0).gameObject; //check face down card

		cardOne.GetComponentsInChildren<Image>()[0].sprite = null; //card sprite disabled
		cardOne.GetComponentsInChildren<Image>()[1].enabled = true; //show card sprite
		
		ShowCard(hand[0], cardOne, 0); //show card value
		
		handVals = GetHandValue(); //dealer hand value

		total.text = "Dealer: " + handVals; //dealer hand vale text display
	}
	
	protected override void ShowValue()
	{
		if(hand.Count > 1)
		{
			// check if the player is not 'stay'
			if(!newReveal)
			{
				//check if the face down card is not swapped
				if (!isSwapped)
				{
					//there is no change for the value
					handVals = hand[1].GetCardHighValue();

					total.text = "Dealer: " + handVals + " + ???";
				}
				else
				{
					//if the face down card is swapped, update the text with newly revealed card. 
					handVals = GetHandValue();
			
					total.text = "Dealer: " + handVals;
				}
			} 
			else //if the player stay
			{
				handVals = GetHandValue();

				total.text = "Dealer: " + handVals;

				BlackJackManager manager = GameObject.Find("Game Manager").GetComponent<BlackJackManager>();

				//check if the dealer bust
				if(handVals > 21)
				{
					manager.DealerBusted();
				} 
				//check if dealer's hand values is less than 17
				else if(!DealStay(handVals))
				{
					//if it is, automatically draw a card
					Invoke("HitMe", 1);
				} 
				//if dealer's hand values is greater than 17 and dealer is not bust
				else 
				{
					//compare hand value with player's
					BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();
					
					if(handVals < playerHand.handVals)
					{
						manager.PlayerWin();
					} 
					else 
					{
						manager.PlayerLose();
					}
				}
			}
		}
		//Bug4&6: When dealer draws or natural BlackJack, call BlackJack
		if (handVals == 21)
		{
			GameObject.Find("Game Manager").GetComponent<BlackJackManager>().BlackJack(); //call GM
		}
	}
	
	//use this function to set access of dealer hand List from BlackJackHand scripts
	public void SetDealerHandValue(int cardIndex,DeckOfCards.Card newCard)
	{
		hand[cardIndex] = newCard;
		ShowValue();
	}

	//use this function to get access of dealer hand List from BlackJackHand scripts
	public DeckOfCards.Card GetDealerHandVale(int cardIndex)
	{
		return hand[cardIndex];
	}

	//called when player swap the face down card and reveal it
	public void RevealCardWhenSwap()
	{
		GameObject cardOne = transform.GetChild(0).gameObject;

		cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = true;

		ShowCard(hand[0], cardOne, 0);
	}
	
	//replacement for 'RevealCard' function
	public void NewRevealCard()
	{
		newReveal = true;
		//when player click stay button, deactivate the swap button
		blackJackHand.SwapButton.interactable = false;
		
		// check if the face down card is already swapped
		if (isSwapped)
		{
			//if it is, directly showValue
			ShowValue();
		}
		else
		{
			GameObject cardOne = transform.GetChild(0).gameObject;

			cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
			cardOne.GetComponentsInChildren<Image>()[1].enabled = true;

			ShowCard(hand[0], cardOne, 0);

			ShowValue();
		}
	}
}
