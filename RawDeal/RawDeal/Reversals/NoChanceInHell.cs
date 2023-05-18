namespace RawDeal.Reversals;


using RawDeal.Plays;


public class NoChanceInHell : Reversal
{
    public NoChanceInHell(string title, List<string> types, List<string> subtypes, string fortitude,
                    string damage, string stunValue, string cardEffect)
                    : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public override bool CanReverse(Card card, int fortitude)
    {
        bool canReverse = false;
        bool fortitudeRestriction = GetFortitude() <= fortitude;
        bool reversalRestriction = card.PlayAs == "Action";
        if (fortitudeRestriction && reversalRestriction)
        {
            canReverse = true;
        }
        return canReverse;
    }

    public override void ReversalEffect(Play play) { return; }
}