namespace RawDeal.DeckHandler;


public class Arsenal : CardSet
{

    public override void AddCard(Card card)
    {
        Cards.Insert(0, card);
    }

    public override Card RemoveCard()
    {
        Card card = GetCard(Cards.Count - 1);
        Cards.RemoveAt(Cards.Count - 1);
        return card;
    }
}