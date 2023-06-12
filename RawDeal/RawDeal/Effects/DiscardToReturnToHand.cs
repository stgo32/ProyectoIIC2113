namespace RawDeal.Effects;


public class DiscardToRetrieveSomeNumberOfCards : Effect
{
    private int _quantity;
    public override bool CantBeReversed { get { return false; } }

    public DiscardToRetrieveSomeNumberOfCards(int quantity, Player player) : base(player) 
    {
        _quantity = quantity;
    }

    public override void Resolve()
    {
        CheckNumberOfCardinHand();
        int quantity = Formatter.View.AskHowManyCardsToDiscard(
            _player.Superstar.Name,
            _quantity
        );
        Effect discard = new DiscardCards(quantity, PlayerTarget.Self, _player);
        Effect retrieve = new RetrieveCards(quantity, _player);
        discard.Resolve();
        retrieve.Resolve();
    }

    private void CheckNumberOfCardinHand()
    {
        if (_player.Hand.Count() < _quantity)
        {
            _quantity = _player.Hand.Count();
        }
    }
}