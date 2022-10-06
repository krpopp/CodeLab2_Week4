using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipsManager : MonoBehaviour
{
    [SerializeField] private const int START_CHIPS = 500;
    public static int allChipsAmount = START_CHIPS;
    public int thisRoundChips = 0;

    [SerializeField] private Text allChipText;
    [SerializeField] private Text thisRoundText;
    [SerializeField] private Text error;
    //each chip amount text
    [SerializeField] private Text five,ten,fifty,hundred;

    public JLZBlackJackHand playerHand;
    public JLZDealerHand dealerHand;

    public Button[] chipButtons;
    public Button[] stayHitButtons;

    private void Awake()
    {
        if(allChipsAmount == 0) allChipsAmount = START_CHIPS;

        ResetChips();
    }

    private void Start()
    {
        playerHand = GameObject.Find("Player Hand Value").GetComponent<JLZBlackJackHand>();
        dealerHand = GameObject.Find("Dealer Hand Value").GetComponent<JLZDealerHand>();

        foreach (Button button in stayHitButtons)
        {
            button.interactable = false;
        }

    }

    private void AddChips(int chipsPoints,Text eachChipAmout)
    {
        int eachChipAmount = Int32.Parse(eachChipAmout.text);
        if (allChipsAmount - chipsPoints >= 0)
        {
            allChipsAmount -= chipsPoints;
            thisRoundChips += chipsPoints;
            eachChipAmount += 1;
            eachChipAmout.text = eachChipAmount.ToString();
            thisRoundText.text = thisRoundChips.ToString();
            allChipText.text = allChipsAmount.ToString();
            error.text = " ";
        }
        else
            error.text = "You don't have enough chips!";
    }

    private void ReduceChips(int chipsPoints, Text eachChipAmout)
    {
        int eachChipAmount = Int32.Parse(eachChipAmout.text);
        if (eachChipAmount - 1 >= 0)
        {
            allChipsAmount += chipsPoints;
            thisRoundChips -= chipsPoints;
            eachChipAmount -= 1;
            eachChipAmout.text = eachChipAmount.ToString();
            thisRoundText.text = thisRoundChips.ToString();
            allChipText.text = allChipsAmount.ToString();
            error.text = " ";
        }
        else
            error.text = "There is no chip to be reduced!";
    }

    public void FiveAdd()
    {
        AddChips(5, five);
    }
    public void FiveReduce()
    {
        ReduceChips(5, five);
    }

    public void TenAdd()
    {
        AddChips(10, ten);
    }
    public void TenReduce()
    {
        ReduceChips(10, ten);
    }

    public void FiftyAdd()
    {
        AddChips(50, fifty);
    }
    public void FiftyReduce()
    {
        ReduceChips(50, fifty);
    }


    public void HundredAdd()
    {
        AddChips(100, hundred);
    }
    public void HundredReduce()
    {
        ReduceChips(100, hundred);
    }

    public void PlayerWin()
    {
        allChipsAmount += thisRoundChips * 2;
        Debug.Log(allChipsAmount);
    }

    /*
    public void playerLose()
    {
        ResetChips();
    }
    */

    private void ResetChips()
    {
        allChipText.text = allChipsAmount.ToString();
        five.text = 0.ToString();
        ten.text = 0.ToString();
        fifty.text = 0.ToString();
        hundred.text = 0.ToString();
        thisRoundText.text = 0.ToString();
        thisRoundChips = 0;
    }

    public void StartBuffet()
    {
        playerHand.HitMe();
        playerHand.HitMe();

        if(playerHand.handVals == 21)
        {
            JLZBlackJackManager gameManager = GameObject.Find("Game Manager").GetComponent<JLZBlackJackManager>();
            gameManager.BlackJack();
            PlayerWin();
        }

        dealerHand.HitMe();
        dealerHand.HitMe();

        GameObject cardOne = dealerHand.transform.GetChild(0).gameObject;
        cardOne.GetComponentInChildren<Text>().text = "";
        cardOne.GetComponentsInChildren<Image>()[0].sprite = dealerHand.cardBack;
        cardOne.GetComponentsInChildren<Image>()[1].enabled = false;

        dealerHand.reveal = false;

        foreach (Button button in stayHitButtons)
        {
            button.interactable = true;
        }

        foreach (Button button in chipButtons)
        {
            button.interactable = false;
        }
    }
}
