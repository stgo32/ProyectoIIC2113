namespace RawDeal.Effects;


public abstract class Effect
{
    protected int _cardId;

    protected Player _player;

    public abstract bool CantBeReversed { get; }

    public Effect(int cardId, Player player)
    {
        _cardId = cardId;
        _player = player;
    }

    public abstract void Resolve();
}
