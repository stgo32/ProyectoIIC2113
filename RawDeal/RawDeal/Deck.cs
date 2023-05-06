namespace RawDeal;

public class Deck
{
    private List<Card> _deck;
    // public List<Card> Cards { get { return _deck; } set { _deck = value; } }
    private List<Card> _arsenal;
    public List<Card> Arsenal { get { return _arsenal; } set { _arsenal = value; } }
    private List<Card> _hand;
    public List<Card> Hand { get { return _hand; } set { _hand = value; } }
    private List<Card> _ringside;
    public List<Card> Ringside { get { return _ringside; } set { _ringside = value; } }
    private List<Card> _ringArea;
    public List<Card> RingArea { get { return _ringArea; } set { _ringArea = value; } }
    
    // public void Read(string filePath, List<Superstar> superstars, List<Card> cards)
    // {
    //     _deck = new List<Card>();
    //     string[] lines = File.ReadAllLines(filePath);
    //     SetSuperstar(lines, superstars);
    //     SetCards(lines, cards);
    // }
}