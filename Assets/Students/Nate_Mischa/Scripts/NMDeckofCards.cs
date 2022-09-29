using UnityEngine;


public class NMDeckOfCards : DeckOfCards
{
    protected override void AddCardsToDeck()
    {
        for (int i = 0; i <= 3; i++)
        {
            foreach (Card.Suit suit in Card.Suit.GetValues(typeof(Card.Suit))){
                foreach (Card.Type type in Card.Type.GetValues(typeof(Card.Type))){
                    deck.Add(new Card(type, suit));
                }
            }
        }
    }

    public int GetDeckCount()
    {
        return deck.Count;
    }

    //credit to Leslie's team code, remove card every time we draw
    public override Card DrawCard()
    {
        Card nextCard = deck.Next();

        deck.Remove(nextCard);

        Debug.Log("Cards in Deck: " + deck.Count);

        return nextCard;
    }

}
