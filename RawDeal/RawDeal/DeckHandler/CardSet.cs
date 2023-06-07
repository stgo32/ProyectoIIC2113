namespace RawDeal.DeckHandler;


public abstract class CardSet
{
    public List<Card> Cards { get; set; }

    public abstract void AddCard(Card card);

    public abstract Card RemoveCard();

    public Card GetCard(int index)
    {
        return Cards[index];
    }

    public void RemoveAt(int index)
    {
        Cards.RemoveAt(index);
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
}