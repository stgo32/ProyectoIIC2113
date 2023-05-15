namespace RawDeal.DeckHandler;


using RawDeal.Superstars;
using RawDeal.Reversals;


public class Deck
{
    private List<Card> _deck;
    private List<Card> _arsenal;
    public List<Card> Arsenal { get { return _arsenal; } set { _arsenal = value; } }
    private List<Card> _hand;
    public List<Card> Hand { get { return _hand; } set { _hand = value; } }
    private List<Card> _ringside;
    public List<Card> Ringside { get { return _ringside; } set { _ringside = value; } }
    private List<Card> _ringArea;
    public List<Card> RingArea { get { return _ringArea; } set { _ringArea = value; } }
    private Superstar _superstar;
    public Superstar Superstar { get { return _superstar; } set { _superstar = value; } } 
    private Player _player;
    public Player Player { get { return _player; } set { _player = value; } }

    public void Read(string filePath, List<Superstar> superstars, List<Card> cards)
    {
        _deck = new List<Card>();
        string[] lines = File.ReadAllLines(filePath);
        SetSuperstar(lines, superstars);
        SetCards(lines, cards);
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

    private void SetSuperstar(string[] fileLines, List<Superstar> superstars)
    {
        string superstarName = fileLines[0];
        superstarName = superstarName.Split('(')[0].Trim();
        foreach (Superstar superstarInfo in superstars)
        {
            if (superstarInfo.Name == superstarName)
            {           
                SetSuperstarType(superstarInfo);
                SetSuperstarInfo(superstarInfo);
                break;
            }
        }
    }

    private void SetSuperstarType(Superstar superstarInfo)
    {
        string superstarName = superstarInfo.Name;
        if (superstarName == "STONE COLD STEVE AUSTIN")
        {
            _superstar = new StoneCold();
        }
        else if (superstarName == "THE UNDERTAKER")
        {
            _superstar = new Undertaker();
        }
        else if (superstarName == "MANKIND")
        {
            _superstar = new ManKind();
        }
        else if (superstarName == "HHH")
        {
            _superstar = new HHH();
        }
        else if (superstarName == "THE ROCK")
        {
            _superstar = new TheRock();
        }
        else if (superstarName == "KANE")
        {
            _superstar = new Kane();
        }
        else if (superstarName == "CHRIS JERICHO")
        {
            _superstar = new Jericho();
        }
        else
        {
            throw new Exception("Superstar not found");
        }
    }

    private void SetSuperstarInfo(Superstar superstarInfo)
    {
        _superstar.Name = superstarInfo.Name;
        _superstar.Logo = superstarInfo.Logo;
        _superstar.HandSize = superstarInfo.HandSize;
        _superstar.SuperstarValue = superstarInfo.SuperstarValue;
        _superstar.SuperstarAbility = superstarInfo.SuperstarAbility;
        _superstar.Player = Player;
    }

    public bool Check(List<Superstar> superstars)
    {
        DeckChecker deckChecker = new DeckChecker(_deck, superstars, Superstar);
        bool isValid = deckChecker.CheckDeck();
        if (isValid)
        {
            SetStartingDecks();
        }
        return isValid;
    }

    private void SetStartingDecks()
    {
        SetStartingArsenal();
        SetStartingHand();
        SetStartingRingside();
        SetStartingRingArea();
    }

    private void SetStartingArsenal()
    {
        _arsenal = new List<Card>();
        _arsenal = _deck;
    }

    private void SetStartingHand()
    {
        _hand = new List<Card>();
        for (int i = 0; i < _superstar.HandSize; i++)
        {
            DrawCardFromArsenalToHand();
        }
    }

    private void SetStartingRingside()
    {
        _ringside = new List<Card>();
    }

    private void SetStartingRingArea()
    {
        _ringArea = new List<Card>();
    }


    public List<Card> GetPossibleCardsToPlay()
    {
        List<Card> possibleCards = new List<Card>();
        foreach (Card card in _hand)
        {
            if (card.IsPossibleToPlay(Player.Fortitude))
            {
                AddPossibleCard(card, possibleCards);
            }
        }
        return possibleCards;
    }

    private void AddPossibleCard(Card card, List<Card> possibleCards)
    {
        foreach (string type in card.Types)
        {
            Card cardToAdd = card.PlayCardAs(type);
            possibleCards.Add(cardToAdd);
        }
    }

    private int FindCardIdAtHandByCountInPossibleCardsToPlay(int cardCount, Card card)
    {
        int idCardAtHand = 0;
        for (int i = 0; i < _hand.Count; i++)
        {
            if (_hand[i].Title == card.Title)
            {
                if (cardCount == 0)
                {
                    idCardAtHand = i;
                    return idCardAtHand;
                }
                else
                {
                    cardCount--;
                }
            }
        }
        return idCardAtHand;
    }

    private int CountCardAppearancesInPossibleCardsToPlay(int cardId)
    {
        Card card = GetPossibleCardsToPlay()[cardId];
        int cardCount = 0;
        int hibridCounter = 0;
        for (int i = 0; i < cardId; i++)
        {
            Card possibleCard = GetPossibleCardsToPlay()[i];
            if (possibleCard.Title == card.Title)
            {
                ManageCountersOptionsForCardAppearances(
                    possibleCard, ref cardCount, ref hibridCounter
                );
            }
        }
        return cardCount;
    }

    private int CountCardAppearancesInPossibleReversals(int cardId, Card oponentCard)
    {
        Card card = GetPossibleReversals(oponentCard)[cardId];
        int cardCount = 0;
        int hibridCounter = 0;
        for (int i = 0; i < cardId; i++)
        {
            Card possibleCard = GetPossibleReversals(oponentCard)[i];
            if (possibleCard.Title == card.Title)
            {
                ManageCountersOptionsForCardAppearances(
                    possibleCard, ref cardCount, ref hibridCounter
                );
            }
        }
        return cardCount;
    }

    private void ManageCountersOptionsForCardAppearances(Card possibleCard, ref int cardCount, 
                                                         ref int hibridCounter)
    {
        if (possibleCard.isHibrid && hibridCounter == 0)
        {
            hibridCounter++;
        }
        else if (possibleCard.isHibrid && hibridCounter == 1)
        {
            cardCount++;
            hibridCounter = 0;
        }
        else
        {
            cardCount++;
        }
    }


    public List<Card> GetPossibleReversals(Card oponentCard)
    {
        List<Card> possibleReversals = new List<Card>();
        foreach (Card card in _hand)
        {
            if (card.Types.Contains("Reversal"))
            {
                AddPossibleReversal(card, ref possibleReversals, oponentCard);
            }
        }
        return possibleReversals;
    }

    private void AddPossibleReversal(Card card, ref List<Card> possibleReversals, Card oponentCard)
    {
        Reversal reversal;
        try {
            reversal = InitReversalByName(card);
        }
        catch (Exception e)
        {
            return;
        }
        if (reversal.GetFortitude() <= Player.Fortitude && reversal.CanReverse(oponentCard))
        {
            possibleReversals.Add(card.PlayCardAs("Reversal"));
        }
    }

    public bool CanReverseCard(Card card)
    {
        return GetPossibleReversals(card).Any();
    }

    public void PutReversedCardIntoRingside(Card card)
    {
        _ringside.Add(card);
    }

    public Reversal InitReversalByName(Card card)
    {
        string reversalTitle = card.Title;
        Reversal reversal;
        if (reversalTitle == "Break the Hold")
        {
            reversal = new BreakTheHole(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                        card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "Escape Move")
        {
            reversal = new EscapeMove(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                      card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "No Chance in Hell")
        {
            reversal = new NoChanceInHell(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                          card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "Step Aside")
        {
            reversal = new StepAside(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                     card.Damage, card.StunValue, card.CardEffect);
        }
        else
        {
            throw new Exception("Reversal not found");
        }
        reversal.PlayAs = "Reversal";
        return reversal;
    }

    public void DrawCardFromArsenalToHand()
    {
        Card card = _arsenal[_arsenal.Count - 1];
        _arsenal.RemoveAt(_arsenal.Count - 1);
        _hand.Add(card);
    }

    public Card DrawCardFromPossibleCardsToRingAreaById(int cardId)
    {
        Card card = GetPossibleCardsToPlay()[cardId];
        int cardCount = CountCardAppearancesInPossibleCardsToPlay(cardId);
        int idCardAtHand = FindCardIdAtHandByCountInPossibleCardsToPlay(cardCount, card);
        DrawCardFromHandToRingAreaById(idCardAtHand);
        return card;
    }

    public void DrawCardFromPossibleCardsToRingsideById(int cardId)
    {
        Card card = GetPossibleCardsToPlay()[cardId];
        int cardCount = CountCardAppearancesInPossibleCardsToPlay(cardId);
        int idCardAtHand = FindCardIdAtHandByCountInPossibleCardsToPlay(cardCount, card);
        DrawCardFromHandToRingsideById(idCardAtHand);
    }

    public void DrawCardFromPossibleReversalsToRingAreaById(int reversedId, Card oponentCard)
    {
        Card card = GetPossibleReversals(oponentCard)[reversedId];
        int cardCount = CountCardAppearancesInPossibleReversals(reversedId, oponentCard);
        int idCardAtHand = FindCardIdAtHandByCountInPossibleCardsToPlay(cardCount, card);
        DrawCardFromHandToRingAreaById(idCardAtHand);
    }
    
    public void DrawCardFromHandToRingAreaById(int cardId)
    {
        Card card = _hand[cardId];
        _hand.RemoveAt(cardId);
        _ringArea.Add(card);
    }

    public void DrawCardFromHandToArsenalById(int cardId)
    {
        Card card = _hand[cardId];
        _hand.RemoveAt(cardId);
        _arsenal.Insert(0, card);
    }

    public Card DrawCardFromArsenalToRingside()
    {
        Card card = _arsenal[_arsenal.Count - 1];
        _arsenal.RemoveAt(_arsenal.Count - 1);
        _ringside.Add(card);
        return card;
    }

    public Card DrawCardFromRingsideToArsenalById(int cardId)
    {
        Card card = _ringside[cardId];
        _ringside.RemoveAt(cardId);
        _arsenal.Insert(0, card);
        return card;
    }

    public void DrawCardFromHandToRingsideById(int cardId)
    {
        Card card = _hand[cardId];
        _hand.RemoveAt(cardId);
        _ringside.Add(card);
    }

    public void DrawCardFromRingsideToHandById(int cardId)
    {
        Card card = _ringside[cardId];
        _ringside.RemoveAt(cardId);
        _hand.Add(card);
    }
}