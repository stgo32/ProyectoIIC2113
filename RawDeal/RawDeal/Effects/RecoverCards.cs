namespace RawDeal.Effects;


public class RecoverCards : Effect
{
    public override bool CantBeReversed { get { return false; } }
    
    private int _quantity;

    public RecoverCards(int quantity, int cardId, Player player) 
                        : base(cardId, player) 
    { 
        _quantity = quantity;
    }

    public override void Resolve()
    {
        _player.RecoverCards(_quantity);
    }
}