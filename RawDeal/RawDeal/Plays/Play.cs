namespace RawDeal.Plays;


using RawDeal.Reversals;
using RawDeal.Initialize;
using RawDeal.Effects;


public abstract class Play
{
    protected int _cardId;
    public int CardId { get { return _cardId; } set { _cardId = value; } }

    private Card _card;
    public Card Card { get { return _card; } set { _card = value; } }

    private Player _player;
    public Player Player { get { return _player; } set { _player = value; } }

    protected bool _mayNotBeReversedEffect = false;

    public Play(int cardId, Player player)
    {
        _cardId = cardId;
        Card = player.Deck.GetPossibleCardsToPlay().GetCard(cardId);
        Player = player;
        _mayNotBeReversedEffect = CheckMayNotBeReversedEffect();
    }

    public void Start()
    {
        Formatter.PlayCard(Card, Player);
        if (!IsBeingReversedByHand())
        {
            SuccessfullyPlayed();
        }
    }

    protected virtual void SuccessfullyPlayed()
    {
        Formatter.View.SayThatPlayerSuccessfullyPlayedACard();
    }
    protected abstract void UseEffect();

    protected bool IsBeingReversedByHand()
    {
        Player reversingPlayer = Player.Oponent;
        bool willBeReversed = false;
        if (!_mayNotBeReversedEffect)
        {
            int reversalSelectedId = SelectReversal(reversingPlayer);
            if (Player.Oponent.WantsToReverseACard)
            {
                ReverseFromHand(reversalSelectedId, reversingPlayer);
                willBeReversed = true;
            }
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
        Reversal reversal = reversingPlayer.Deck.GetReversalById(reversalSelectedId);
        reversal.ReverseFromHand(this);
    }

    protected bool CheckMayNotBeReversedEffect()
    {
        Effect effect = EffectFactory.GetEffect(_cardId, Player);
        return effect.CantBeReversed;
    }
}