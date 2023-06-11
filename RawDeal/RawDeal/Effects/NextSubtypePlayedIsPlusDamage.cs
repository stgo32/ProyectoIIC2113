namespace RawDeal.Effects;


// using RawDeal;
using RawDealView.Options;


public class NextSubtypePlayedIsPlusDamage : Effect
{
    private Card _card;
    private Subtype _subtype;
    private int _damage;

    public override bool CantBeReversed { get { return false; } }
    
    public NextSubtypePlayedIsPlusDamage(Card card, Subtype subtype, int damage, Player player)
                                               : base(player) 
    {
        _card = card;
        _subtype = subtype;
        _damage = damage;
    }

    public override void Resolve()
    {
        _player.NextSubtypeIsPlusD = _damage;
        _player.NextSubtypeDoesSomeEffect = _subtype;
        _player.LastCardPlayed = _card.Title;
    }
}