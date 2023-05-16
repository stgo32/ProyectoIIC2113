namespace RawDeal;

public class Action : Play
{
    public Action(int cardId, Player player) : base(cardId, player) { }

    public override void Start()
    {
        Formatter.PlayCard(Card, Player);
        Formatter.View.SayThatPlayerSuccessfullyPlayedACard();
        Player.DiscardPossibleCardById(_cardId);
        Player.DrawACard();
    }

    public override void Stop()
    {
    }
}