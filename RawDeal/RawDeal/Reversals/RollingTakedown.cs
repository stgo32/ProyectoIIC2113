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
            oponent.NextGrapplesReversalIsPlus8F
        );
        bool reversalRestriction = CalculateDamageRestriction(card, oponent);
        return fortitudeRestriction && reversalRestriction;
    }

    private bool CalculateDamageRestriction(Card card, Player oponent)
    {
        int damage = card.GetDamage();
        if (oponent.NextGrappleIsPlus4D)
        {
            damage += 4;
        }
        if (oponent.Oponent.Superstar.CanUseAbilityBeforeTakingDamage)
        {
            damage = oponent.Oponent.Superstar.TakeLessDamage(damage);
        }
        return card.ContainsSubtype("Grapple") && card.PlayAs == "Maneuver" && damage <= 7;
    }

    protected override void ReversalEffect(Play play) { return; }

    protected override void ApplyDamage(Play play)
    {
        int damage = play.Player.Oponent.HandleDamage(play.Card.GetDamage(), true);
        if (play.Player.NextGrappleIsPlus4D)
        {
            damage += 4;
        }
        DeliverDamage(play.Player, damage);
    }

    private void DeliverDamage(Player oponent, int damage)
    {
        if (damage > 0)
        {
            Formatter.View.SayThatOpponentWillTakeSomeDamage(oponent.Superstar.Name, damage);
        }
        for (int i = 0; i < damage; i++)
        {
            if (oponent.Arsenal.Count == 0)
            {
                break;
            }
            Card cardOverturned = OverTurnCard(oponent, i, damage);
        }
    }

    private Card OverTurnCard(Player oponent, int iter, int damage)
    {
        Card cardOverTurned = oponent.RecieveDamage();
        Formatter.PrintCardOverturned(cardOverTurned, iter+1, damage);
        return cardOverTurned;
    }
}