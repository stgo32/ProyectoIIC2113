namespace RawDeal.Reversals;


using RawDeal.Plays;


public class StepAside : Reversal
{
    public StepAside(string title, List<string> types, List<string> subtypes, string fortitude,
                    string damage, string stunValue, string cardEffect)
                    : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public override bool CanReverse(Card card, int fortitude, Player oponent)
    {
        bool canReverse = false;
        bool fortitudeRestriction = CalculateFortitudeRestriction(fortitude, oponent.NextGrapplesReversalIsPlus8F);
        bool reversalRestriction = card.ContainsSubtype("Strike") && card.PlayAs == "Maneuver";
        if (fortitudeRestriction && reversalRestriction)
        {
            canReverse = true;
        }
        return canReverse;
    }

    protected override void ReversalEffect(Play play) { return; }

    protected override void ApplyDamage(Play play) { return; }
}