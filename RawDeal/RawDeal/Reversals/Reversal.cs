namespace RawDeal.Reversals;


using RawDeal.Plays;


public abstract class Reversal : Card
{
    private int _reversalId;
    public int ReversalId { get { return _reversalId; } set { _reversalId = value; } }
    
    public Reversal(string title, List<string> types, List<string> subtypes, string fortitude,
                    string damage, string stunValue, string cardEffect)
                    : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public abstract bool CanReverse(Card card, int fortitude);

    protected abstract void ReversalEffect(Play play);
    
    protected abstract void ApplyDamage(Play play);

    public void ReverseFromHand(Play play)
    {
        Player playerReversing = play.Player.Oponent;
        Player oponent = play.Player;
        
        oponent.Deck.DrawCardFromPossibleCardsToRingsideById(play.CardId);
        playerReversing.Deck.DrawCardFromPossibleReversalsToRingAreaById(ReversalId, play.Card, playerReversing.Fortitude);
        Formatter.ReverseACard(this, playerReversing);
        ReversalEffect(play);
        ApplyDamage(play);
        playerReversing.HasReversedACard = true;
        playerReversing.WantsToReverseACard = false;
        play.Reversed = true;
    }

    public void ReverseByDeck(Play play, int gapDamage)
    {
        Player playerReversing = play.Player.Oponent;
        Player oponent = play.Player;

        Formatter.View.SayThatCardWasReversedByDeck(playerReversing.Superstar.Name);
        oponent.DrawCardsBecauseOfStunValue(play.Card.GetStunValue(), gapDamage);
        playerReversing.HasReversedACard = true;
        play.Reversed = true;
    }

}

