using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_DeckOfCards : DeckOfCards
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public int GetCardLowValue()
	{                   //Checks the face value on the card and in case of A,J,K or Q it assigns it a value
		int val;                                    //in case of it being a number it defaults to the number on it

		switch (cardNum)
		{
			//BUG; ace should be 11 or 1
			case Type.A:
				val = 1;
				break;
			case Type.K:
			case Type.Q:
			case Type.J:
				val = 10;
				break;
			default:
				val = (int)cardNum;
				break;
		}

		return val;
	}

}
