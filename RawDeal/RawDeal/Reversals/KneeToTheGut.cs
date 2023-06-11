namespace RawDeal.Reversals;


using RawDeal.Plays;


public class KneeToTheGut : Reversal
{
    public KneeToTheGut(string title, List<string> types, List<string> subtypes, string fortitude,
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
        // if (oponent.NextGrappleIsPlus4D)
        // {
        //     damage += 4;
        // }
        damage += oponent.NextSubtypeIsPlusD;
        if (oponent.Oponent.Superstar.CanUseAbilityBeforeTakingDamage)
        {
            damage = oponent.Oponent.Superstar.TakeLessDamage(damage);
        }
        bool damageRestriction = card.ContainsSubtype("Strike") && 
                                 card.PlayAs == PlayAs.Maneuver &&
                                 card.GetDamage() <= 7;
        return damageRestriction;
    }
    
    protected override void UseReversalEffect(Play play) { return; }

    protected override void ApplyDamage(Play play)
    {
        int damage = play.Player.Oponent.HandleDamage(play.Card.GetDamage(), true);
        DeliverDamage(play.Player, damage);
    }

    private void DeliverDamage(Player oponent, int damage)
    {
        if (damage > 0)
        {
            Formatter.View.SayThatSuperstarWillTakeSomeDamage(oponent.Superstar.Name, damage);
        }
        for (int i = 0; i < damage; i++)
        {
            if (oponent.Arsenal.IsEmpty())
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