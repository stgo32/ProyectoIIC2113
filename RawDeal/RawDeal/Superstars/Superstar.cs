namespace RawDeal;


public abstract class Superstar
{
    public string Name { get; set;}

    public string Logo { get; set;}

    public int HandSize { get; set; }

    public int SuperstarValue { get; set; }

    public string SuperstarAbility { get; set;}

    public abstract Player Player { get; set; }

    public abstract bool UsedAbilityThisTurn { get; set; }

    public abstract bool CanChooseToUseAbility { get; }

    public abstract bool CanUseAbilityAtBeginOfTurn { get; }
    
    public abstract bool CanUseAbilityBeforeTakingDamage { get; }

    public abstract void UseAbility();

    public virtual int TakeLessDamage(int damage)
    {
        return damage;
    }
}