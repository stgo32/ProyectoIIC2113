namespace RawDeal.Plays;


using RawDeal.Initialize;
using RawDeal.Effects;

public class Action : Play
{
    public Action(int cardId, Player player) : base(cardId, player) { }

    protected override void SuccessfullyPlayed()
    {
        base.SuccessfullyPlayed();
        UseEffect();
        Player.PlayedAManeuverLast = false;
        // Player.LastCardPlayed = Card.Title;
        Console.WriteLine("Player.LastCardPlayed = " + Player.LastCardPlayed);
    }

    protected override void UseEffect()
    {
        Effect effect = EffectFactory.GetEffect(Card, _cardId, Player);
        if (effect.GetType().Name != "StandardActionEffect")
        {
            Card card = Player.Deck.DrawCardFromPossibleCardsToRingAreaById(_cardId);
        }
        effect.Resolve();
    }
}