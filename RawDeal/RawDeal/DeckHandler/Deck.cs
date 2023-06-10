namespace RawDeal.DeckHandler;


using RawDeal.Reversals;
using RawDeal.Initialize;


public class Deck
{
    private List<Card> _deck;

    private Arsenal _arsenal;
    public Arsenal Arsenal { get { return _arsenal; } set { _arsenal = value; } }

    private Hand _hand;
    public Hand Hand { get { return _hand; } set { _hand = value; } }

    private CardSet _ringside;
    public CardSet Ringside { get { return _ringside; } set { _ringside = value; } }

    private RingArea _ringArea;
    public RingArea RingArea { get { return _ringArea; } set { _ringArea = value; } }

    private PossibleCardsToPlay _possibleCardsToPlay  = new PossibleCardsToPlay();

    private PossibleReversalsToPlay _possibleReversalsToPlay;

    private Superstar _superstar;
    public Superstar Superstar { get { return _superstar; } set { _superstar = value; } } 
    
    private Player _player;
    public Player Player { get { return _player; } set { _player = value; } }

    public void ReadCardsFromFile(string filePath, List<Superstar> superstars, List<Card> cards)
    {
        DeckReader deckReader = new DeckReader(filePath, superstars, cards);
        _superstar = deckReader.ReadSuperstar(_player);
        _deck = deckReader.ReadCards();
    }

    public bool CheckCorrectness(List<Superstar> superstars)
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
        SetStartingPossibleReversals();
    }

    private void SetStartingArsenal()
    {
        _arsenal = new Arsenal();
        _arsenal.Cards = _deck;
    }

    private void SetStartingHand()
    {
        _hand = new Hand();
        for (int i = 0; i < _superstar.HandSize; i++)
        {
            DrawCardFromArsenalToHand();
        }
    }

    private void SetStartingRingside()
    {
        _ringside = new CardSet();
    }

    private void SetStartingRingArea()
    {
        _ringArea = new RingArea();
    }

    private void SetStartingPossibleReversals()
    {
        _possibleReversalsToPlay = new PossibleReversalsToPlay(_player);
    }


    public PossibleCardsToPlay GetPossibleCardsToPlay()
    {
        return _possibleCardsToPlay.Get(_player);
    }

    public PossibleReversalsToPlay GetPossibleReversals(Card oponentCard, int fortitude)
    {
        return _possibleReversalsToPlay.Get(fortitude, oponentCard);
    }

    public Reversal GetReversalById(int cardId)
    {
        Reversal reversal = _possibleReversalsToPlay.GetCardAsReversal(cardId);
        return reversal;
    }

    public bool CanReverseCard(Card card, int fortitude)
    {
        return GetPossibleReversals(card, fortitude).Any();
    }

    public void DrawCardFromArsenalToHand()
    {
        Card card = _arsenal.RemoveCard();
        _hand.AddCard(card);
    }
    
    public void DrawCardFromHandToRingAreaById(int cardId)
    {
        Card card = _hand.RemoveAt(cardId);
        _ringArea.AddCard(card);
    }

    public void DrawCardFromHandToArsenalById(int cardId)
    {
        Card card = _hand.RemoveAt(cardId);
        _arsenal.AddCard(card);
    }

    public Card DrawCardFromArsenalToRingside()
    {
        Card card = _arsenal.RemoveCard();
        _ringside.AddCard(card);
        return card;
    }

    public Card DrawCardFromRingsideToArsenalById(int cardId)
    {
        Card card = _ringside.RemoveAt(cardId);
        _arsenal.AddCard(card);
        return card;
    }

    public void DrawCardFromHandToRingsideById(int cardId)
    {
        Card card = _hand.RemoveAt(cardId);
        _ringside.AddCard(card);
    }

    public void DrawCardFromRingsideToHandById(int cardId)
    {
        Card card = _ringside.RemoveAt(cardId);
        _hand.AddCard(card);
    }

    public Card DrawCardFromPossibleCardsToRingAreaById(int cardId)
    {
        Card card = GetPossibleCardsToPlay().GetCard(cardId);
        int cardCount = _possibleCardsToPlay.CountCardAppearances(cardId);
        int idCardAtHand = _hand.FindCardIdByCountInPossibleCardsToPlay(cardCount, card);
        DrawCardFromHandToRingAreaById(idCardAtHand);
        return card;
    }

    public void DrawCardFromPossibleCardsToRingsideById(int cardId)
    {
        Card card = GetPossibleCardsToPlay().GetCard(cardId);
        int cardCount = _possibleCardsToPlay.CountCardAppearances(cardId);
        int idCardAtHand = _hand.FindCardIdByCountInPossibleCardsToPlay(cardCount, card);
        DrawCardFromHandToRingsideById(idCardAtHand);
    }

    public void DrawCardFromPossibleReversalsToRingAreaById(int reversedId, Card oponentCard,
                                                            int fortitude)
    {
        GetPossibleReversals(oponentCard, fortitude);
        Card card = _possibleReversalsToPlay.GetCard(reversedId);
        int cardCount = _possibleReversalsToPlay.CountCardAppearances(reversedId, oponentCard, fortitude);
        int idCardAtHand = Hand.FindCardIdByCountInPossibleCardsToPlay(cardCount, card);
        DrawCardFromHandToRingAreaById(idCardAtHand);
    }
}