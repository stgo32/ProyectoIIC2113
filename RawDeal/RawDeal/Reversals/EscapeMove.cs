namespace RawDeal.Reversals;


public class EscapeMove : Reversal
{
    public EscapeMove(string title, List<string> types, List<string> subtypes, string fortitude,
                    string damage, string stunValue, string cardEffect)
                    : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public override bool CanReverse(Card card)
    {
        return card.ContainsSubtype("Grapple") && card.PlayAs == "Maneuver";
    }

    public override void ReversalEffect(Card card)
    {
        throw new NotImplementedException();
    }
}