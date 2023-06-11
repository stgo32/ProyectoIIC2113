namespace RawDeal.Effects;


public class StandardActionEffect : Effect
{
    private int _cardId;
    public override bool CantBeReversed { get { return false; } }
    
    public StandardActionEffect(int cardId, Player player) : base(player)
    {
        _cardId = cardId;
    }

    public override void Resolve()
    {
        _player.DiscardPossibleCardById(_cardId);
        _player.DrawACard();
    }
}