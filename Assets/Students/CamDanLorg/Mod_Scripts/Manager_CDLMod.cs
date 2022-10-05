using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager_CDLMod : OverManager
{
    float currentBet; //updated version that's used to visualize it, but is not saved as the actual bet yet
    bool balanceSet = false;
    private float finalizedBet; //Bet that we're actually working with in the future
    [HideInInspector] public float FinalizedBet   // the actual variable that we're accessing
	{
		get { 
			return finalizedBet; }
		set { 
                string tempValue = finalizedBet.ToString();
                finalizedBet = value;
                string newValue = betText.text.Replace (tempValue, finalizedBet.ToString());
			    betText.text = newValue;
			} 
	}
    private float balance; //how much money we generally own
    [HideInInspector] public float Balance   // the actual variable that we're accessing
	{
		get { 
			return balance; }
		set { 
            if (balanceSet)
            {
                string tempValue = balance.ToString();
                balance = value;
                string newValue = balanceText.text.Replace (tempValue, balance.ToString());
			    balanceText.text = newValue; //we update it's visual text! when it's getting set! I'm so happy I remembered you can do that:)
            } else balance = value;
                   balanceSet = true;
			} 
	}

    [Header("Player Settings")]
    [SerializeField] float defaultBet = 100;
    [SerializeField] float defaultBalance = 500;

    [Header("UI Settings")]
    [SerializeField] GameObject BetWindow;
    [SerializeField] string replaceKey; //a piece of string that needs to be replaced in the whole string with the new value
    [SerializeField] TextMeshProUGUI currentBetText;
    [SerializeField] TextMeshProUGUI betText;
    [SerializeField] TextMeshProUGUI balanceText;

    protected virtual void Start()
    {
        string tempBalanceText = balanceText.text.Replace (replaceKey, defaultBalance.ToString());
        balanceText.text = tempBalanceText;
        string tempBetText = betText.text.Replace (replaceKey, "0");
        betText.text = tempBetText;
        Balance = defaultBalance;
        FinalizedBet = 0;
    }
    public void BettingOn()
    {
        BetWindow.SetActive(true);
        currentBet = defaultBet;
        replaceKey = defaultBet.ToString();
        UpdateValue();
    }

    public void Increase()
    {
        Debug.Log(Balance);
        Debug.Log(balanceSet);
        if (currentBet < Balance) // if current bet is not larger than the amount of money we have 
        {
            currentBet +=100;
            Debug.Log(currentBet);
            UpdateValue();
        }
    }
    
    public void Decrease()
    {
        if (currentBet > 100)
        {
            currentBet -= 100;
            UpdateValue();
        }
    }

    void UpdateValue()
    {
        string newValue = currentBetText.text.Replace (replaceKey, currentBet.ToString());
        replaceKey = currentBet.ToString();
        currentBetText.text = newValue;
    }

    public void Bet()
    {
        BetWindow.SetActive(false);
        Balance -= currentBet;
        Debug.Log(Balance);
        FinalizedBet = currentBet;
    }
}
