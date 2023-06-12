namespace RawDeal.Preconditions;



public class DoesNotRequireFortitudeWhenPlayedAsAction : Precondition
{
    public DoesNotRequireFortitudeWhenPlayedAsAction(Card card) : base(card) { }

    public override bool IsPossibleToPlay(Player player)
    {
        return FortitudePrecodition(player.Fortitude) && PlayAsPrecondition();
    }

    protected override bool FortitudePrecodition(int fortitude)
    {
        if (_card.PlayAs == Plays.PlayAs.Action)
        {
            return true;
        }
        else
        {
            return base.FortitudePrecodition(fortitude);
        }
    }
}