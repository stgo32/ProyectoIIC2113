namespace RawDeal.Effects;


using RawDealView.Options;


public class JockeyingForPosition : Effect
{
    public override bool CantBeReversed { get { return false; } }
    
    public JockeyingForPosition(Player player) : base(player) { }

    public override void Resolve()
    {
        SelectedEffect selectedEffect = Formatter.View.AskUserToSelectAnEffectForJockeyForPosition(
            _player.Superstar.Name
        );
        Effect effect = new NoEffect(_player);
        if (selectedEffect == SelectedEffect.NextGrappleIsPlus4D)
        {
            effect = new NextSubtypePlayedIsPlusDamage(
                "Jockeying for Position", Subtype.Grapple, 4, _player
            );
        }
        else if (selectedEffect == SelectedEffect.NextGrapplesReversalIsPlus8F)
        {
            effect = new NextSubtypeReversalIsPlusFortitude(
                "Jockeying for Position", Subtype.Grapple, 8, _player
            );
        }
        effect.Resolve();
    }
}