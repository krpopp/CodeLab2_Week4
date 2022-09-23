using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class JLZBlackJackManager : BlackJackManager
{

	public Text statusText;
	public GameObject tryAgain;
	public string loadScene;


	// notify that the player busts
	public void PlayerBusted(){
		HidePlayerButtons();
		GameOverText("YOU BUST", Color.red);
	}

	// notify the dealer busts
	public void DealerBusted(){
		GameOverText("DEALER BUSTS!", Color.green);
	}
		
	// notify the player wins
	public void PlayerWin(){
		GameOverText("YOU WIN!", Color.green);
	}
		
	// notify the player loses
	public void PlayerLose(){
		GameOverText("YOU LOSE.", Color.red);
	}

	// notify "black jack" and hide the player button 
	public void BlackJack(){
		GameOverText("Black Jack!", Color.green);
		HidePlayerButtons();
	}

	// set the game over text active
	public void GameOverText(string str, Color color){
		statusText.text = str;
		statusText.color = color;

		tryAgain.SetActive(true);
	}

	// disable the hit button and stay button 
	public void HidePlayerButtons(){
		GameObject.Find("HitButton").SetActive(false);
		GameObject.Find("StayButton").SetActive(false);
	}

	// reset the game
	public void TryAgain(){
		SceneManager.LoadScene(loadScene);
	}

	// get the sum of card value
	public virtual int GetHandValue(List<DeckOfCards.Card> hand){
		int handValue = 0;

		foreach(DeckOfCards.Card handCard in hand){
			handValue += handCard.GetCardHighValue();
		}
		return handValue;
	}
}
