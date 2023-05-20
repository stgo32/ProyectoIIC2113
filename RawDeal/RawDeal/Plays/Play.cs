namespace RawDeal.Plays;


using RawDeal.Reversals;


public abstract class Play
{
    protected int _cardId;
    public int CardId { get { return _cardId; } set { _cardId = value; } }

    private Card _card;
    public Card Card { get { return _card; } set { _card = value; } }

    public string PlayAs { get { return Card.PlayAs; } }

    private Player _player;
    public Player Player { get { return _player; } set { _player = value; } }

    private Play _previousPlay;
    public Play PreviousPlay { get { return _previousPlay; } set { _previousPlay = value; } }

    private Play _nextPlay;
    public Play NextPlay { get { return _nextPlay; } set { _nextPlay = value; } }

    private bool _reversed = false;
    public bool Reversed { get { return _reversed; } set { _reversed = value; } }

    public Play(int cardId, Player player)
    {
        _cardId = cardId;
        Card = player.Deck.GetPossibleCardsToPlay()[cardId];
        Player = player;
    }

    public abstract void Start();

    protected virtual void Attack()
    {
        Formatter.View.SayThatPlayerSuccessfullyPlayedACard();
    }

    public bool IsBeingReversedByHand()
    {
        bool willBeReversed = false;
        int reversalSelectedId = -1;
        if (Player.Oponent.Deck.CanReverseCard(Card, Player.Oponent.Fortitude))
        {
            reversalSelectedId = Player.Oponent.SelectReversal(Card);
        }
        if (Player.Oponent.WantsToReverseACard)
        {
            Reversal reversal = Player.Oponent.Deck.GetReversalById(reversalSelectedId, Card, Player.Oponent.Fortitude);
            reversal.ReverseFromHand(this);
            willBeReversed = true;
        }
        return willBeReversed;
    }
}