namespace RawDeal.Reversals;


using RawDeal.Plays;
using RawDeal.Initialize;
using RawDeal.Effects;


public class IrishWhip : Reversal
{
    public IrishWhip(string title, List<string> types, List<string> subtypes, string fortitude,
                      string damage, string stunValue, string cardEffect)
                      : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {}

    public override bool CanReverse(Card card, int fortitude, Player oponent)
    {
        bool fortitudeRestriction = CalculateFortitudeRestriction(
            fortitude,
            oponent.NextSubtypeReversalIsPlusF
        );
        bool reversalRestriction = card.PlayAs == PlayAs.Action
                                   && card.Title == Title;
        return fortitudeRestriction && reversalRestriction;
    }

    protected override void UseReversalEffect(Play play)
    {
        Effect effect = EffectFactory.GetEffect(this, -1, play.Player.Oponent);
        effect.Resolve();
    }

    protected override void ApplyDamage(Play play) { return; }
}