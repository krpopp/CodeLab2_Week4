using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class NMBlackJackManager : BlackJackManager
{
    protected NMDeckOfCards deck;

    public NMBlackJackHand nMBlackJackHand;
    public NMDealerHand nMDealerHand;

    private void Start()
    {
        deck = GameObject.Find("Deck").GetComponent<NMDeckOfCards>(); //get deck
    }

    public override int GetHandValue(List<DeckOfCards.Card> hand)
    {
        int handValue = 0;
        bool weHaveAnAce = false;

        foreach(DeckOfCards.Card handCard in hand)
        {
            handValue += handCard.GetCardHighValue(); //check hand values
            if (handCard.cardNum == DeckOfCards.Card.Type.A) //if card of type a is in hand
            {
                weHaveAnAce = true; //record ace
            }
        }

        if (weHaveAnAce) //if ace is in hand
        {
            if (handValue - 11 <= 10) //if hand minus 11 is less than or equal to 10, ace value defaults to 11
            {

            }
            else //so set to
            {
                handValue -= 11; //1
                handValue += 1; //11
            }
        }
        //Debug.Log("new get hand value");
        return handValue; //update hand value
    }

    //Bug4: shuffle only 20 cards left
    public void NewTryAgain()
    {
        if (deck.GetDeckCount() <= 20) //if deck count is less than 20
        {
            SceneManager.LoadScene(loadScene); //reload scene
        }
        else
        {
            //clear all cards from scene
            //store cards removed from play
            //update the deck to exclude removed cards
            nMDealerHand.ReSetUpHand(); //reset dealer hand
            nMBlackJackHand.ReSetUpHand(); //reset player hand
        }
    }
}
