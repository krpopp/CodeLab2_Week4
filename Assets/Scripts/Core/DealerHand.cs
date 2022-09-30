using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DealerHand : BlackJackHand {

	public Sprite cardBack;

	bool reveal;

	//Make the dealer's hand, flip over the first card
	protected override void SetupHand(){
		base.SetupHand();

		GameObject cardOne = transform.GetChild(0).gameObject;
		cardOne.GetComponentInChildren<Text>().text = "";
		cardOne.GetComponentsInChildren<Image>()[0].sprite = cardBack;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = false;

		reveal = false;
	}

	//determine dealer's score based on which cards have been revealed
	//determine if the dealer has busted, should hit, or should stay (and who wins)
	protected override void ShowValue(){

		if(hand.Count > 1){
			if(!reveal){
				handVals = hand[1].GetCardHighValue();

				total.text = "Dealer: " + handVals + " + ???";
			} else {
				handVals = GetHandValue();

				total.text = "Dealer: " + handVals;

				BlackJackManager manager = GameObject.Find("Game Manager").GetComponent<BlackJackManager>();

				if(handVals > 21){
					manager.DealerBusted();
				} else if(!DealStay(handVals)){
					Invoke("HitMe", 1);
				} else {
					BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();

					if(handVals < playerHand.handVals){
						manager.PlayerWin();
					} else {
						manager.PlayerLose();
					}
				}
			}
		}
	}

	//determines when the dealer should stay
    //BUG: dealer shouldn't always hit over 17
	protected virtual bool DealStay(int handVal){
		return handVal > 17;
	}

	//flip over the dealer's first card
	public void RevealCard(){
		reveal = true;

		GameObject cardOne = transform.GetChild(0).gameObject;

		cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = true;

		ShowCard(hand[0], cardOne, 0);

		ShowValue();
	}
}
