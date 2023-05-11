namespace RawDeal.Reversals;


public class NoChanceInHell : Reversal
{
    public NoChanceInHell(string title, List<string> types, List<string> subtypes, string fortitude,
                    string damage, string stunValue, string cardEffect)
                    : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public override bool CanReverse(Card card)
    {
        return card.PlayAs == "Action";
    }

    public override void ReversalEffect(Card card)
    {
        throw new NotImplementedException();
    }
}