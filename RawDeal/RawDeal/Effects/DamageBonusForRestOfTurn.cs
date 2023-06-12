namespace RawDeal.Effects;


public class DamageBonusForRestOfTurn : Effect
{
    private int _damage;

    private Subtype _subtype;

    public override bool CantBeReversed { get { return false; } }
    
    public DamageBonusForRestOfTurn(int damage, Subtype subtype, Player player) : base(player) 
    {
        _damage = damage;
        _subtype = subtype;
    }

    public override void Resolve()
    {
        _player.DamageBonusForRestOfTurn += _damage;
        _player.DamageBonusForRestOfTurnSubtype = _subtype;
    }
}