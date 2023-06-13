namespace RawDeal.DeckHandler;


using RawDeal.Reversals;
using RawDealView.Options;

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
        _ringside = new CardSet();
        _ringArea = new RingArea();
        _possibleReversalsToPlay = new PossibleReversalsToPlay(_player);
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

    public void ReturnACard()
    {
        List<string> formattedHand = Formatter.GetFormattedCardList(Hand.Cards, NextPlay.ShowCards);
        int cardId = Formatter.View.AskPlayerToReturnOneCardFromHisHandToHisArsenal(
            Superstar.Name,
            formattedHand
        );
        DrawCardFromHandToArsenalById(cardId);
    }
    public void DiscardCards(int quantity)
    {
        for (int i = quantity; i > 0; i--)
        {
            DiscardACard(i);
        }
    }

    public void DiscardACard(int iter = 1)
    {
        if (Hand.Any())
        {
            int discardCardId = SelectCardToDiscard(iter);
            DrawCardFromHandToRingsideById(discardCardId);
        }
    }

    private int SelectCardToDiscard(int iter)
    {
        List<string> formattedHand = Formatter.GetFormattedCardList(Hand.Cards, NextPlay.ShowCards);
        int discardCardId = Formatter.View.AskPlayerToSelectACardToDiscard(
            formattedHand,
            Superstar.Name,
            Superstar.Name, 
            iter
        );
        return discardCardId;
    }

    public void DiscardCardsFromOponentHand(int quantity)
    {
        for (int i = quantity; i > 0; i--)
        {
            DiscardACardFromOponentHand(i);
        }
    }

    public void DiscardACardFromOponentHand(int iter = 1)
    {
        if (_player.Oponent.Hand.Any())
        {
            int discardCardId = SelectCardFromOponentHandToDiscard(iter);
            _player.Oponent.Deck.DrawCardFromHandToRingsideById(discardCardId);
        }
    }

    public void DiscardPossibleCardById(int cardId)
    {
        string cardTitle = GetPossibleCardsToPlay().GetCard(cardId).Title;
        Formatter.View.SayThatPlayerMustDiscardThisCard(Superstar.Name, cardTitle);
        DrawCardFromPossibleCardsToRingsideById(cardId);
    }

    private int SelectCardFromOponentHandToDiscard(int iter)
    {
        List<string> formattedHand = Formatter.GetFormattedCardList(
            _player.Oponent.Hand.Cards,
            NextPlay.ShowCards
        );
        int discardCardId = Formatter.View.AskPlayerToSelectACardToDiscard(
            formattedHand,
            _player.Oponent.Superstar.Name,
            Superstar.Name,
            iter
        );
        return discardCardId;
    }

    public void DrawCards(int quantity)
    {
        if (quantity > 0)
        {
            Formatter.View.SayThatPlayerDrawCards(Superstar.Name, quantity);
            for (int i = quantity; i > 0; i--)
            {
                DrawCardFromArsenalToHand();
            }
        }
    }

    public void DrawACard()
    {
        DrawCardFromArsenalToHand();
        Formatter.View.SayThatPlayerDrawCards(Superstar.Name, 1);
    }

    public void DrawCardsBecauseOfStunValue(int stunValue, int gapDamage)
    {
        int howManyWillDraw = 0;
        if (stunValue > 0 && gapDamage > 0)
        {
            howManyWillDraw = Formatter.View.AskHowManyCardsToDrawBecauseOfStunValue(
                Superstar.Name,
                stunValue
            );
            DrawCards(howManyWillDraw);
        }
    }

    public void RecoverCards(int quantity)
    {
        for (int i = quantity; i > 0; i--)
        {
            RecoverACard(i);
        }
    }

    public void RecoverACard(int iter = 1)
    {
        List<string> formattedRingside = Formatter.GetFormattedCardList(
            Ringside.Cards,
            NextPlay.ShowCards
        );
        int cardId = Formatter.View.AskPlayerToSelectCardsToRecover(
            Superstar.Name,
            iter,
            formattedRingside
        );
        DrawCardFromRingsideToArsenalById(cardId);
    }

    public void RetrieveCards(int quantity)
    {
        for (int i = quantity; i > 0; i--)
        {
            RetrieveACard(i);
        }
    }

    public void RetrieveACard(int iter = 1)
    {
        List<string> formattedRingside = Formatter.GetFormattedCardList(
            Ringside.Cards,
            NextPlay.ShowCards
        );
        int cardId = Formatter.View.AskPlayerToSelectCardsToPutInHisHand(
            Superstar.Name,
            iter,
            formattedRingside
        );
        DrawCardFromRingsideToHandById(cardId);
    }
}