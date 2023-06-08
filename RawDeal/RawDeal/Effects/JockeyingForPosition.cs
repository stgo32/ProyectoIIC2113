namespace RawDeal.Effects;


// using RawDeal;
using RawDealView.Options;


public class JockeyingForPosition : Effect
{
    public JockeyingForPosition(int cardId, Player player) : base(cardId, player) { }

    public override void Resolve()
    {
        _player.Deck.DrawCardFromPossibleCardsToRingAreaById(_cardId);
        SelectedEffect selectedEffect = Formatter.View.AskUserToSelectAnEffectForJockeyForPosition(
            _player.Superstar.Name
        );
        if (selectedEffect == SelectedEffect.NextGrappleIsPlus4D)
        {
            NextGrappleIsPlus4D();
        }
        else if (selectedEffect == SelectedEffect.NextGrapplesReversalIsPlus8F)
        {
            NextGrapplesReversalIsPlus8F();
        }
        _player.PlayedJockeyingForPositionLast = true;
    }

    private void NextGrappleIsPlus4D()
    {
        _player.NextGrappleIsPlus4D = true;
    }

    private void NextGrapplesReversalIsPlus8F()
    {
        _player.NextGrapplesReversalIsPlus8F = true;
    }
}