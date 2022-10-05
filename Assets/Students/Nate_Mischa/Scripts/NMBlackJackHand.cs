using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NMBlackJackHand : BlackJackHand
{
    public NMBlackJackHand hitMe;
    public NMDealerHand dealerHand;

    public Button SwapButton;

    bool playerBust = false;
    bool blackJack = false;

    bool hold = false;
    bool swapSelect = false;

    public GameObject SelectedCard;
    public bool playersCard;

    protected override void ShowValue()
    {
        handVals = GetHandValue(); //get player hand value
			
        total.text = "Player: " + handVals; //player hand value text display

        if(handVals > 21) //if player busts, hand exceeds 21
        {
            GameObject.Find("Game Manager").GetComponent<BlackJackManager>().PlayerBusted();
            //Bug1: called when player bust(dealer show hands value)
            dealerHand.RevealCardWhenPlayerBust();
            playerBust = true;
        }
        //Bug2&6: call blackjack when player hit 21 or natural Blackjack
        else if (handVals == 21)
        {
            GameObject.Find("Game Manager").GetComponent<BlackJackManager>().BlackJack(); //call GM
            blackJack = true;
        }
    }
    
    public void ReSetUpHand()
    {
        for (int i = 0; i < hand.Count; i++) //check all card obj in hand
        {
            Destroy(handBase.transform.GetChild(i).gameObject); //destroy cards in hand
        }
        hand.Clear(); //clear hand
        SetupHand(); //reset hand
    }

    public void HitMe()
    {
        if (!hold)
        {
            DeckOfCards.Card card = deck.DrawCard(); //draw new card

            GameObject cardObj = Instantiate(Resources.Load("prefab/Card")) as GameObject; //instantiate visuals

            ShowCard(card, cardObj, hand.Count); //reveal card

            hand.Add(card); //add to hand

            ShowValue(); //update value

            SwapButton.interactable = true; //make swap button interactable

            if (playerBust == true || blackJack == true) //if bust or blackjack
            {
                SwapButton.interactable = false; //set to false
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && swapSelect == true) //if left mouse is clicked && swapSelect is true
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current); //trigger raycasting
            eventData.position = Input.mousePosition; //get input
            List<RaycastResult> raycastResults = new List<RaycastResult>(); //generate list of raycasted objects
            EventSystem.current.RaycastAll(eventData, raycastResults); //update list with raycast
            foreach (var result in raycastResults)
            {
                //split the name of the raycasted object by ' ' and store them into a string array
                string[] splitArray = result.gameObject.name.Split(char.Parse(" "));
                if (splitArray[0] == "The")//check the first element of the string array equal to "The"
                {
                    if (SelectedCard == null)
                    {
                        SelectedCard = result.gameObject; //store the card selected into the SelectedCard
                        Debug.Log(result.gameObject.name); //debug the name of the card we click

                        if (SelectedCard.transform.parent == gameObject.transform)
                        {
                            playersCard = true;
                            Debug.Log("first select player's card");
                        }
                        else
                        {
                            playersCard = false;
                            Debug.Log("first select dealer's card");
                        }
                    }
                    else
                    {
                        if (playersCard == true)
                        {
                            if (result.gameObject.transform.parent == dealerHand.transform)
                            {
                                Debug.Log("Legal Swap, deactivate swap button");
                                SwapCards(SelectedCard, result.gameObject);
                                SelectedCard = null;
                                SwapButton.interactable = false;
                            }
                            else
                            {
                                SelectedCard = null;
                                Debug.Log("Illegal Swap, exit swap mode");
                            }
                        }
                        else
                        {
                            if (result.gameObject.transform.parent == gameObject.transform)
                            {
                                Debug.Log("Legal Swap, deactivate swap button");
                                SwapCards(result.gameObject,SelectedCard);
                                SelectedCard = null;
                                SwapButton.interactable = false;
                            }
                            else
                            {
                                SelectedCard = null;
                                Debug.Log("Illegal Swap, exit swap mode");
                            }
                        }
                    }
                }
            }//debug results, check if objects have been clicked
        }
    }

    public void SwapMe()
    {
        swapSelect = true;
    }

    public void SwapCards(GameObject playersCard, GameObject dealersCard)
    {
        Vector3 tempPosition = Vector3.zero;
        Transform tempTransform = new RectTransform();
        DeckOfCards.Card tempCard = new DeckOfCards.Card(DeckOfCards.Card.Type.A,DeckOfCards.Card.Suit.CLUBS);
        
        //swap hand value
        tempCard = hand[playersCard.GetComponent<RectTransform>().GetSiblingIndex()];
        hand[playersCard.GetComponent<RectTransform>().GetSiblingIndex()] =
            dealerHand.GetDealerHandVale(dealersCard.GetComponent<RectTransform>().GetSiblingIndex());
        dealerHand.SetDealerHandValue(dealersCard.GetComponent<RectTransform>().GetSiblingIndex(),tempCard);
        ShowValue();
        
        //swap card1 and card2 rect position
        tempPosition = playersCard.GetComponent<RectTransform>().position;
        playersCard.GetComponent<RectTransform>().position = dealersCard.GetComponent<RectTransform>().position;
        dealersCard.GetComponent<RectTransform>().position = tempPosition;
        
        //swap card1 and card2 parent
        tempTransform = playersCard.GetComponent<RectTransform>();
        playersCard.GetComponent<RectTransform>().parent = dealersCard.GetComponent<RectTransform>().parent;
        dealersCard.GetComponent<RectTransform>().parent = tempTransform;
    }
}
