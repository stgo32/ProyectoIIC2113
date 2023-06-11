namespace RawDeal.Plays;


using RawDeal.Reversals;
using RawDeal.Initialize;
using RawDeal.Effects;


public class Maneuver : Play
{
    public Maneuver(int cardId, Player player) : base(cardId, player) { }

    private void Stop(Card card, int gapDamage)
    {
        Reversal reversal = ReversalFactory.GetReversal(card);
        reversal.ReverseByDeck(this, gapDamage);
    }

    protected override void SuccessfullyPlayed()
    {
        base.SuccessfullyPlayed();
        UseEffect();
        if (!Player.Oponent.HasWon)
        {
            int damage = HandleDamage();
            DeliverDamage(damage);
        }
    }

    protected override void UseEffect()
    {
        Effect effect = EffectFactory.GetEffect(Card, _cardId, Player);
        Card card = Player.Deck.DrawCardFromPossibleCardsToRingAreaById(_cardId);
        effect.Resolve();
    }

    private int HandleDamage()
    {
        int damage = Card.GetDamage();
        if (Player.NextGrappleIsPlus4D && Card.ContainsSubtype("Grapple"))
        {
            damage += 4;
        }
        damage = Player.HandleDamage(damage);
        Player.PlayedAManeuverLast = true;
        Player.LastDamageInflicted = damage;
        return damage;
    }

    private void DeliverDamage(int damage)
    {
        Player oponent = Player.Oponent;
        if (damage > 0)
        {
            Formatter.View.SayThatSuperstarWillTakeSomeDamage(oponent.Superstar.Name, damage);
        }
        for (int i = 0; i < damage; i++)
        {
            if (oponent.Arsenal.IsEmpty())
            {
                Player.HasWonByPinVictory(); 
                break;
            }
            Card cardOverturned = OverTurnCard(oponent, i, damage);
            if (CanBeReversedByDeck(cardOverturned))
            {
                Stop(cardOverturned, damage-i-1);
                break;
            }
        }
    }

    private Card OverTurnCard(Player oponent, int iter, int damage)
    {
        Card cardOverTurned = oponent.RecieveDamage();
        Formatter.PrintCardOverturned(cardOverTurned, iter+1, damage);
        return cardOverTurned;
    }

    private bool CanBeReversedByDeck(Card cardOvertuned)
    {   
        bool canBeReversed = false;
        if (!_mayNotBeReversedEffect && cardOvertuned.Types.Contains("Reversal"))
        {
            Reversal reversal;
            try {
                reversal = ReversalFactory.GetReversal(cardOvertuned);
            } catch (Exception e) {
                return canBeReversed;
            }
            if (reversal.CanReverse(Card, Player.Oponent.Fortitude, Player))
            {
                canBeReversed = true;
            }
        }
        return canBeReversed;
    }
}