namespace RawDeal.Effects;


public class StandardActionEffect : Effect
{
    public override bool CantBeReversed { get { return false; } }
    
    public StandardActionEffect(int cardId, Player player) : base(cardId, player) { }

    public override void Resolve()
    {
        _player.DiscardPossibleCardById(_cardId);
        _player.DrawACard();
    }
}