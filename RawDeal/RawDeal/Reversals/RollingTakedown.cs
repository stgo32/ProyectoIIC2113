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
        bool canReverse = false;
        bool fortitudeRestriction = CalculateFortitudeRestriction(fortitude, oponent.NextGrapplesReversalIsPlus8F);
        bool reversalRestriction = CalculateDamageRestriction(card, oponent);
        if (fortitudeRestriction && reversalRestriction)
        {
            canReverse = true;
        }
        return canReverse;
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
        Console.WriteLine("Card: " + Title);
        Console.WriteLine("Card damage: " + card.GetDamage());
        Console.WriteLine("JFP damage: " + damage);
        Console.WriteLine("JFP +4D: " + oponent.NextGrappleIsPlus4D);
        return card.ContainsSubtype("Grapple") && card.PlayAs == "Maneuver" && damage <= 7;
    }

    protected override void ReversalEffect(Play play) { return; }

    protected override void ApplyDamage(Play play)
    {
        int damage = play.Player.Oponent.HandleDamage(play.Card.GetDamage(), true);
        Console.WriteLine("play.Player: " + play.Player.Superstar.Name);
        Console.WriteLine("play.Player.NextGrappleIsPlus4D: " + play.Player.NextGrappleIsPlus4D);
        Console.WriteLine("oponent: " + play.Player.Oponent.Superstar.Name);
        Console.WriteLine("oponent.NextGrappleIsPlus4D: " + play.Player.Oponent.NextGrappleIsPlus4D);
        if (play.Player.NextGrappleIsPlus4D)
        {
            damage += 4;
        }
        DeliverDamage(play.Player, damage);
    }

    private void DeliverDamage(Player oponent, int damage)
    {
        // Player oponent = Player.Oponent;
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
            // if (CanBeReversedByDeck(cardOverturned))
            // {
            //     Stop(cardOverturned, damage-i-1);
            //     break;
            // }
        }
    }

    private Card OverTurnCard(Player oponent, int iter, int damage)
    {
        Card cardOverTurned = oponent.RecieveDamage();
        Formatter.PrintCardOverturned(cardOverTurned, iter+1, damage);
        return cardOverTurned;
    }
}