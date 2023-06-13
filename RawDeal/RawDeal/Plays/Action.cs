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
    }

    protected override void UseEffect()
    {
        Effect effect = EffectFactory.GetEffect(Card, _cardId, Player);
        effect.TheCardHasBeenUsed(_cardId);
        effect.Resolve();
    }
}