namespace RawDeal.Effects;


public class NextManeuverPlayedIsPlusDamage : Effect
{
    private Subtype _subtype;
    private int _damage;

    public override bool CantBeReversed { get { return false; } }
    
    public NextManeuverPlayedIsPlusDamage(int damage, Subtype subtype, Player player)
                                         : base(player) 
    {
        _damage = damage;
        _subtype = subtype;
    }

    public override void Resolve()
    {
        _player.NextManeuverIsPlusD = _damage;
    }

    public override Subtype GetSubtypeDoesSomeEffect()
    {
        return _subtype;
    }
}