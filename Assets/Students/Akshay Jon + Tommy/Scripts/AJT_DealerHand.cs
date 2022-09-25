using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_DealerHand : DealerHand
{
	public void ResetHand() {
		foreach (Transform child in transform) Destroy(child.gameObject);
		SetupHand();
	}
    protected override bool DealStay(int handVal)
    {
		return handVal > 17;
	}
}
