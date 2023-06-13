namespace RawDeal.Superstars;


public class HHH : Superstar
{
    public override Player Player { get; set; }

    public override bool UsedAbilityThisTurn { get; set; } = false;

    public override bool CanChooseToUseAbility { get { return false; } }

    public override bool CanUseAbilityAtBeginOfTurn { get { return false; } }
    
    public override bool CanUseAbilityBeforeTakingDamage { get { return false; } }

    public override void UseAbility()
    {
        return;
    }
}