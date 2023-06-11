namespace RawDeal.Effects;


public class DamageBonusForRestOfTurn : Effect
{
    private int _damage;

    public override bool CantBeReversed { get { return false; } }
    
    public DamageBonusForRestOfTurn(int damage, Player player) : base(player) 
    {
        _damage = damage;
    }

    public override void Resolve()
    {
        _player.DamageBonusForRestOfTurn = _damage;
    }
}