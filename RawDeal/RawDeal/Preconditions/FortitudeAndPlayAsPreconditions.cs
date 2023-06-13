namespace RawDeal.Preconditions;



public class FortitudeAndPlayAsPreconditions : Precondition
{
    public FortitudeAndPlayAsPreconditions(Card card) : base(card) { }

    public override bool IsPossibleToPlay(Player player)
    {
        return FortitudePrecodition(player.Fortitude) && PlayAsPrecondition();
    }
}