using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_CDLMod : BlackJackHand
{
    protected override void SetupHand()
	{
        base.SetupHand();
		//check if the player has a blackjack at the begin, if so call func to set up game ui
		if(GetHandValue() == 21)
		{
			Manager_CDLMod manager = Manager_CDLMod.FindInstance();

			manager.CDL_BlackJack();
		}
    }
}
