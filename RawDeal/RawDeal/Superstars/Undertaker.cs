namespace RawDeal.Superstars;


public class Undertaker : Superstar
{
    public override Player Player { get; set; }

    public override bool UsedAbilityThisTurn { get; set; } = false;

    public override bool CanChooseToUseAbility { 
        get {
            if (UsedAbilityThisTurn) 
            {
                return false;
            }
            return Player.Hand.ContainsMoreOrEqualThan(2);
        }
    }

    public override bool CanUseAbilityAtBeginOfTurn { get { return false; } }
    
    public override bool CanUseAbilityBeforeTakingDamage { get { return false; } }

    public override void UseAbility()
    {
        Formatter.View.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        Player.Deck.DiscardCards(2);
        Player.Deck.RetrieveACard();
        UsedAbilityThisTurn = true;
    }
}