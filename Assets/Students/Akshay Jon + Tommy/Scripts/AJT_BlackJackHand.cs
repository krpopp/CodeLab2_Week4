using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_BlackJackHand : BlackJackHand {
  
    public List<DeckOfCards.Card> Hand { get { return hand; } }

    //BUG FIX
    //removes the remaining cards from the previous round and sets up new hands	
    public void ResetHand() {
        foreach (Transform child in transform) Destroy(child.gameObject);
        SetupHand();
	}
    
    //BUG FIX
    //overidden to update game manager ref and check for natural Black Jack
    protected override void ShowValue()
    {
        handVals = GetHandValue();
        //Display text
        total.text = "Player: " + handVals;
        //Trigger bust if over 21
        if (handVals > 21)
            GameObject.Find("Game Manager").GetComponent<AJT_BlackJackManager>().PlayerBusted();
        
        //calls the blackjack function from BlackJackManager anytime the player gets a natural black jack
        if (handVals == 21 && hand.Count == 2)        
            GameObject.Find("Game Manager").GetComponent<AJT_BlackJackManager>().BlackJack();
         
        else if (handVals == 21) 
            GameObject.Find("Game Manager").GetComponent<AJT_BlackJackManager>().PlayerStays();

    }


}
