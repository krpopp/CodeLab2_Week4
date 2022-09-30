using UnityEngine;


public class NMDeckOfCards : DeckOfCards
{
    protected override void AddCardsToDeck() //build deck
    {
        for (int i = 0; i <= 3; i++)
        {
            foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))) //add cards of this suit
            {
                foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))) //add cards of this type
                {
                    deck.Add(new Card(type, suit)); //update card types and suits
                }
            }
        }
    }

    public int GetDeckCount()
    {
        return deck.Count; //check deck count
    }

    //credit to Leslie's team code, remove card every time we draw
    public override Card DrawCard()
    {
        Card nextCard = deck.Next(); //draw next card

        deck.Remove(nextCard); //remove previous

        Debug.Log("Cards in Deck: " + deck.Count); //check deck count

        return nextCard; //add drawn card
    }

}
