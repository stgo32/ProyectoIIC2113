namespace RawDeal;


using RawDealView.Options;


public abstract class Reversal : Card
{
    private int _reversalId;
    public int ReversalId { get { return _reversalId; } set { _reversalId = value; } }
    public Reversal(string title, List<string> types, List<string> subtypes, string fortitude,
                    string damage, string stunValue, string cardEffect)
                    : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public abstract bool CanReverse(Card card, int fortitude);

    public abstract void ReversalEffect(Card card);

    public void ReverseFromHand(Play play)
    {
        Player playerReversing = play.Player.Oponent;
        Player oponent = play.Player;
        string superstarName = playerReversing.Superstar.Name;
        oponent.Deck.DrawCardFromPossibleCardsToRingsideById(play.CardId);
        playerReversing.Deck.DrawCardFromPossibleReversalsToRingAreaById(ReversalId, play.Card);
        string reversalInfo = Formatter.FormatCard(this, NextPlay.PlayCard);
        Formatter.View.SayThatPlayerReversedTheCard(superstarName, reversalInfo);
        play.Reversed = true;
    }

    public void ReverseByDeck(Play play, int gapDamage)
    {
        Player playerReversing = play.Player.Oponent;
        Formatter.View.SayThatCardWasReversedByDeck(playerReversing.Superstar.Name);
        play.Player.DrawCardsBecauseOfStunValue(play.Card.GetStunValue(), gapDamage);
        playerReversing.WantsToReverseACard = true;
        play.Reversed = true;
    }
}

