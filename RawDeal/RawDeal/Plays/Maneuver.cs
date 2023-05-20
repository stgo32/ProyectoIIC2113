namespace RawDeal.Plays;


using RawDeal.Reversals;


public class Maneuver : Play
{
    public Maneuver(int cardId, Player player) : base(cardId, player) { }

    public override void Start()
    {
        Formatter.PlayCard(Card, Player);
        if (!IsBeingReversedByHand())
        {
            Attack();
        }
    }

    private void Stop(Card card, int gapDamage)
    {
        Reversal reversal = Initializer.InitReversalByTitle(card);
        reversal.ReverseByDeck(this, gapDamage);
    }

    protected override void Attack()
    {
        base.Attack();
        Player.Deck.DrawCardFromPossibleCardsToRingAreaById(_cardId);
        int damage = HandleDamage();
        DeliverDamage(damage);
        // int damage = Player.HandleDamage(Card.GetDamage());
        // Card.DeliverDamage(Player.Oponent, damage);
    }

    private int HandleDamage()
    {
        int damage = Card.GetDamage();
        if (Player.NextGrappleIsPlus4D && Card.ContainsSubtype("Grapple"))
        {
            damage += 4;
            Player.ResetJockeyingForPosition();
        }
        damage = Player.HandleDamage(damage);
        return damage;
    }

    private void DeliverDamage(int damage)
    {
        Player oponent = Player.Oponent;
        if (damage > 0)
        {
            Formatter.View.SayThatOpponentWillTakeSomeDamage(oponent.Superstar.Name, damage);
        }
        for (int i = 0; i < damage; i++)
        {
            if (oponent.Arsenal.Count == 0)
            {
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
        if (cardOvertuned.Types.Contains("Reversal"))
        {
            Reversal reversal;
            try {
                reversal = Initializer.InitReversalByTitle(cardOvertuned);
            } catch (Exception e) {
                return canBeReversed;
            }
            if (reversal.CanReverse(Card, Player.Fortitude))
            {
                canBeReversed = true;
            }
        }
        return canBeReversed;
    }
}