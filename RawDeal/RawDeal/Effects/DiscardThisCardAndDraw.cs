namespace RawDeal.Effects;


public class DiscardThisCardAndDraw : Effect
{
    private int _cardId;
    public override bool CantBeReversed { get { return false; } }
    
    public DiscardThisCardAndDraw(int cardId, Player player) : base(player)
    {
        _cardId = cardId;
    }

    public override void Resolve()
    {
        _player.Deck.DrawACard();
    }

    public override void TheCardHasBeenUsed(int cardId)
    {
        _player.Deck.DiscardPossibleCardById(cardId);
    }
}