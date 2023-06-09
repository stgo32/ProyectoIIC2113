namespace RawDeal.Preconditions;



public class StandardPrecondition : Precondition
{
    public StandardPrecondition(Card card) : base(card) { }

    public override bool IsPossibleToPlay(int fortitude)
    {
        return fortitudePrecodition(fortitude) && PlayAsPrecondition();
    }
}