using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class JLZBlackJackManager : BlackJackManager
{
    ChipsManager chipsManager;
    public void Awake()
    {
        chipsManager = GameObject.Find("ChipsManager").GetComponent<ChipsManager>();
    }
    /*
    new public void PlayerBusted()
    {
        HidePlayerButtons();
        GameOverText("YOU BUST", Color.red);
        //chipsManager.playerLose();
    }

    new public void DealerBusted()
    {
        GameOverText("DEALER BUSTS!", Color.green);
        chipsManager.PlayerWin();
    }

    new public void PlayerWin()
    {
        GameOverText("YOU WIN!", Color.green);
        chipsManager.PlayerWin();
    }

    new public void PlayerLose()
    {
        GameOverText("YOU LOSE.", Color.red);
        //chipsManager.playerLose();
    }


    new public void BlackJack()
    {
        GameOverText("Black Jack!", Color.green);
        HidePlayerButtons();
        chipsManager.PlayerWin();
    }
    */

    public override int GetHandValue(List<DeckOfCards.Card> hand){
		// get the sum of card value
		int handValue = 0;
		foreach(DeckOfCards.Card handCard in hand){
			handValue += handCard.GetCardHighValue();
		}

		// BUG FIX (Ace): Ace can either be 1 or 11, whichever is more advantagous to the player
		int i = 0;
		while (handValue > 21 && i < hand.Count)
		{
			if (hand[i].GetCardHighValue() == 11)
			{
				handValue -= 10;
			}
			i++;
		}
        return handValue;
	}

}
