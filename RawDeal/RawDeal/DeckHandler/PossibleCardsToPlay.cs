namespace RawDeal.DeckHandler;


public class PossibleCardsToPlay : CardSet
{
    public PossibleCardsToPlay Get(int fortitude, Hand hand)
    {
        Clear();
        foreach (Card card in hand.Cards)
        {
            foreach (string type in card.Types)
            {
                Card cardToAdd = card.PlayCardAs(type);
                AddCardInCaseIsPossible(cardToAdd, fortitude);
            }
        }
        return this;
    }

    private void AddCardInCaseIsPossible(Card card, int fortitude)
    {
        if (card.IsPossibleToPlay(fortitude))
        {
            AddCard(card);
        }
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