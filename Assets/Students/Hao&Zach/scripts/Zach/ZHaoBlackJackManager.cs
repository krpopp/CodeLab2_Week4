using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZHaoBlackJackManager : BlackJackManager
{
    //pot elements for betting
    public Text moneyText;
    public Text betText;
    int pot = 0;
    private int money = 1000;
    public Button betButton;
    
    //bools for RoundOver
    //not sure if these work as intended, but they dont seem to break anything
    public bool playerBust;
    public bool dealerBust;
    public bool player21;
    public bool dealer21;


    //bugfix to set the value of Ace cards
    public override int GetHandValue(List<DeckOfCards.Card> hand)
    {
        int handValue = 0;

        foreach (DeckOfCards.Card handCard in hand)
        {
            handValue += handCard.GetCardHighValue();
        }

        //if busted, check if there is an ace in hand
        if (handValue > 21)
        {
            foreach (DeckOfCards.Card handCard in hand)
            {
                //if there is an ace, subtract 10 from hand value until the total is less than 21
                if (handCard.cardNum == DeckOfCards.Card.Type.A)
                {
                    handValue -= 10;
                    if (handValue < 21)
                        break;
                }
            }
        }

        return handValue;
    }

    //set the pot and betting text
    private void Start()
    {
        //set pot size, 20 from dealer and player
        pot = 40;
        betText.text = "BETS: $" + pot.ToString();
        //initial bet of 20
        AdjustMoney(-20);
        moneyText.text = "$" + GetMoney().ToString();
    }

    //adjust player money amount
    public void AdjustMoney(int amount)
    {
        money += amount;
    }

    public int GetMoney()
    {
        return money;
    }
    
    //check for a winner and loser, if so hand is over
    //this is mostly here to consolidate the round being over for betting
    //it would be better to modify the functions but they are not virtual 
    void RoundOver()
    {
        //if none of these are true, round isnt over
        if (!playerBust && !player21 && !dealerBust && !dealer21)
        {
            return;
        }

        bool roundOver = true;

        //all bust, bets returned
        if (playerBust && dealerBust)
        {
            AdjustMoney(pot / 2);
        }
        //dealer wins
        else if (playerBust || !dealerBust)
        {
            //player loses bet
        }
        //player wins
        else if (dealerBust)
        {
            AdjustMoney(pot);
        }
        else
        {
            roundOver = false;
        }

        if (roundOver)
        {
            moneyText.text = "$" + GetMoney().ToString();
        }
    }

    //when the player makes a bet
    public void BetClicked()
    {
        Text newBet = betButton.GetComponentInChildren(typeof(Text)) as Text;
        //parse the text in the bet button and remove everything but the numbers to get the 20,
        //pretty neat
        int bet = int.Parse(newBet.text.ToString().Remove(0, 4));
        AdjustMoney(-bet);
        moneyText.text = "$" + GetMoney().ToString();
        pot += (bet * 2);
        betText.text = "BETS: $" + pot.ToString();
    }
}
