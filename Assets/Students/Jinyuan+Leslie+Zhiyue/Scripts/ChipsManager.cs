using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipsManager : MonoBehaviour
{
    [SerializeField] private int startChips = 500;
    public int allChipsAmount;
    public int thisRoundChips = 0;

    [SerializeField] private Text allChipText;
    [SerializeField] private Text thisRoundText;
    [SerializeField] private Text error;
    //each chip amount text
    [SerializeField] private Text five,ten,fifty,hundred;

    private void Awake()
    {
        allChipsAmount = startChips;
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

    public void fiveAdd()
    {
        AddChips(5, five);
    }
    public void fiveReduce()
    {
        ReduceChips(5, five);
    }

    public void tenAdd()
    {
        AddChips(10, ten);
    }
    public void tenReduce()
    {
        ReduceChips(10, ten);
    }

    public void fiftyAdd()
    {
        AddChips(50, fifty);
    }
    public void fiftyReduce()
    {
        ReduceChips(50, fifty);
    }


    public void hundredAdd()
    {
        AddChips(100, hundred);
    }
    public void hundredReduce()
    {
        ReduceChips(100, hundred);
    }

    public void playerWin()
    {
        allChipsAmount += thisRoundChips * 2;
        allChipText.text = allChipsAmount.ToString();
        Reset();
    }

    public void playerLose()
    {
        Reset();
    }

    private void Reset()
    {
        five.text = 0.ToString();
        ten.text = 0.ToString();
        fifty.text = 0.ToString();
        hundred.text = 0.ToString();
        thisRoundText.text = 0.ToString();
        thisRoundChips = 0;
    }
}
