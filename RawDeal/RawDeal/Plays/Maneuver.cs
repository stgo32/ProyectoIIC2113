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
        Console.WriteLine("Player.LastCardPlayed = " + Player.LastCardPlayed);
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
        Console.WriteLine("1. Damage: " + damage);
        Console.WriteLine("Player.NextManeuverIsPlusD = " + Player.NextManeuverIsPlusD);
        Console.WriteLine("Player.NextManeuverIsPlusDCounter = " + Player.NextManeuverIsPlusDCounter);
        Console.WriteLine("Player.LastCardPlayed = " + Player.LastCardPlayed);
        Console.WriteLine("Player.NextSubtypeDoesSomeEffect = " + Player.NextSubtypeDoesSomeEffect);
        if (Card.ContainsSubtype(Player.NextSubtypeDoesSomeEffect.ToString()) || 
            Player.NextSubtypeDoesSomeEffect == Subtype.All)
        {
            damage += Player.NextSubtypeIsPlusD;
        }
        if (Card.ContainsSubtype(Player.NextManeuverIsPlusDSubtype.ToString()) || 
            Player.NextManeuverIsPlusDSubtype == Subtype.All)
        {
            if (Card.PlayAs == PlayAs.Maneuver && Player.NextManeuverIsPlusDCounter > 0 && Player.PlayedAManeuverLast)
            {
                damage += Player.NextManeuverIsPlusD;
                Player.NextManeuverIsPlusDCounter --;
            }
        }
        Console.WriteLine("2. Damage: " + damage);
        if (Card.ContainsSubtype(Player.DamageBonusForRestOfTurnSubtype.ToString()) || 
            Player.DamageBonusForRestOfTurnSubtype == Subtype.All)
        {
            damage += Player.DamageBonusForRestOfTurn;
        }
        damage += Player.DamageBonusForPlayedAfterSomeDamage;
        Console.WriteLine("3. Damage: " + damage);
        damage = Player.HandleDamage(damage);
        Player.PlayedAManeuverLast = true;
        Effect effect = EffectFactory.GetEffect(Card, _cardId, Player);
        if (effect.GetType().Name == "NextManeuverPlayedIsPlusDamage")
        {
            Player.NextManeuverIsPlusDCounter ++;
            Player.NextManeuverIsPlusDSubtype = effect.GetSubtypeDoesSomeEffect();
            Console.WriteLine("AA Player.NextManeuverIsPlusDSubtype = " + Player.NextManeuverIsPlusDSubtype);
        }
        else 
        {
            Player.NextManeuverIsPlusDCounter = 0;
        }
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
}