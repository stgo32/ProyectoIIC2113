namespace RawDeal;


using RawDeal.Plays;
using RawDeal.Initialize;
using RawDeal.Preconditions;
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

    private PlayAs _playAs;
    public PlayAs PlayAs { get { return _playAs; } set { _playAs = value; } }

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

    public bool IsPossibleToPlay(Player player)
    {
        bool isPossible = false;
        Precondition precondition = PreconditionFactory.GetPrecondition(this);
        isPossible = precondition.IsPossibleToPlay(player);
        return isPossible;
    }

    public bool ContainsSubtype(Subtype subtype)
    {
        if (subtype == Subtype.All)
        {
            return true;
        }
        return this.Subtypes.Contains(subtype.ToString());
    }

    public Card PlayCardAs(string type)
    {
        Card card = MemberwiseClone() as Card;
        card._playAs = (PlayAs)Enum.Parse(typeof(PlayAs), type);
        return card;
    }
}