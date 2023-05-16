namespace RawDeal;

public class Maneuver : Play
{
    public Maneuver(int cardId, Player player) : base(cardId, player) { }

    public override void Start()
    {
        Formatter.PlayCard(Card, Player);
        if (!IsBeingReversedFromHand())
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
        }
    }
}