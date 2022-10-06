using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_CDLMod : BlackJackHand
{
    protected override void SetupHand(){
        base.SetupHand();

		if(GetHandValue() == 21){
			BlackJackManager manager = Manager_CDLMod.FindInstance();

			manager.BlackJack();
		}
    }
}
