namespace RawDeal.Effects;


public class StandardActionEffect : Effect
{
    public StandardActionEffect(int cardId, Player player) : base(cardId, player) { }

    public override void Resolve()
    {
        _player.DiscardPossibleCardById(_cardId);
        _player.DrawACard();
    }
}