namespace RawDeal.Reversals;


using RawDeal.Plays;


public class CleanBreak : Reversal
{
    public CleanBreak(string title, List<string> types, List<string> subtypes, string fortitude,
                      string damage, string stunValue, string cardEffect)
                      : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public override bool CanReverse(Card card, int fortitude, Player oponent)
    {
        bool fortitudeRestriction = CalculateFortitudeRestriction(
            fortitude,
            oponent.NextGrapplesReversalIsPlus8F
        );
        bool reversalRestriction = card.PlayAs == "Action" && card.Title == "Jockeying for Position";
        return fortitudeRestriction && reversalRestriction;
    }

    protected override void UseReversalEffect(Play play)
    {
        play.Player.DiscardCards(4);
        play.Player.Oponent.DrawACard();
    }

    protected override void ApplyDamage(Play play) { return; }
}