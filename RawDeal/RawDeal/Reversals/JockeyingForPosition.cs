namespace RawDeal.Reversals;


using RawDeal.Plays;
using RawDeal.Initialize;
using RawDeal.Effects;


public class JockeyingForPosition : Reversal
{
    public JockeyingForPosition(string title, List<string> types, List<string> subtypes, string fortitude,
                      string damage, string stunValue, string cardEffect)
                      : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public override bool CanReverse(Card card, int fortitude, Player oponent)
    {
        bool fortitudeRestriction = CalculateFortitudeRestriction(
            fortitude,
            oponent.NextSubtypeReversalIsPlusF
        );
        bool reversalRestriction = card.PlayAs == PlayAs.Action
                                   && card.Title == Title;
        return fortitudeRestriction && reversalRestriction;
    }
}