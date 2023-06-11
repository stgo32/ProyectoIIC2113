namespace RawDeal.Effects;


public class MayNotBeReversed : Effect
{
    public override bool CantBeReversed { get { return true; } }
    
    public MayNotBeReversed(Player player) : base(player) { }

    public override void Resolve() { }
}