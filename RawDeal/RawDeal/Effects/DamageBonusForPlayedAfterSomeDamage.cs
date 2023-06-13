namespace RawDeal.Effects;


public class DamageBonusForPlayedAfterSomeDamage : Effect
{
    private int _damage;

    public override bool CantBeReversed { get { return false; } }
    
    public DamageBonusForPlayedAfterSomeDamage(int damage, Player player) : base(player) 
    {
        _damage = damage;
    }

    public override void Resolve()
    {
        if (_player.LastDamageInflicted >= _damage)
        {
            _player.DamageBonusForPlayedAfterSomeDamage = _damage;
        }
    }
}