namespace RawDeal.Effects;


public class NextSubtypePlayedIsPlusDamage : Effect
{
    private string _cardTitle;
    private Subtype _subtype;
    private int _damage;

    public override bool CantBeReversed { get { return false; } }
    
    public NextSubtypePlayedIsPlusDamage(string cardTitle, Subtype subtype, int damage, Player player)
                                         : base(player) 
    {
        _cardTitle = cardTitle;
        _subtype = subtype;
        _damage = damage;
    }

    public override void Resolve()
    {
        _player.NextSubtypeIsPlusD = _damage;
        _player.NextSubtypeDoesSomeEffect = _subtype;
        _player.LastCardPlayedTitle = _cardTitle;
    }
}