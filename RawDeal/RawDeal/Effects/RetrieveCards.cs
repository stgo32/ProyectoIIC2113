namespace RawDeal.Effects;


public class RetrieveCards : Effect
{
    private int _quantity;
    
    public override bool CantBeReversed { get { return false; } }

    public RetrieveCards(int quantity, Player player) : base(player) 
    { 
        _quantity = quantity;
    }

    public override void Resolve()
    {
        _player.RetrieveCards(_quantity);
    }
}