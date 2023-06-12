namespace RawDeal.Reversals;


using RawDeal.Plays;


public class RollingTakedown : Reversal
{
    public RollingTakedown(string title, List<string> types, List<string> subtypes, string fortitude,
                    string damage, string stunValue, string cardEffect)
                    : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public override bool CanReverse(Card card, int fortitude, Player oponent)
    {
        bool fortitudeRestriction = CalculateFortitudeRestriction(
            fortitude,
            oponent.NextSubtypeReversalIsPlusF
        );
        bool reversalRestriction = CalculateDamageRestriction(card, oponent);
        return fortitudeRestriction && reversalRestriction;
    }

    private bool CalculateDamageRestriction(Card card, Player oponent)
    {
        int damage = card.GetDamage();
        damage += oponent.NextSubtypeIsPlusD;
        if (card.ContainsSubtype(oponent.DamageBonusForRestOfTurnSubtype.ToString()) || 
            oponent.DamageBonusForRestOfTurnSubtype == Subtype.All)
        {
            damage += oponent.DamageBonusForRestOfTurn;
        }
        if (oponent.Oponent.Superstar.CanUseAbilityBeforeTakingDamage)
        {
            damage = oponent.Oponent.Superstar.TakeLessDamage(damage);
        }
        return card.ContainsSubtype("Grapple") && card.PlayAs == PlayAs.Maneuver && damage <= 7;
    }

    protected override void ApplyDamage(Play play)
    {
        int damage = play.Player.Oponent.HandleDamage(play.Card.GetDamage(), true);
        if (play.Card.ContainsSubtype(play.Player.DamageBonusForRestOfTurnSubtype.ToString()) || 
            play.Player.DamageBonusForRestOfTurnSubtype == Subtype.All)
        {
            damage += play.Player.DamageBonusForRestOfTurn;
        }
        damage += play.Player.NextSubtypeIsPlusD;
        DeliverDamage(play.Player, damage);
    }
}