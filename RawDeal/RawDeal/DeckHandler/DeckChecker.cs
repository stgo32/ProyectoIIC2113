namespace RawDeal.DeckHandler;


public class DeckChecker
{
    private int _deckSize = 60;
    private int _maxCardCountInDeck = 3;
    private int _maxUniqueCardCountInDeck = 1;

    private CardSet _deck;

    private SuperstarSet _superstars;

    private Superstar _superstar;

    public DeckChecker(CardSet deck, SuperstarSet superstars, Superstar superstar)
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
        if (_deck.Count() != _deckSize)
        {
            isValid = false;
        }
        return isValid;
    }

    private bool CheckUniqueAndSetUpCardTypes()
    {
        Dictionary<string, int> cardCount = new Dictionary<string, int>();
        bool isValid = true;
        foreach (Card card in _deck.Cards)
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
        if (cardCount.ContainsKey(card.Title) && 
            cardCount[card.Title] >= _maxCardCountInDeck)
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
        if (cardCount.ContainsKey(card.Title) && 
            cardCount[card.Title] >= _maxUniqueCardCountInDeck)
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
        foreach (Card card in _deck.Cards)
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

    private bool CheckSuperstarLogo(SuperstarSet superstars)
    {
        bool isValid = true;
        foreach (Card card in _deck.Cards)
        {
            bool containsLogo = CheckLogoInSubtypes(card, superstars);
            if (!containsLogo)
            {
                isValid = false;
                break;
            }
        }
        return isValid;
    }

    private bool CheckLogoInSubtypes(Card card, SuperstarSet superstars)
    {
        bool containsLogo = true;
        foreach (string subtype in card.Subtypes)
        {
            containsLogo = CardContainsSuperstarLogo(subtype, superstars);
            if (!containsLogo)
            {
                break;
            }
        }
        return containsLogo;
    }

    private bool CardContainsSuperstarLogo(string subtype, SuperstarSet superstars)
    {
        bool containsLogo = true;
        if (superstars.Set.Exists(s => s.Logo == subtype))
        {
            if (!_superstar.Logo.Contains(subtype))
            {
                containsLogo = false;
            }
        }
        return containsLogo;
    }
}