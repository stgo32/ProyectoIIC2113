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
        bool canReverse = false;
        Console.WriteLine("This: " + oponent.Oponent.NextGrappleIsPlus4D);
        Console.WriteLine("Oponent: " + oponent.NextGrappleIsPlus4D);
        bool fortitudeRestriction = CalculateFortitudeRestriction(fortitude, oponent.NextGrapplesReversalIsPlus8F);
        bool reversalRestriction = CalculateDamageRestriction(card, oponent.NextGrappleIsPlus4D);
        if (fortitudeRestriction && reversalRestriction)
        {
            canReverse = true;
        }
        return canReverse;
    }

    private bool CalculateDamageRestriction(Card card, bool nextGrappleIsPlus4D)
    {
        int damage = card.GetDamage();
        if (nextGrappleIsPlus4D)
        {
            damage += 4;
        }
        Console.WriteLine("Card: " + Title);
        Console.WriteLine("Card damage: " + card.GetDamage());
        Console.WriteLine("JFP damage: " + damage);
        Console.WriteLine("JFP +4D: " + nextGrappleIsPlus4D);
        return card.PlayAs == "Maneuver" && damage <= 7;
    }

    protected override void ReversalEffect(Play play) { return; }

    protected override void ApplyDamage(Play play)
    {
        int damage = play.Player.Oponent.HandleDamage(GetDamage());
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