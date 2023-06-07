namespace RawDeal.DeckHandler;


public abstract class CardSet
{
    public List<Card> Cards { get; set; } = new List<Card>();

    public abstract void AddCard(Card card);

    public abstract Card RemoveCard();

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
}