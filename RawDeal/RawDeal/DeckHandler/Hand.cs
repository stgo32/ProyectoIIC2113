namespace RawDeal.DeckHandler;


public class Hand : CardSet
{
    public int FindCardIdByCountInPossibleCardsToPlay(int cardCount, Card card)
    {
        int idCardAtHand = 0;
        for (int i = 0; i < Count(); i++)
        {
            if (GetCard(i).Title != card.Title)
            {
                continue;
            }
            if (cardCount == 0)
            {
                idCardAtHand = i;
                break;
            }
            cardCount--;
        }
        return idCardAtHand;
    }
}