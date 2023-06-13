namespace RawDeal.DeckHandler;


public class DeckChecker
{
    private List<Card> _deck;

    private List<Superstar> _superstars;

    private Superstar _superstar;

    public DeckChecker(List<Card> deck, List<Superstar> superstars, Superstar superstar)
    {
        _deck = deck;
        _superstars = superstars;
        _superstar = superstar;
    }

    public bool CheckDeck()
    {
        bool isValid = CheckSuperstar() && CheckCardCount() &&
                       CheckUniqueAndSetUpCardTypes() && CheckHeelFaceCardTypes() &&
                       CheckSuperstarLogo(_superstars);
        return isValid;
    }

    private bool CheckSuperstar() 
    { 
        bool isValid = true;
        if (_superstar == null) {
            isValid = false;
        }
        return isValid;
    }

    private bool CheckCardCount()
    {
        bool isValid = true;
        if (_deck.Count != 60)
        {
            isValid = false;
        }
        return isValid;
    }

    private bool CheckUniqueAndSetUpCardTypes()
    {
        Dictionary<string, int> cardCount = new Dictionary<string, int>();
        bool isValid = true;
        foreach (Card card in _deck)
        {
            if (card.ContainsSubtype(Subtype.Unique))
            {
                isValid = CheckUniqueTypeCard(card, cardCount);
            }
            else if (card.ContainsSubtype(Subtype.SetUp))
            {
                isValid = CheckSetUpTypeCard(card, cardCount);
            }
            else
            {
                isValid = CheckRegularTypeCard(card, cardCount);
            }
            if (!isValid)
            {
                break;
            }
        }
        return isValid;
    }

    private bool CheckRegularTypeCard(Card card, Dictionary<string, int> cardCount)
    {
        bool isValid = true;
        if (cardCount.ContainsKey(card.Title) && cardCount[card.Title] >= 3)
        {
            isValid = false;
        }
        else
        {
            if (cardCount.ContainsKey(card.Title))
            {
                cardCount[card.Title]++;
            }
            else
            {
                cardCount[card.Title] = 1;
            }
        }
        return isValid;
    }
    
    private bool CheckSetUpTypeCard(Card card, Dictionary<string, int> cardCount)
    {
        cardCount[card.Title] = int.MaxValue;
        return true;
    }

    private bool CheckUniqueTypeCard(Card card, Dictionary<string, int> cardCount)
    {
        bool isValid = true;
        if (cardCount.ContainsKey(card.Title) && cardCount[card.Title] >= 1)
        {
            isValid = false;
        }
        else
        {
            cardCount[card.Title] = 1;
        }
        return isValid;
    }

    private bool CheckHeelFaceCardTypes()
    {
        bool isHeel = false;
        bool isFace = false;
        bool isValid = true;
        foreach (Card card in _deck)
        {
            if (card.ContainsSubtype(Subtype.Heel))
            {
                isHeel = true;
            }
            else if (card.ContainsSubtype(Subtype.Face))
            {
                isFace = true;
            }
            if (isHeel && isFace)
            {
                isValid = false;
                break;
            }
        }

        return isValid;
    }

    private bool CheckSuperstarLogo(List<Superstar> superstars)
    {
        foreach (Card card in _deck)
        {
            foreach (string subtype in card.Subtypes)
            {
                if (superstars.Exists(s => s.Logo == subtype))
                {
                    if (!_superstar.Logo.Contains(subtype))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
}