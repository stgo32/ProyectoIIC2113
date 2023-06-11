namespace RawDeal.Effects;


public class DiscardCards : Effect
{
    private int _quantity;

    private PlayerTarget _target;
    
    public override bool CantBeReversed { get { return false; } }

    public DiscardCards(int quantity, PlayerTarget target, int cardId, Player player) 
                        : base(cardId, player) 
    { 
        _quantity = quantity;
        _target = target;
    }

    public override void Resolve()
    {
        if (_target == PlayerTarget.Self)
        {
            _player.DiscardCards(_quantity);
        }
        else if (_target == PlayerTarget.Oponent)
        {
            _player.Oponent.DiscardCards(_quantity);
        }
        else if (_target == PlayerTarget.Both)
        {
            _player.DiscardCards(_quantity);
            _player.DiscardCardsFromOponentHand(_quantity);
        }
    }
}