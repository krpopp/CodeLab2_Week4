using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_DealerHand : DealerHand
{
    //because this script inherits from DealerHand which in turn inherits from BlackJackHand
    //the overidden functionality of AJT_BlackJackHand is not made available to this.
    public List<DeckOfCards.Card> Hand { get { return this.hand; } }

    //BUG FIX
    //removes the remaining cards from the previous round and sets up new hands
    public void ResetHand() {
		foreach (Transform child in transform) Destroy(child.gameObject);
		SetupHand();
	}
}
