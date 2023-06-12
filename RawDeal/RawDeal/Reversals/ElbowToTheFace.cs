namespace RawDeal.Reversals;


using RawDeal.Plays;


public class ElbowToTheFace : Reversal
{
    public ElbowToTheFace(string title, List<string> types, List<string> subtypes, string fortitude,
                    string damage, string stunValue, string cardEffect)
                    : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public override bool CanReverse(Card card, int fortitude, Player oponent)
    {
        bool fortitudeRestriction = CalculateFortitudeRestriction(
            fortitude,
            oponent.NextSubtypeReversalIsPlusF
        );
        Console.WriteLine("Fortitude restriction: " + fortitudeRestriction);
        bool reversalRestriction = CalculateDamageRestriction(card, oponent);
        Console.WriteLine("Reversal restriction: " + reversalRestriction);
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
        Console.WriteLine("Card: " + Title);
        Console.WriteLine("Damage: " + damage);
        return card.PlayAs == PlayAs.Maneuver && damage <= 7;
    }

    protected override void ApplyDamage(Play play)
    {
        int damage = play.Player.Oponent.HandleDamage(GetDamage());
        DeliverDamage(play.Player, damage);
    }
}