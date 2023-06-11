namespace RawDeal.Effects;


public class MayNotBeReversed : Effect
{
    public override bool CantBeReversed { get { return true; } }
    
    public MayNotBeReversed(int cardId, Player player) : base(cardId, player) { }

    public override void Resolve() { }
}