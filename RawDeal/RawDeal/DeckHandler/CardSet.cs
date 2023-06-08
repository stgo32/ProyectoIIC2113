namespace RawDeal.DeckHandler;


public class CardSet
{
    public List<Card> Cards { get; set; } = new List<Card>();

    public virtual void AddCard(Card card)
    {
        Cards.Add(card);
    }

    public virtual Card RemoveCard()
    {
        Card card = GetCard(0);
        Cards.RemoveAt(0);
        return card;
    }

    public Card GetCard(int index)
    {
        return Cards[index];
    }

    public Card RemoveAt(int index)
    {
        Card card = GetCard(index);
        Cards.RemoveAt(index);
        return card;
    }

    public bool IsEmpty()
    {
        return Cards.Count == 0;
    }

    public bool Any()
    {
        return Cards.Count > 0;
    }

    public bool ContainsMoreOrEqualThan(int number)
    {
        return Cards.Count >= number;
    }

    public int Count()
    {
        return Cards.Count;
    }

    protected void Clear()
    {
        Cards.Clear();
    }

    protected void ManageCountersOptionsForCardAppearances(Card possibleCard, ref int cardCount, 
                                                         ref int hibridCounter)
    {
        if (possibleCard.isHibrid && hibridCounter == 0)
        {
            hibridCounter++;
        }
        else if (possibleCard.isHibrid && hibridCounter == 1)
        {
            cardCount++;
            hibridCounter = 0;
        }
        else
        {
            cardCount++;
        }
    }
}