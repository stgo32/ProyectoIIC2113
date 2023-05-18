namespace RawDeal.Superstars;


public class ManKind : Superstar
{
    private Player _player;
    public override Player Player { 
        get {
            return _player;
        }
        set {
            _player = value;

            _player.WantsToUseAbility = true;
        }
    }

    public override bool CanChooseToUseAbility { get { return false; } }

    public override bool UsedAbilityThisTurn { get; set; } = false;

    public override bool CanUseAbilityAtBeginOfTurn { get { return true; } }
    
    public override bool CanUseAbilityBeforeTakingDamage { get { return true; } }

    public override void UseAbility()
    {
        if (Player.Arsenal.Count > 0)
        {
            Player.Deck.DrawCardFromArsenalToHand();
        }
    }

    public override int TakeLessDamage(int damage)
    {
        return damage - 1;
    }
}