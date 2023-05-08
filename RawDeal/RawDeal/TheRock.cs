namespace RawDeal;


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
    public override bool CanChooseToUseAbility { 
        get { return false; } 
        set { CanChooseToUseAbility = value; }
    }
    public override bool CanUseAbilityAtBeginOfTurn { 
        get {
            bool isValid = false;
            if (Player.Ringside.Count > 0)
            {
                isValid = true;
            }
            return isValid;
        }
    }

    public override bool CanUseAbilityBeforeTakingDamage { get { return false; } }
    
    public override void UseAbility()
    {
        Player.RecoverACard();
    }

   public override int TakeLessDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}