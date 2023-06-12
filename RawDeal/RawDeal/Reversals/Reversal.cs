namespace RawDeal.Reversals;


using RawDeal.Plays;
using RawDeal.Initialize;
using RawDeal.Effects;

public abstract class Reversal : Card
{
    private int _reversalId;
    public int ReversalId { get { return _reversalId; } set { _reversalId = value; } }
    
    public Reversal(string title, List<string> types, List<string> subtypes, string fortitude,
                    string damage, string stunValue, string cardEffect)
                    : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public abstract bool CanReverse(Card card, int fortitude, Player oponent);

    public virtual bool CanReverseByDeck(Card card, int fortitude, Player oponent)
    {
        return CanReverse(card, fortitude, oponent);
    }

    protected bool CalculateFortitudeRestriction(int oponentFortitude, 
                                                 int NextSubtypeReversalIsPlusF)
    {
        int fortitude = GetFortitude();
        fortitude += NextSubtypeReversalIsPlusF;
        return fortitude <= oponentFortitude;
    }

    protected void UseReversalEffect(Play play)
    {
        Effect effect = EffectFactory.GetEffect(this, -1, play.Player.Oponent);
        effect.Resolve();  
    }
    
    protected abstract void ApplyDamage(Play play);

    public void ReverseFromHand(Play play)
    {
        Player playerReversing = play.Player.Oponent;
        Player oponent = play.Player;

        SetDecksAfterReversingFromHand(play, playerReversing);
        Formatter.ReverseACard(this, playerReversing);
        UseReversalEffect(play);
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

    protected void DeliverDamage(Player oponent, int damage)
    {
        if (damage > 0)
        {
            Formatter.View.SayThatSuperstarWillTakeSomeDamage(oponent.Superstar.Name, damage);
        }
        for (int i = 0; i < damage; i++)
        {
            if (oponent.Arsenal.IsEmpty())
            {
                break;
            }
            Card cardOverturned = OverTurnCard(oponent, i, damage);
        }
    }

    protected Card OverTurnCard(Player oponent, int iter, int damage)
    {
        Card cardOverTurned = oponent.RecieveDamage();
        Formatter.PrintCardOverturned(cardOverTurned, iter+1, damage);
        return cardOverTurned;
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

