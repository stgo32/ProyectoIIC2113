namespace RawDeal;


using RawDeal.DeckHandler;
using RawDealView.Options;


public class Player
{
    private bool _hasWon = false;
    public bool HasWon { 
        get { return _hasWon; }
        set { _hasWon = value; }
    }
    private int _fortitude = 0;
    public int Fortitude { 
        get { return _fortitude; } 
        set { _fortitude = value; } 
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
    private bool _hasPlayedThisTurn = false;
    public bool HasPlayedThisTurn { 
        get { return _hasPlayedThisTurn; } 
        set { _hasPlayedThisTurn = value; } 
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
        if (idCardSelected != -1)
        {
            Play = Initializer.InitPlayByType(idCardSelected, this);
            Play.Start();
        }
        // Card card = Deck.GetPossibleCardsToPlay()[idCardSelected];
        // if (card.PlayAs == "Maneuver")
        // {
        //     Play = new Maneuver(idCardSelected, this);
        // }
        // else if (card.PlayAs == "Action")
        // {
        //     Play = new Action(idCardSelected, this);
        // }
        // Play.Start();
    }

    // private Play InitPlayByType(int cardId)
    // {
    //     Card card = Deck.GetPossibleCardsToPlay()[cardId];
    //     if (card.PlayAs == "Maneuver")
    //     {
    //         Play = new Maneuver(cardId, this);
    //     }
    //     else if (card.PlayAs == "Action")
    //     {
    //         Play = new Action(cardId, this);
    //     }
    //     return Play;
    // }

    // public int PlayCardAsManeuver(int cardId)
    // {
    //     Card card = Deck.DrawCardFromPossibleCardsToRingAreaById(cardId);
    //     int damage = card.GetDamage();
    //     damage = DeliverDamage(damage);
    //     return damage;
    // }

    // public void PlayCardAsAction(int cardId)
    // {
    //     DiscardPossibleCardById(cardId);
    //     DrawACard();
    // }

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

    public void ReverseCardFromHand(int oponentCardId, int reversalId)
    {
        if (reversalId != -1)
        {
            Card oponentCard = Oponent.Deck.GetPossibleCardsToPlay()[oponentCardId];
            Card reversal = Deck.GetPossibleReversals(oponentCard)[reversalId];
            // reversal.ReverseFromHand(
            Oponent.Deck.DrawCardFromPossibleCardsToRingsideById(oponentCardId);
            Deck.DrawCardFromPossibleReversalsToRingAreaById(reversalId, oponentCard);
            string reversalInfo = Formatter.FormatCard(reversal, NextPlay.PlayCard);
            Formatter.View.SayThatPlayerReversedTheCard(Superstar.Name, reversalInfo);
        }
    }

    // private int DeliverDamage(int damage)
    // {
    //     _fortitude += damage;
    //     if (Oponent.Superstar.CanUseAbilityBeforeTakingDamage)
    //     {
    //         damage = Oponent.Superstar.TakeLessDamage(damage);
    //     }
    //     return damage;
    // }

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