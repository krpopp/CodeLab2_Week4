using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager_CDLMod : OverManager
{
     #region SingletonDeclaration 
    private static Manager_CDLMod instance; 
    public static Manager_CDLMod FindInstance()
    {
        return instance; //that's just a singletone as the region says
    }

    void Awake() //this happens before the game even starts and it's a part of the singletone
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else if (instance == null)
        {
            //DontDestroyOnLoad(this);
            instance = this;
        }
    }
    #endregion

    float currentBet; //updated version that's used to visualize it, but is not saved as the actual bet yet
    //bool balanceSet = false;
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
    [SerializeField] string BalanceKey = "Balance";
    [HideInInspector] public float Balance   // the actual variable that we're accessing
	{
		get { 
            return PlayerPrefs.GetFloat(BalanceKey, defaultBalance);
            }
        set
        {
            string tempValue = PlayerPrefs.GetFloat(BalanceKey, defaultBalance).ToString();
            balance = value;
            PlayerPrefs.SetFloat(BalanceKey, balance);
            string newValue = balanceText.text.Replace (tempValue, balance.ToString());
            balanceText.text = newValue; //we update it's visual text! when it's getting set! I'm so happy I remembered you can do that:)
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
    [SerializeField] GameObject HitButton;
    [SerializeField] GameObject StayButton;

    protected virtual void Start()
        //set up the bet value and text for balance to display
    {
        string tempBalanceText = balanceText.text.Replace (replaceKey, Balance.ToString());
        balanceText.text = tempBalanceText;
        string tempBetText = betText.text.Replace (replaceKey, "0");
        betText.text = tempBetText;
        currentBet = defaultBet;
        string tempCurrentBetText = currentBetText.text.Replace (replaceKey, defaultBet.ToString());
        currentBetText.text = tempCurrentBetText;
        FinalizedBet = 0;
    }

    //if the current bet is less than the balance player has, add the bet.
    public void Increase()
    {
        if (currentBet < Balance) 
        {
            replaceKey = currentBet.ToString();
            currentBet +=100;
            UpdateValue();
        }
    }
    
    //if the bet is more than 100, plaer can decrease the bet
    public void Decrease()
    {
        if (currentBet > 100)
        {
            replaceKey = currentBet.ToString();
            currentBet -= 100;
            UpdateValue();
        }
    }

    //update the bet everytime it changed, both increase and decrease
    void UpdateValue()
    {
        string newValue = currentBetText.text.Replace (replaceKey, currentBet.ToString());
        currentBetText.text = newValue;
        Debug.Log(newValue);
    }

    //set up the ui for bet and calculate the blalance 
    public void Bet()
    {
        BetWindow.SetActive(false);
        ShowPlayerButtons();
        Balance -= currentBet;
        FinalizedBet = currentBet;
    }

    //set up the button ui for player
    void ShowPlayerButtons()
    {
        HitButton.SetActive(true);
		StayButton.SetActive(true);
    }

    //call when player wins the bet, increase the balance according to the bet.
    public void BalanceUpdate()
    {
        Balance += currentBet*2;
    }


    //set up the game ui when dealer has a blackjack
    public void CDL_DealerBlackJack()
    {
        GameOverText("Dealer Black Jack!", Color.red);
        if (HitButton.activeSelf == true)
        {
            HidePlayerButtons();
        }
        BetWindow.SetActive(false);
    }


    //set up the game ui when player has a blackjack
    public void CDL_BlackJack()
    {
        GameOverText("Black Jack!", Color.green);
        if (HitButton.activeSelf == true)
        {
            HidePlayerButtons();
        }
        BetWindow.SetActive(false);
        BalanceUpdate();
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
