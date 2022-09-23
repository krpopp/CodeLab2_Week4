using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BlackJackManager : MonoBehaviour {

	public Text statusText;
	public GameObject tryAgain;
	public string loadScene;

	//if the player's hand is over 21. 
	public void PlayerBusted(){
		HidePlayerButtons(); 
		GameOverText("YOU BUST", Color.red); //displays game over text. 
	}

	//if dealer's hand is over 21. 
	public void DealerBusted(){ 
		GameOverText("DEALER BUSTS!", Color.green); //displays game over tex.t 
	}
		
	//if the player's hand is larger than dealers
	public void PlayerWin(){
		GameOverText("YOU WIN!", Color.green);
	}
		
	//if dealers hand is larger than players. 
	public void PlayerLose(){
		GameOverText("YOU LOSE.", Color.red);
	}


	//if player or dealer have a handvalue of 21. 
	public void BlackJack(){
		GameOverText("Black Jack!", Color.green);
		HidePlayerButtons();
	}

	//prints game over text, activates try again button. 
	public void GameOverText(string str, Color color){
		statusText.text = str;
		statusText.color = color;

		tryAgain.SetActive(true);
	}

	//disables hit and stay buttons at the end of the game. 
	public void HidePlayerButtons(){
		GameObject.Find("HitButton").SetActive(false);
		GameObject.Find("StayButton").SetActive(false);
	}

	//reloads the scene. 
	public void TryAgain(){
		SceneManager.LoadScene(loadScene);
	}

	//gets the value of the player and dealer's hand. 
	public virtual int GetHandValue(List<DeckOfCards.Card> hand){
		int handValue = 0;

		foreach(DeckOfCards.Card handCard in hand){
			handValue += handCard.GetCardHighValue();
		}
		return handValue;
	}
}
