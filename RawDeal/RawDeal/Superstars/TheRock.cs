namespace RawDeal.Superstars;


public class TheRock : Superstar
{
    private Player _player;
    public override Player Player { 
        get {
            return _player;
        }
        set {
            _player = value;
            _player.NeedToAskToUseAbility = true;
        }
    }

    public override bool UsedAbilityThisTurn { get; set; } = false;

    public override bool CanChooseToUseAbility { get { return false; } }
    
    public override bool CanUseAbilityAtBeginOfTurn { 
        get {
            bool isValid = false;
            if (Player.Ringside.Any())
            {
                isValid = true;
            }
            return isValid;
        }
    }

    public override bool CanUseAbilityBeforeTakingDamage { get { return false; } }
    
    public override void UseAbility()
    {
        Player.Deck.RecoverACard();
    }
}