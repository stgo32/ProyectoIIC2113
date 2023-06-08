namespace RawDeal.DeckHandler;


public class PossibleCardsToPlay : CardSet
{
    public override void AddCard(Card card)
    {
        foreach (string type in card.Types)
        {
            if (type == "Action" || type == "Maneuver")
            {
                Card cardToAdd = card.PlayCardAs(type);
                Cards.Add(cardToAdd);   
            }
        }
    }

    public PossibleCardsToPlay Get(int fortitude, Hand hand)
    {
        Clear();
        foreach (Card card in hand.Cards)
        {
            if (card.IsPossibleToPlay(fortitude))
            {
                AddCard(card);
            }
        }
        return this;
    }

    public int CountCardAppearances(int cardId)
    {
        Card card = GetCard(cardId);
        int cardCount = 0;
        int hibridCounter = 0;
        for (int i = 0; i < cardId; i++)
        {
            Card possibleCard = GetCard(i);
            if (possibleCard.Title == card.Title)
            {
                ManageCountersOptionsForCardAppearances(
                    possibleCard, ref cardCount, ref hibridCounter
                );
            }
        }
        return cardCount;
    }
}