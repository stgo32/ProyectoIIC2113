namespace RawDeal.DeckHandler;


using RawDeal.Initialize;

public class DeckReader
{
    private SuperstarSet _superstars;

    private CardSet _cards;

    private string[] _fileLines;

    public DeckReader(string filePath, SuperstarSet superstars, CardSet cards)
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
        for (int i = 0; i < _superstars.Count(); i++)
        {
            if (_superstars.Set[i].Name == superstarName)
            {
                supearstarIndex = i;
                break;
            }
        }
        Superstar superstar = SuperstarFactory.GetSuperstar(_superstars.Set[supearstarIndex]);
        superstar.Player = player;
        return superstar;
    }

    public CardSet ReadCards()
    {
        CardSet deck = new CardSet();
        _fileLines = _fileLines.Skip(1).ToArray(); 
        foreach (string cardTitle in _fileLines)
        {
            Card card = _cards.Cards.Find(c => c.Title == cardTitle);
            deck.AddCard(card);
        }
        return deck;
    }
}