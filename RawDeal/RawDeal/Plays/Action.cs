namespace RawDeal.Plays;

public class Action : Play
{
    public Action(int cardId, Player player) : base(cardId, player) { }

    public override void Start()
    {
        Formatter.PlayCard(Card, Player);
        if (!IsBeingReversedByHand())
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        base.Attack();
        Player.DiscardPossibleCardById(_cardId);
        Player.DrawACard();
    }
}