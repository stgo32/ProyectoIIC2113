namespace RawDeal.Effects;


public abstract class Effect
{
    protected Player _player;

    public abstract bool CantBeReversed { get; }

    public Effect(Player player)
    {
        _player = player;
    }

    public abstract void Resolve();

    public virtual Subtype GetSubtypeDoesSomeEffect()
    {
        return Subtype.None;
    }

    public virtual void TheCardHasBeenUsed(int cardId)
    {
        _player.Deck.DrawCardFromPossibleCardsToRingAreaById(cardId);
    }

    public virtual void AfterEffectConfig()
    {
        _player.NextManeuverIsPlusDCounter = 0;
    }
}
