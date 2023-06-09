namespace RawDeal.Initialize;


using RawDeal.Preconditions;


public static class PreconditionFactory
{
    public static Precondition GetPrecondition(Card card)
    {
        Precondition precondition;
        precondition = new StandardPrecondition(card);
        return precondition;
    }
}