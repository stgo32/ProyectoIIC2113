namespace RawDeal;

public abstract class Play
{
    protected int _cardId;
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

    public virtual void Start()
    {
        Formatter.View.SayThatPlayerSuccessfullyPlayedACard();
    }

    public abstract void Stop();
}