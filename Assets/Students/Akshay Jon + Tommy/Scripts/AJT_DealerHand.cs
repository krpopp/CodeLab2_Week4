using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_DealerHand : DealerHand
{

    //BUG FIX
    //removes the remaining cards from the previous round and sets up new hands
    //because this script inherits from DealerHand which in turn inherits from Black Jack Hand
    //the overidden functions from Black Jack hand is not made available to this.
	public void ResetHand() {
		foreach (Transform child in transform) Destroy(child.gameObject);
		SetupHand();
	}
}
