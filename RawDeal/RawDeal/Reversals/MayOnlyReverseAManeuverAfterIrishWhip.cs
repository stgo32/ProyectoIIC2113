namespace RawDeal.Reversals;


using RawDeal.Plays;
using RawDeal.Initialize;
using RawDeal.Effects;


public class MayOnlyReverseAManeuverAfterIrishWhip : Reversal
{
    bool _canReverseByDeck;

    public MayOnlyReverseAManeuverAfterIrishWhip(
        bool CanReverseByDeck,
        string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect
    ) : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
        _canReverseByDeck = CanReverseByDeck;
    }

    public override bool CanReverse(Card card, int fortitude, Player oponent)
    {
        bool fortitudeRestriction = CalculateFortitudeRestriction(
            fortitude,
            oponent.NextSubtypeReversalIsPlusF
        );
        bool reversalRestriction = card.PlayAs == PlayAs.Maneuver && 
                                                  oponent.LastCardPlayed == "Irish Whip";
        Console.WriteLine($"ShoulderBlock.CanReverse: {fortitudeRestriction} && {reversalRestriction}");
        Console.WriteLine("Oponent last card played: " + oponent.LastCardPlayed);
        return fortitudeRestriction && reversalRestriction;
    }

    protected override void UseReversalEffect(Play play)
    {
        Effect effect = EffectFactory.GetEffect(this, -1, play.Player.Oponent);
        effect.Resolve();  
    }

    protected override void ApplyDamage(Play play)
    {
        int damage = play.Player.Oponent.HandleDamage(GetDamage());
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

    public override bool CanReverseByDeck(Card card, int fortitude, Player oponent)
    {
        if (_canReverseByDeck)
        {
            return base.CanReverseByDeck(card, fortitude, oponent);
        }
        else
        {
            return false;
        }
    }
}