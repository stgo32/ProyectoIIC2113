namespace RawDeal.Reversals;


using RawDeal.Plays;


public class MayOnlyReverseTheManeuverTitled : Reversal
{
    private string _maneuverToReverseTitle;

    public MayOnlyReverseTheManeuverTitled(
        string maneuverToReverseTitle, string title, List<string> types, List<string> subtypes,
        string fortitude, string damage, string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
        _maneuverToReverseTitle = maneuverToReverseTitle;
    }

    public override bool CanReverse(Card card, int fortitude, Player oponent)
    {
        bool fortitudeRestriction = CalculateFortitudeRestriction(
            fortitude,
            oponent.NextSubtypeReversalIsPlusF
        );
        bool reversalRestriction = card.PlayAs == PlayAs.Maneuver && 
                                                  card.Title == _maneuverToReverseTitle;
        return fortitudeRestriction && reversalRestriction;
    }

    protected override void ApplyDamage(Play play)
    {
        int damage = play.Player.Oponent.HandleDamage(GetDamage());
        DeliverDamage(play.Player, damage);
    }
}