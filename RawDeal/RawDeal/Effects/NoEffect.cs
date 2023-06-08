namespace RawDeal.Effects;


public class NoEffect : Effect
{
    public NoEffect(int cardId, Player player) : base(cardId, player) { }

    public override void Resolve() { }
}