namespace RawDeal.DeckHandler;


using RawDeal.Initialize;

public class DeckReader
{
    private List<Superstar> _superstars;

    private List<Card> _cards;

    private string[] _fileLines;

    public DeckReader(string filePath, List<Superstar> superstars, List<Card> cards)
    {
        _fileLines = File.ReadAllLines(filePath);
        _superstars = superstars;
        _cards = cards;
    }

    public Superstar ReadSuperstar(Player player)
    {
        string superstarName = _fileLines[0];
        superstarName = superstarName.Split('(')[0].Trim();
        int supearstarIndex = 0;
        for (int i = 0; i < _superstars.Count; i++)
        {
            if (_superstars[i].Name == superstarName)
            {
                supearstarIndex = i;
                break;
            }
        }
        Superstar superstar = SuperstarFactory.GetSuperstar(_superstars[supearstarIndex]);
        superstar.Player = player;
        return superstar;
    }

    public List<Card> ReadCards()
    {
        List<Card> deck = new List<Card>();
        _fileLines = _fileLines.Skip(1).ToArray(); 
        foreach (string cardTitle in _fileLines)
        {
            Card card = _cards.Find(c => c.Title == cardTitle);
            deck.Add(card);
        }
        return deck;
    }
}