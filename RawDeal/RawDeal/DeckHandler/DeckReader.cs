namespace RawDeal.DeckHandler;


using RawDeal.Initialize;

public class DeckReader
{
    private List<Card> _deck;
    public List<Card> Deck { get { return _deck; } }

    public DeckReader(string filePath, List<Superstar> superstars, List<Card> cards)
    {
        ReadDeckFromFile(filePath, superstars, cards);
    }

    public void ReadDeckFromFile(string filePath, List<Superstar> superstars, List<Card> cards)
    {
        _deck = new List<Card>();
        string[] lines = File.ReadAllLines(filePath);
        SetSuperstarInfo(lines, superstars);
        SetCards(lines, cards);
    }


    private void SetSuperstarInfo(string[] fileLines, List<Superstar> superstars)
    {
        string superstarName = fileLines[0];
        superstarName = superstarName.Split('(')[0].Trim();
        foreach (Superstar superstarInfo in superstars)
        {
            if (superstarInfo.Name == superstarName)
            {           
                SetSuperstar(superstarInfo);
                break;
            }
        }
    }

    private void SetCards(string[] fileLines, List<Card> cards)
    {
        fileLines = fileLines.Skip(1).ToArray(); 
        foreach (string cardTitle in fileLines)
        {
            Card card = cards.Find(c => c.Title == cardTitle);
            _deck.Add(card);
        }
    }

    private void SetSuperstar(Superstar superstarInfo)
    {
        Superstar superstar = SuperstarFactory.GetSuperstar(superstarInfo);
        superstar.Name = superstarInfo.Name;
        superstar.Logo = superstarInfo.Logo;
        superstar.HandSize = superstarInfo.HandSize;
        superstar.SuperstarValue = superstarInfo.SuperstarValue;
        superstar.SuperstarAbility = superstarInfo.SuperstarAbility;
    }
}