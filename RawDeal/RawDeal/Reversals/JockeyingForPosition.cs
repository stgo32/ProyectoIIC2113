namespace RawDeal.Reversals;


using RawDeal.Plays;
using RawDealView.Options;


public class JockeyingForPosition : Reversal
{
    public JockeyingForPosition(string title, List<string> types, List<string> subtypes, string fortitude,
                      string damage, string stunValue, string cardEffect)
                      : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public override bool CanReverse(Card card, int fortitude, Player oponent)
    {
        bool fortitudeRestriction = CalculateFortitudeRestriction(fortitude, oponent.NextGrapplesReversalIsPlus8F);
        bool reversalRestriction = card.PlayAs == "Action"
                                   && card.Title == "Jockeying for Position";
        return fortitudeRestriction && reversalRestriction;
    }

    protected override void UseReversalEffect(Play play)
    {
        Player reversingPlayer = play.Player.Oponent;
        SelectedEffect selectedEffect = Formatter.View.AskUserToSelectAnEffectForJockeyForPosition(
            reversingPlayer.Superstar.Name
        );
        if (selectedEffect == SelectedEffect.NextGrappleIsPlus4D)
        {
            reversingPlayer.NextGrappleIsPlus4D = true;
        }
        else if (selectedEffect == SelectedEffect.NextGrapplesReversalIsPlus8F)
        {
            reversingPlayer.NextGrapplesReversalIsPlus8F = true;
        }
        reversingPlayer.PlayedJockeyingForPositionLast = true;
    }

    protected override void ApplyDamage(Play play) { return; }
}