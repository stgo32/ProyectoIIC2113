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

    public abstract bool CanReverse(Card card, int fortitude, Player oponent);

    protected bool CalculateFortitudeRestriction(int oponentFortitude, bool nextGrapplesReversalIsPlus8F)
    {
        int fortitude = GetFortitude();
        if (nextGrapplesReversalIsPlus8F)
        {
            fortitude += 8; 
        }
        return fortitude <= oponentFortitude;
    }

    protected abstract void ReversalEffect(Play play);
    
    protected abstract void ApplyDamage(Play play);

    public void ReverseFromHand(Play play)
    {
        Player playerReversing = play.Player.Oponent;
        Player oponent = play.Player;

        SetDecksAfterReversingFromHand(play, playerReversing);
        Formatter.ReverseACard(this, playerReversing);
        ReversalEffect(play);
        ApplyDamage(play);
        SetReversalState(playerReversing);
    }

    public void ReverseByDeck(Play play, int gapDamage)
    {
        Player playerReversing = play.Player.Oponent;
        Player oponent = play.Player;

        Formatter.View.SayThatCardWasReversedByDeck(playerReversing.Superstar.Name);
        oponent.DrawCardsBecauseOfStunValue(play.Card.GetStunValue(), gapDamage);
        SetReversalState(playerReversing);
    }

    private void SetReversalState(Player player)
    {
        player.HasReversedACard = true;
        player.WantsToReverseACard = false;        
    }

    private void SetDecksAfterReversingFromHand(Play play, Player playerReversing)
    {
        playerReversing.Oponent.Deck.DrawCardFromPossibleCardsToRingsideById(play.CardId);
        playerReversing.Deck.DrawCardFromPossibleReversalsToRingAreaById(
            ReversalId,
            play.Card,
            playerReversing.Fortitude
        );
    }
}

