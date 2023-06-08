namespace RawDeal.DeckHandler;


using RawDeal.Initialize;
using RawDeal.Reversals;

public class PossibleReversalsToPlay : CardSet
{
    private Player _player;

    public PossibleReversalsToPlay(Player player)
    {
        _player = player;
    }

    public Reversal GetCardAsReversal(int index)
    {
        Card card = GetCard(index);
        Reversal reversal = ReversalFactory.GetReversal(card);
        reversal.ReversalId = index;
        return reversal;
    }

    public PossibleReversalsToPlay Get(int fortitude,  Card oponentCard)
    {
        Clear();
        foreach (Card card in _player.Hand.Cards)
        {
            if (card.Types.Contains("Reversal"))
            {
                bool isReversalPossibleToPlay = CheckReversalIsPossibleToPlay(card, fortitude, oponentCard);
                AddPossibleReversal(card, isReversalPossibleToPlay);
            }
        }
        return this;
    }

    private void AddPossibleReversal(Card card, bool isReversalPossibleToPlay)
    {
        if (isReversalPossibleToPlay)
        {
            Reversal reversal = ReversalFactory.GetReversal(card);
            AddCard(reversal);
        }
    }

    private bool CheckReversalIsPossibleToPlay(Card card, int fortitude, Card oponentCard)
    {
        Reversal reversal;
        try {
            reversal = ReversalFactory.GetReversal(card);
        }
        catch (Exception e)
        {
            return false;
        }
        if (reversal.CanReverse(oponentCard, fortitude, _player.Oponent))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int CountCardAppearances(int cardId, Card oponentCard, int fortitude)
    {
        Get(fortitude, oponentCard);
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