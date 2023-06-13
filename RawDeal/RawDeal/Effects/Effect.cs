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
}
