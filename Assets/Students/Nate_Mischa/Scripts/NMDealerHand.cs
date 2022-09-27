using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 1, dealer only show hands when player bust - dealer should show hands whenever players loses (lose or bust)
 * 2, when player hit 21, 'BlackJack' is not called - 'BlackJack' should be called when player hit 21
 * 3, ??? - ui bug(?), drawing more than 5 cards will result in the fifth being cut out
 * 4, when dealer hit 21, 'BlackJack' is not called - 'BlackJack' should be called when dealer hit 21
 * 5, Ace only has one value 11 - Ace should alternate between 1 and 11
 * 6, no natural blackjack - there should be natural blackjack win or lose
 */

public class NMDealerHand : DealerHand
{
	bool reveal;
	
	//Bug1: called when player bust
	//Called when player bust, show dealer's hand and update text
	public void RevealCardWhenPlayerBust()
	{
		GameObject cardOne = transform.GetChild(0).gameObject;

		cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = true;
		
		ShowCard(hand[0], cardOne, 0);
		
		handVals = GetHandValue();

		total.text = "Dealer: " + handVals;
	}
	
	protected override void ShowValue()
	{
		base.ShowValue();
		//Bug4&6: When dealer draws or natural BlackJack, call BlackJack
		if (handVals == 21)
		{
			GameObject.Find("Game Manager").GetComponent<BlackJackManager>().BlackJack();
		}
	}
}
