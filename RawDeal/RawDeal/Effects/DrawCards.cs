namespace RawDeal.Effects;


public class DrawCards : Effect
{
    private int _quantity;
    private PlayerTarget _target;

    private bool _mayChooseHowMany;

    public DrawCards(int quantity, PlayerTarget target, bool mayChooseHowMany, int cardId, Player player) 
                     : base(cardId, player) 
    { 
        _quantity = quantity;
        _target = target;
        _mayChooseHowMany = mayChooseHowMany;
    }


    public override void Resolve()
    {
        Player target = _player;
        int howManyToDraw = _quantity;
        if (_mayChooseHowMany)
        {
            howManyToDraw = Formatter.View.AskHowManyCardsToDrawBecauseOfACardEffect(
                _player.Superstar.Name,
                _quantity
            );
        }
        else if (_target == PlayerTarget.Oponent)
        {
            target = _player.Oponent;
        }
        DrawCardsAtTarget(target, howManyToDraw);
    }

    private void DrawCardsAtTarget(Player target, int quantity)
    {
        Formatter.View.SayThatPlayerDrawCards(target.Superstar.Name, quantity);
        for (int i = quantity; i > 0; i--)
        {
            target.Deck.DrawCardFromArsenalToHand();
        }
    }
}