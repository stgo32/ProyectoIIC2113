namespace RawDeal.Superstars;


public class StoneCold:Superstar
{
    public override Player Player { get; set; }

    public override bool CanChooseToUseAbility { 
        get {
            if (UsedAbilityThisTurn) 
            {
                return false;
            }
            return Player.Arsenal.ContainsMoreOrEqualThan(1);
        }
    }

    public override bool UsedAbilityThisTurn { get; set; } = false;

    public override bool CanUseAbilityAtBeginOfTurn { get { return false; } }

    public override bool CanUseAbilityBeforeTakingDamage { get { return false; } }

    public override void UseAbility()
    {
        Formatter.View.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        Player.Deck.DrawACard();
        Player.Deck.ReturnACard();
        UsedAbilityThisTurn = true;
    }
}