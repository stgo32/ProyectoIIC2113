namespace RawDeal.Reversals;


using RawDeal.Plays;


public class ChynaInterferes : Reversal
{
    public ChynaInterferes(string title, List<string> types, List<string> subtypes, string fortitude,
                    string damage, string stunValue, string cardEffect)
                    : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public override bool CanReverse(Card card, int fortitude, Player oponent)
    {
        bool fortitudeRestriction = CalculateFortitudeRestriction(
            fortitude,
            oponent.NextSubtypeReversalIsPlusF
        );
        bool reversalRestriction = card.PlayAs == PlayAs.Maneuver;
        return fortitudeRestriction && reversalRestriction;
    }
}