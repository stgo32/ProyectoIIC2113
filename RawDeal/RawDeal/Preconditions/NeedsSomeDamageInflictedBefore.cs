namespace RawDeal.Preconditions;



public class NeedsSomeDamageInflictedBefore : Precondition
{
    private int _damageNeeded;

    public NeedsSomeDamageInflictedBefore(int damageNeeded, Card card) : base(card)
    {
        _damageNeeded = damageNeeded;
    }

    public override bool IsPossibleToPlay(Player player)
    { 
        bool isPossible = fortitudePrecodition(player.Fortitude) &&
                          NeedsSomeDamageInflictedBeforePrecondition(player.LastDamageInflicted) &&
                          player.PlayedAManeuverLast;
        return isPossible;
    }

    private bool NeedsSomeDamageInflictedBeforePrecondition(int damageInflicted)
    {
        return damageInflicted >= _damageNeeded;
    }
}