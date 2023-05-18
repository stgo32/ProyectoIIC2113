namespace RawDeal;


using RawDeal.DeckHandler;
using RawDeal.Plays;
using RawDealView.Options;


public class Player
{
    private bool _hasWon = false;
    public bool HasWon { 
        get { return _hasWon; }
        set { _hasWon = value; }
    }

    // private int _fortitude = 0;
    public int Fortitude { 
        get {
            // count the fortitude of the cards in the ring area
            int fortitude = 0;
            foreach (Card card in Deck.RingArea)
            {
                fortitude += card.GetDamage();
            }
            return fortitude;
        } 
    }

    private bool _needToAskToUseAbility = false;
    public bool NeedToAskToUseAbility {
        get { return _needToAskToUseAbility; }
        set { _needToAskToUseAbility = value; }
    }

    private bool _wantsToUseAbility = false;
    public bool WantsToUseAbility {
        get { return _wantsToUseAbility; }
        set { _wantsToUseAbility = value; }
    }

    private bool _wantsToReverseACard = false;
    public bool WantsToReverseACard {
        get { return _wantsToReverseACard; }
        set { _wantsToReverseACard = value; }
    }

    private bool _hasReversedACard = false;
    public bool HasReversedACard {
        get { return _hasReversedACard; }
        set { _hasReversedACard = value; }
    }

    private Deck _deck = new Deck();
    public Deck Deck { 
        get { return _deck; }
        set { 
            _deck = value; 
            _deck.Player = this;
        }
    }

    public Superstar Superstar { 
        get { return Deck.Superstar; }
    } 

    public List<Card> Hand { get { return Deck.Hand; } }

    public List<Card> Arsenal { get { return Deck.Arsenal; } }

    public List<Card> RingArea { get { return Deck.RingArea; } }

    public List<Card> Ringside { get { return Deck.Ringside; } }

    private Player _oponent;
    public Player Oponent { 
        get { return _oponent; }
        set { _oponent = value; } 
    }

    private Play _play;
    public Play Play { 
        get { return _play; }
        set { _play = value; } 
    }

    public int SelectCardToPlay()
    {
        List<Card> possibleCards = Deck.GetPossibleCardsToPlay();
        List<string> formattedPossibleCards = Formatter.GetFormattedCardList(possibleCards, NextPlay.PlayCard);
        int idCardSelected = Formatter.View.AskUserToSelectAPlay(formattedPossibleCards);
        return idCardSelected;
    }

    public void PlayCard(int idCardSelected)
    {
        Play = Initializer.InitPlayByType(idCardSelected, this);
        Play.Start();
    }

    public int SelectReversal(Card oponentCard)
    {
        List<string> formattedReversals = Formatter.GetFormattedCardList(
            Deck.GetPossibleReversals(oponentCard),
            NextPlay.PlayCard
        );
        int reversalSelected = Formatter.View.AskUserToSelectAReversal(Superstar.Name, formattedReversals);
        if (reversalSelected != -1)
        {
            _wantsToReverseACard = true;
        }
        return reversalSelected;
    }

    public int HandleDamage(int damage)
    {
        // Fortitude += damage;
        if (Oponent.Superstar.CanUseAbilityBeforeTakingDamage)
        {
            damage = Oponent.Superstar.TakeLessDamage(damage);
        }
        return damage;
    }

    public Card RecieveDamage()
    {
        return Deck.DrawCardFromArsenalToRingside();
    }

    public void ReturnACard()
    {
        List<string> formattedHand = Formatter.GetFormattedCardList(Hand, NextPlay.ShowCards);
        int cardId = Formatter.View.AskPlayerToReturnOneCardFromHisHandToHisArsenal(Superstar.Name, formattedHand);
        Deck.DrawCardFromHandToArsenalById(cardId);
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
        int discardCardId = SelectCardToDiscard(iter);
        Deck.DrawCardFromHandToRingsideById(discardCardId);
    }

    public void DiscardPossibleCardById(int cardId)
    {
        string cardTitle = Deck.GetPossibleCardsToPlay()[cardId].Title;
        Formatter.View.SayThatPlayerMustDiscardThisCard(Superstar.Name, cardTitle);
        Deck.DrawCardFromPossibleCardsToRingsideById(cardId);
    }

    private int SelectCardToDiscard(int iter)
    {
        List<string> formattedHand = Formatter.GetFormattedCardList(Hand, NextPlay.ShowCards);
        int discardCardId = Formatter.View.AskPlayerToSelectACardToDiscard(
            formattedHand,
            Superstar.Name,
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
                Deck.DrawCardFromArsenalToHand();
            }
        }
    }

    public void DrawACard()
    {
        Deck.DrawCardFromArsenalToHand();
        Formatter.View.SayThatPlayerDrawCards(Superstar.Name, 1);
    }

    public void RecoverACard()
    {
        List<string> formattedRingside = Formatter.GetFormattedCardList(Ringside, NextPlay.ShowCards);
        int cardId = Formatter.View.AskPlayerToSelectCardsToRecover(Superstar.Name, 1, formattedRingside);
        Deck.DrawCardFromRingsideToArsenalById(cardId);
    }

    public void RetrieveACard()
    {
        List<string> formattedRingside = Formatter.GetFormattedCardList(Ringside, NextPlay.ShowCards);
        int cardId = Formatter.View.AskPlayerToSelectCardsToPutInHisHand(Superstar.Name, 1, formattedRingside);
        Deck.DrawCardFromRingsideToHandById(cardId);
    }

    public void DrawCardsBecauseOfStunValue(int stunValue, int gapDamage)
    {
        int howManyWillDraw = 0;
        if (stunValue > 0 && gapDamage > 0)
        {
            Console.WriteLine("Gap: " + gapDamage);
            howManyWillDraw = Formatter.View.AskHowManyCardsToDrawBecauseOfStunValue(Superstar.Name, stunValue);
            DrawCards(howManyWillDraw);
        }
    }
}