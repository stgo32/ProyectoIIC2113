namespace RawDeal.Reversals;


using RawDeal.Plays;


public class MayReverseSomeDamageOrLess : Reversal
{
    protected PlayAs _playAs;
    protected Subtype _subtype;
    protected int _damageNeeded;

    public MayReverseSomeDamageOrLess(
        PlayAs playAs, Subtype subtype, int damageNeeded, string title, List<string> types, 
        List<string> subtypes, string fortitude, string damage, string stunValue, string cardEffect
    ) : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
        _playAs = playAs;
        _subtype = subtype;
        _damageNeeded = damageNeeded;
    }

    public override bool CanReverse(Card card, int fortitude, Player oponent)
    {
        bool fortitudeRestriction = CalculateFortitudeRestriction(
            fortitude,
            oponent.NextSubtypeReversalIsPlusF
        );
        bool subtypeRestriction = card.ContainsSubtype(_subtype) && 
                                  card.PlayAs == _playAs;
        bool damageRestriction = CalculateDamageRestriction(card, oponent);
        return fortitudeRestriction && subtypeRestriction && damageRestriction;
    }

    protected bool CalculateDamageRestriction(Card card, Player oponent)
    {
        int damage = card.GetDamage();
        damage += oponent.NextSubtypeIsPlusD;
        damage += oponent.DamageBonusForPlayedAfterSomeDamage;
        if (card.ContainsSubtype(oponent.DamageBonusForRestOfTurnSubtype))
        {
            damage += oponent.DamageBonusForRestOfTurn;
        }
        if (oponent.Oponent.Superstar.CanUseAbilityBeforeTakingDamage)
        {
            damage = oponent.Oponent.Superstar.TakeLessDamage(damage);
        }
        return damage <= _damageNeeded;
    }
}