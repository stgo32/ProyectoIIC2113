namespace RawDeal;

public abstract class Reversal : Card
{
    public Reversal(string title, List<string> types, List<string> subtypes, string fortitude,
                    string damage, string stunValue, string cardEffect)
                    : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public abstract bool CanReverse(Card card);

    public abstract void ReversalEffect(Card card);
}

