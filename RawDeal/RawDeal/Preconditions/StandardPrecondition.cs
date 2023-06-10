namespace RawDeal.Preconditions;



public class StandardPrecondition : Precondition
{
    public StandardPrecondition(Card card) : base(card) { }

    public override bool IsPossibleToPlay(Player player)
    {
        return fortitudePrecodition(player.Fortitude) && PlayAsPrecondition();
    }
}