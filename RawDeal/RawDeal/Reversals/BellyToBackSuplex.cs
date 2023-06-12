namespace RawDeal.Reversals;


using RawDeal.Plays;


public class BellyToBackSuplex : Reversal
{
    public BellyToBackSuplex(string title, List<string> types, List<string> subtypes, string fortitude,
                      string damage, string stunValue, string cardEffect)
                      : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public override bool CanReverse(Card card, int fortitude, Player oponent)
    {
        bool fortitudeRestriction = CalculateFortitudeRestriction(
            fortitude,
            oponent.NextSubtypeReversalIsPlusF
        );
        bool reversalRestriction = card.PlayAs == PlayAs.Maneuver && 
                                                  card.Title == Title;
        return fortitudeRestriction && reversalRestriction;
    }

    protected override void UseReversalEffect(Play play) { return; }

    protected override void ApplyDamage(Play play) { return; }
}