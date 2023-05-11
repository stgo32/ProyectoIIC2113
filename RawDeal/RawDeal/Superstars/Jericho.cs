namespace RawDeal;


public class Jericho : Superstar
{
    public override Player Player { get; set; }
    public override bool UsedAbilityThisTurn { get; set; } = false;
    public override bool CanChooseToUseAbility { 
        get {
            if (UsedAbilityThisTurn) 
            {
                return false;
            }
            return Player.Hand.Count >= 1;
        }
    }
    public override bool CanUseAbilityAtBeginOfTurn { get { return false; } }
    public override bool CanUseAbilityBeforeTakingDamage { get { return false; } }

    public override void UseAbility()
    {
        Formatter.View.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        Player.DiscardACard();
        Player.Oponent.DiscardACard();
        UsedAbilityThisTurn = true;
    }
    
    public override int TakeLessDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}