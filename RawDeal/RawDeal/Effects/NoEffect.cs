namespace RawDeal.Effects;


public class NoEffect : Effect
{
    public override bool CantBeReversed { get { return false; } }
    
    public NoEffect(int cardId, Player player) : base(cardId, player) { }

    public override void Resolve() { }
}