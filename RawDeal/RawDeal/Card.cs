namespace RawDeal;


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
    public bool isHibrid { get { return Types.Count > 1; } }

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
        if (this.Damage != null)
        {
            damage = int.Parse(this.Damage);
        }
        return damage;
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
}