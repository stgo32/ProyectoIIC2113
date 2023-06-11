namespace RawDeal.Effects;


using RawDealView.Options;

public class ChooseBetweenDrawOrForcingOpponentToDiscard : Effect
{    
    private int _quantity;
    private bool _mayChooseHowMany;

    public override bool CantBeReversed { get { return false; } }

    public ChooseBetweenDrawOrForcingOpponentToDiscard(int quantity, bool mayChooseHowMany,
                                                       Player player) : base(player) 
    {
        _quantity = quantity;
        _mayChooseHowMany = mayChooseHowMany;
    }

    public override void Resolve()
    {
        SelectedEffect selected_effect = Formatter.View.AskUserToChooseBetweenDrawingOrForcingOpponentToDiscardCards(
            _player.Superstar.Name
        );
        Effect effect = new NoEffect(_player);
        if (selected_effect == SelectedEffect.DrawCards)
        {
            effect = new DrawCards(_quantity, PlayerTarget.Self, _mayChooseHowMany, _player);
        }
        else if (selected_effect == SelectedEffect.ForceOpponentToDiscard)
        {
            effect = new DiscardCards(_quantity, PlayerTarget.Oponent, _player);
        }
        effect.Resolve();
    }
}