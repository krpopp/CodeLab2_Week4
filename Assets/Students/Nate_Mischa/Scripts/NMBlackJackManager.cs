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
        deck = GameObject.Find("Deck").GetComponent<NMDeckOfCards>();
    }

    public override int GetHandValue(List<DeckOfCards.Card> hand)
    {
        int handValue = 0;
        bool weHaveAnAce = false;

        foreach(DeckOfCards.Card handCard in hand){
            handValue += handCard.GetCardHighValue();
            if (handCard.cardNum == DeckOfCards.Card.Type.A)
            {
                weHaveAnAce = true;
            }
        }

        if (weHaveAnAce)
        {
            if (handValue - 11 <= 10)
            {

            }
            else
            {
                handValue -= 11;
                handValue += 1;
            }
        }
        //Debug.Log("new get hand value");
        return handValue;
    }

    //Bug4: shuffle only 20 cards left
    public void NewTryAgain()
    {
        if (deck.GetDeckCount() <= 200)
        {
            SceneManager.LoadScene(loadScene);
        }
        else
        {
            //clear all cards from scene
            //store cards removed from play
            //update the deck to exclude removed cards
            nMDealerHand.ReSetUpHand();
            nMBlackJackHand.ReSetUpHand();
        }
    }
}
