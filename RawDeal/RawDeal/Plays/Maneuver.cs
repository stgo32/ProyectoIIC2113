namespace RawDeal.Plays;


using RawDeal.Reversals;
using RawDeal.Initialize;
using RawDeal.Effects;


public class Maneuver : Play
{
    private Effect _effect;
    public Maneuver(int cardId, Player player) : base(cardId, player)
    { 
        _effect = EffectFactory.GetEffect(Card, _cardId, Player);
    }

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
        Card card = Player.Deck.DrawCardFromPossibleCardsToRingAreaById(_cardId);
        _effect.Resolve();
    }

    private int HandleDamage()
    {
        int damage = Card.GetDamage();
        damage += GetDamageBonus();
        damage = Player.HandleDamage(damage);
        ResetDamageBonusEffects(damage);
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
        Player.DamageBonusForPlayedAfterSomeDamage = 0;
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
            if (reversal.CanReverseByDeck(Card, Player.Oponent.Fortitude, Player))
            {
                canBeReversed = true;
            }
        }
        return canBeReversed;
    }

    private int GetDamageBonus()
    {
        int damageBonus = 0;
        damageBonus += CheckNextSubtypeDoesSomeEffect();
        damageBonus += CheckNextManeuverIsPlusDamage();
        damageBonus += CheckDamageBonusForRestOfTurn();
        damageBonus += Player.DamageBonusForPlayedAfterSomeDamage;
        return damageBonus;
    }

    private int CheckNextSubtypeDoesSomeEffect()
    {
        int damageBonus = 0;
        if (Card.ContainsSubtype(Player.NextSubtypeDoesSomeEffect) || 
            Player.NextSubtypeDoesSomeEffect == Subtype.All)
        {
            damageBonus = Player.NextSubtypeIsPlusD;
        }
        return damageBonus;
    }

    private int CheckNextManeuverIsPlusDamage()
    {
        int damageBonus = 0;
        if (Card.ContainsSubtype(Player.NextManeuverIsPlusDSubtype) || 
            Player.NextManeuverIsPlusDSubtype == Subtype.All)
        {
            if (Card.PlayAs == PlayAs.Maneuver && Player.NextManeuverIsPlusDCounter > 0 && Player.PlayedAManeuverLast)
            {
                damageBonus = Player.NextManeuverIsPlusD;
                Player.NextManeuverIsPlusDCounter --;
            }
        }
        return damageBonus;
    }

    private int CheckDamageBonusForRestOfTurn()
    {
        int damageBonus = 0;
        if (Card.ContainsSubtype(Player.DamageBonusForRestOfTurnSubtype) || 
            Player.DamageBonusForRestOfTurnSubtype == Subtype.All)
        {
            damageBonus = Player.DamageBonusForRestOfTurn;
        }        
        return damageBonus;
    }

    private void ResetDamageBonusEffects(int damage)
    {
        _effect.AfterEffectConfig();
        Player.PlayedAManeuverLast = true;
        Player.LastDamageInflicted = damage;
    }
}