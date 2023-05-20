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
        Player reversingPlayer = Player.Oponent;
        bool willBeReversed = false;
        int reversalSelectedId = SelectReversal(reversingPlayer);
        if (Player.Oponent.WantsToReverseACard)
        {
            ReverseFromHand(reversalSelectedId, reversingPlayer);
            willBeReversed = true;
        }
        return willBeReversed;
    }

    private int SelectReversal(Player reversingPlayer)
    {
        int reversalSelectedId = -1;
        int fortitude = Player.Oponent.Fortitude;
        if (reversingPlayer.Deck.CanReverseCard(Card, fortitude))
        {
            reversalSelectedId = reversingPlayer.SelectReversal(Card);
        }
        return reversalSelectedId;
    }

    private void ReverseFromHand(int reversalSelectedId, Player reversingPlayer)
    {
        Reversal reversal = reversingPlayer.Deck.GetReversalById(
            reversalSelectedId,
            Card,
            reversingPlayer.Fortitude
        );
        reversal.ReverseFromHand(this);
    }
}