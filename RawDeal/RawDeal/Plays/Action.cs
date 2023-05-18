namespace RawDeal.Plays;

public class Action : Play
{
    public Action(int cardId, Player player) : base(cardId, player) { }

    public override void Start()
    {
        Formatter.PlayCard(Card, Player);
        if (!IsBeingReversedByHand())
        {
            Formatter.View.SayThatPlayerSuccessfullyPlayedACard();
            Player.DiscardPossibleCardById(_cardId);
            Player.DrawACard();
        }
    }
}