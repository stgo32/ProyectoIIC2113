namespace RawDeal.Initialize;


using RawDeal.Preconditions;


public static class PreconditionFactory
{
    public static Precondition GetPrecondition(Card card)
    {
        Precondition precondition;
        if (card.Title == "Undertaker's Tombstone Piledriver")
        {
            return new DoesNotRequireFortitudeWhenPlayedAsAction(card);
        }
        else
        {
            return new StandardPrecondition(card);
        }
        return precondition;
    }
}