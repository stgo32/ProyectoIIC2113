namespace RawDeal.Initialize;


using RawDeal.Preconditions;


public static class PreconditionFactory
{
    public static Precondition GetPrecondition(Card card)
    {
        Precondition precondition;
        if (card.Title == "Undertaker's Tombstone Piledriver")
        {
            precondition = new DoesNotRequireFortitudeWhenPlayedAsAction(card);
        }
        else if (card.Title == "Spit At Opponent")
        {
            precondition = new NeedsSomeNumberOfCardsInHand(2, card);
        }
        else
        {
            precondition = new StandardPrecondition(card);
        }
        return precondition;
    }
}