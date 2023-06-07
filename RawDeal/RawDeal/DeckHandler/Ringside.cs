namespace RawDeal.DeckHandler;


public class Ringside : CardSet
{

    public override void AddCard(Card card)
    {
        Cards.Add(card);
    }

    public override Card RemoveCard()
    {
        Card card = GetCard(0);
        Cards.RemoveAt(0);
        return card;
    }
}