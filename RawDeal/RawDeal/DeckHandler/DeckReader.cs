namespace RawDeal.DeckHandler;


using RawDeal.Initialize;

public class DeckReader
{
    private List<Card> _deck;

    public void ReadDeckFromFile(string filePath, List<Superstar> superstars, List<Card> cards)
    {
        _deck = new List<Card>();
        string[] lines = File.ReadAllLines(filePath);
        SetSuperstar(lines, superstars);
        SetCards(lines, cards);
    }


    private Superstar SetSuperstar(string[] fileLines, List<Superstar> superstars)
    {
        string superstarName = fileLines[0];
        superstarName = superstarName.Split('(')[0].Trim();
        Superstar superstar = null;
        foreach (Superstar superstarInfo in superstars)
        {
            if (superstarInfo.Name == superstarName)
            {           
                superstar = SuperstarFactory.GetSuperstar(superstarInfo);
                break;
            }
        }
        return superstar;
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
}