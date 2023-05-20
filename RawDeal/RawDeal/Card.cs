namespace RawDeal;


using RawDeal.Reversals;
using RawDealView.Formatters;


public class Card : IViewableCardInfo
{
    public string Title { get; set; }

    public List<string> Types { get; set; }

    public List<string> Subtypes { get; set; }
    
    public string Fortitude { get; set; }

    public string Damage { get; set; }

    public string StunValue { get; set; }

    public string CardEffect { get; set; }

    private string _playAs;
    public string PlayAs { get { return _playAs; } set { _playAs = value; } }

    public bool isHibrid { get { return Types.Contains("Maneuver") && Types.Contains("Action"); } }
    

    public Card(string title, List<string> types, List<string> subtypes, string fortitude,
                string damage, string stunValue, string cardEffect)
    {
        Title = title;
        Types = types;
        Subtypes = subtypes;
        Fortitude = fortitude;
        Damage = damage;
        StunValue = stunValue;
        CardEffect = cardEffect;
    }

    public int GetFortitude()
    {
        int fortitude = 0;
        if (this.Fortitude != null)
        {
            fortitude = int.Parse(this.Fortitude);
        }
        return fortitude;
    }
    
    public int GetDamage()
    {
        int damage = 0;
        if (this.Damage != "#")
        {
            damage = int.Parse(this.Damage);
        }
        return damage;
    }

    public int GetStunValue()
    {
        int stunValue = 0;
        if (this.StunValue != null)
        {
            stunValue = int.Parse(this.StunValue);
        }
        return stunValue;
    }

    public bool IsPossibleToPlay(int fortitude)
    {
        bool isPossible = false;
        if (
            GetFortitude() <= fortitude &&
            (Types.Contains("Action") || Types.Contains("Maneuver"))
        )
        {
            isPossible = true;
        }
        return isPossible;
    }

    public bool ContainsSubtype(string subtype)
    {
        return this.Subtypes.Contains(subtype);
    }

    public Card PlayCardAs(string type)
    {
        Card card = MemberwiseClone() as Card;
        card._playAs = type;
        return card;
    }

    // private int HandleDamage(Player player, int damage)
    // {
    //     player.Fortitude += damage;
    //     if (player.Oponent.Superstar.CanUseAbilityBeforeTakingDamage)
    //     {
    //         damage = player.Oponent.Superstar.TakeLessDamage(damage);
    //     }
    //     return damage;
    // }

    // public void DeliverDamage(Player player, int damage)
    // {
    //     if (damage > 0)
    //     {
    //         Formatter.View.SayThatOpponentWillTakeSomeDamage(player.Superstar.Name, damage);
    //     }
    //     for (int i = 0; i < damage; i++)
    //     {
    //         if (player.Arsenal.Count == 0)
    //         {
    //             break;
    //         }
    //         Card cardOverturned = OverTurnCard(player, i, damage);
    //         if (CanBeReversedByDeck(cardOverturned, player.Oponent.Fortitude))
    //         {
    //             StopDamage(cardOverturned, damage-i-1);
    //             break;
    //         }
    //     }
    // }

    // private Card OverTurnCard(Player oponent, int iter, int damage)
    // {
    //     Card cardOverTurned = oponent.RecieveDamage();
    //     Formatter.PrintCardOverturned(cardOverTurned, iter+1, damage);
    //     return cardOverTurned;
    // }

    // private bool CanBeReversedByDeck(Card cardOvertuned, int fortitude)
    // {   
    //     bool canBeReversed = false;
    //     if (cardOvertuned.Types.Contains("Reversal"))
    //     {
    //         Reversal reversal;
    //         try {
    //             reversal = Initializer.InitReversalByTitle(cardOvertuned);
    //         } catch (Exception e) {
    //             return canBeReversed;
    //         }
    //         if (reversal.CanReverse(this, fortitude))
    //         {
    //             canBeReversed = true;
    //         }
    //     }
    //     return canBeReversed;
    // }

    // private void StopDamage(Card card, int gapDamage)
    // {
    //     Reversal reversal = Initializer.InitReversalByTitle(card);
    //     reversal.ReverseByDeck(this, gapDamage);
    // }
}