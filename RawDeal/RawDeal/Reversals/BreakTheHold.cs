namespace RawDeal.Reversals;


public class BreakTheHole : Reversal
{
    public BreakTheHole(string title, List<string> types, List<string> subtypes, string fortitude,
                    string damage, string stunValue, string cardEffect)
                    : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public override bool CanReverse(Card card, int fortitude)
    {
        bool canReverse = false;
        bool fortitudeRestriction = GetFortitude() <= fortitude;
        bool reversalRestriction = card.ContainsSubtype("Submission") && card.PlayAs == "Maneuver";
        if (fortitudeRestriction && reversalRestriction)
        {
            canReverse = true;
        }
        return canReverse;
    }

    public override void ReversalEffect(Card card)
    {
        throw new NotImplementedException();
    }
}