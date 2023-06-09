namespace RawDeal.Preconditions;



public class DoesNotRequireFortitudeWhenPlayedAsAction : Precondition
{
    public DoesNotRequireFortitudeWhenPlayedAsAction(Card card) : base(card) { }

    public override bool IsPossibleToPlay(int fortitude)
    {
        return fortitudePrecodition(fortitude) && PlayAsPrecondition();
    }

    protected override bool fortitudePrecodition(int fortitude)
    {
        if (_card.PlayAs == Plays.PlayAs.Action)
        {
            return true;
        }
        else
        {
            return base.fortitudePrecodition(fortitude);
        }
    }
}