using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_BlackJackHand : BlackJackHand
{    
	//public accessor for hands
	public List<DeckOfCards.Card> Hand { get { return hand; } }


    //BUG FIX
    //removes the remaining cards from the previous round and sets up new hands
	public void ResetHand() {
		//This loop gave us so much trouble.
		//We weren't grabbing all the children, and w/o DestroyImmediate the code doesn't execute in time for other calls
        for (int i = transform.childCount - 1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
		SetupHand();
    }

    //BUG FIX
    //overidden to check for natural Black Jack
    protected override void ShowValue()
    {
        base.ShowValue();

        //check for blackjack
        if (handVals == 21) { 
            //call for natural blackjack
            if (hand.Count == 2) {
                GameObject.Find("Game Manager").GetComponent<AJT_BlackJackManager>().BlackJack();
            }
            //Automatically stay if the player has 21
            else
                GameObject.Find("Game Manager").GetComponent<AJT_BlackJackManager>().PlayerStays();
        }
    }


}
