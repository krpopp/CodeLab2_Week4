using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//BUG; dealer continues to hit even if they're above the player

public class DealerHand : BlackJackHand {

	//Ref to scene gameObject
	public Sprite cardBack;
	//Tracks if dealer has revealed their hand
	bool reveal;

	//Override to hide the first card from player
	protected override void SetupHand(){
		
		base.SetupHand();

		//hide first card
		GameObject cardOne = transform.GetChild(0).gameObject;
		cardOne.GetComponentInChildren<Text>().text = "";
		cardOne.GetComponentsInChildren<Image>()[0].sprite = cardBack;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = false;
		//default value for hidden card
		reveal = false;
	}
		
	protected override void ShowValue(){

		if(hand.Count > 1){

			//shows value of dealer's revealed cards only
			if(!reveal){
				//sets handVals to value of revealed card
				handVals = hand[1].GetCardHighValue();

				total.text = "Dealer: " + handVals + " + ???";
			} else {
				//shows value of all dealer cards
				handVals = GetHandValue();

				total.text = "Dealer: " + handVals;

				BlackJackManager manager = GameObject.Find("Game Manager").GetComponent<BlackJackManager>();

				//handles dealer bust
				if(handVals > 21){
					manager.DealerBusted();
					//draws new card for dealer if dealer is under 17
				} else if(!DealStay(handVals)){
					Invoke("HitMe", 1);
				} else {
					// once dealer stays, compares dealer and player hand values
					BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();

					if(handVals < playerHand.handVals){
						//player wins if player has higher total than dealer
						manager.PlayerWin();
					} else {
						//player loses in case of tie or higher dealer hand
						manager.PlayerLose();
					}
				}
			}
		}
	}

	//returns true when dealer is under 17
	//BUG; should stay when value is above player value
	protected virtual bool DealStay(int handVal){
		return handVal > 17;
	}

	//function to update game UI to reveal previously hidden dealer card
	public void RevealCard(){
		//sets true when card is revealed
		reveal = true;


		GameObject cardOne = transform.GetChild(0).gameObject;

		cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = true;

		//passes previously hidden card to ShowCard, updates game UI to reveal that card
		ShowCard(hand[0], cardOne, 0);
		//updates value of dealer hand
		ShowValue();
	}
}
