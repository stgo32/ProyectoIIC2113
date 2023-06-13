namespace RawDeal.Effects;


public class DiscardCards : Effect
{
    private int _quantity;

    private PlayerTarget _target;

    private bool _mayChooseHowMany;
    
    public override bool CantBeReversed { get { return false; } }

    public DiscardCards(int quantity, PlayerTarget target, Player player) 
                        : base(player) 
    { 
        _quantity = quantity;
        _target = target;
    }

    public override void Resolve()
    {
        if (_target == PlayerTarget.Self)
        {
            _player.Deck.DiscardCards(_quantity);
        }
        else if (_target == PlayerTarget.Oponent)
        {
            _player.Oponent.Deck.DiscardCards(_quantity);
        }
        else if (_target == PlayerTarget.Both)
        {
            _player.Deck.DiscardCards(_quantity);
            _player.Deck.DiscardCardsFromOponentHand(_quantity);
        }
    }
}