namespace RawDeal.Effects;


public class NoEffect : Effect
{
    public override bool CantBeReversed { get { return false; } }
    
    public NoEffect(Player player) : base(player) { }

    public override void Resolve() { }
}