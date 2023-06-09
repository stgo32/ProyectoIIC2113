namespace RawDeal.Reversals;


using RawDeal.Plays;


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
                                   oponent.LastCardPlayedTitle == "Irish Whip";
        return fortitudeRestriction && reversalRestriction;
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