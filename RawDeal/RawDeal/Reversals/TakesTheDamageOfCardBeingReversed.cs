namespace RawDeal.Reversals;


using RawDeal.Plays;


public class TakesTheDamageOfTheCardBeingReversed : MayReverseSomeDamageOrLess
{
    public TakesTheDamageOfTheCardBeingReversed(
        PlayAs playAs, Subtype subtype, int damageNeeded, string title, List<string> types, 
        List<string> subtypes, string fortitude, string damage, string stunValue, string cardEffect
    ) : base(playAs, subtype, damageNeeded, title, types, subtypes, fortitude, damage, stunValue,
             cardEffect) { }

    protected override void ApplyDamage(Play play)
    {
        int damage = play.Player.Oponent.HandleDamage(play.Card.GetDamage(), true);
        if (play.Card.ContainsSubtype(play.Player.DamageBonusForRestOfTurnSubtype))
        {
            damage += play.Player.DamageBonusForRestOfTurn;
        }
        damage += play.Player.NextSubtypeIsPlusD;
        DeliverDamage(play.Player, damage);
    }
}