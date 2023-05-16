namespace RawDeal.Reversals;


public class BreakTheHole : Reversal
{
    public BreakTheHole(string title, List<string> types, List<string> subtypes, string fortitude,
                    string damage, string stunValue, string cardEffect)
                    : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public override bool CanReverse(Card card)
    {
        return card.ContainsSubtype("Submission") && card.PlayAs == "Maneuver";
    }

    public override void ReversalEffect(Card card)
    {
        throw new NotImplementedException();
    }
}