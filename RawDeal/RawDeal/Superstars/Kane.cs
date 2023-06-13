namespace RawDeal.Superstars;


public class Kane : Superstar
{
    private Player _player;
    public override Player Player { 
        get {
            return _player;
        }
        set {
            _player = value;
            _player.NeedToAskToUseAbility = false;
            _player.WantsToUseAbility = true;
        }
    }

    public override bool UsedAbilityThisTurn { get; set; } = false; 
     
    public override bool CanChooseToUseAbility { get { return false; } } 

    public override bool CanUseAbilityAtBeginOfTurn { get { return true; } }
    
    public override bool CanUseAbilityBeforeTakingDamage { get { return false; } }

    public override void UseAbility()
    {
        Formatter.View.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        DeliverExtraDamage();
    }

    private void DeliverExtraDamage()
    {
        Card cardOverturned = Player.Oponent.RecieveDamage();
        Formatter.View.SayThatSuperstarWillTakeSomeDamage(Player.Oponent.Superstar.Name, 1);
        Formatter.PrintCardOverturned(cardOverturned, 1, 1);
    }
}