namespace RawDeal;

public class Maneuver : Play
{
    public Maneuver(int cardId, Player player) : base(cardId, player) { }

    public override void Start()
    {
        Formatter.PlayCard(Card, Player);
        if (!IsBeingReversedByHand())
        {
            Formatter.View.SayThatPlayerSuccessfullyPlayedACard();
            Player.Deck.DrawCardFromPossibleCardsToRingAreaById(_cardId);
            Attack();
        }
    }

    public override void Stop()
    {
    }

    private void Attack()
    {
        int damage = Card.GetDamage();
        Player.Fortitude += damage;
        if (Player.Oponent.Superstar.CanUseAbilityBeforeTakingDamage)
        {
            damage = Player.Oponent.Superstar.TakeLessDamage(damage);
        }
        DeliverDamage(damage);
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
            Card cardOvertuned = oponent.RecieveDamage();
            Formatter.PrintCardOverturned(cardOvertuned, i+1, damage);
            if (CanBeReversedByDeck(cardOvertuned))
            {
                Reversal reversal = Initializer.InitReversalByTitle(cardOvertuned);
                reversal.ReverseByDeck(this);
                break;
            }

            // if (cardOvertuned.Types.Contains("Reversal"))
            // {
            //     Reversal reversal = Initializer.InitReversalByTitle(cardOvertuned);
            //     if (reversal.CanReverse(Card, Player.Fortitude))
            //     {
            //         Formatter.View.SayThatCardWasReversedByDeck(oponent.Superstar.Name);
            //         oponent.WantsToReverseACard = true;
            //         break;
            //     }
            // }
        }
    }

    private bool CanBeReversedByDeck(Card cardOvertuned)
    {   
        bool canBeReversed = false;
        if (cardOvertuned.Types.Contains("Reversal"))
        {
            Reversal reversal = Initializer.InitReversalByTitle(cardOvertuned);
            if (reversal.CanReverse(Card, Player.Fortitude))
            {
                canBeReversed = true;
            }
        }
        return canBeReversed;
    }
}