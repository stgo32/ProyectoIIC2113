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
        else if (card.Title == "Lionsault")
        {
            precondition = new NeedsSomeDamageInflictedBefore(4, card);
        }
        else if (card.Title == "Austin Elbow Smash")
        {
            precondition = new NeedsSomeDamageInflictedBefore(5, card);
        }
        else
        {
            precondition = new StandardPrecondition(card);
        }
        return precondition;
    }
}