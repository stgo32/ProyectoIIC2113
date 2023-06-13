namespace RawDeal;


using RawDeal.DeckHandler;
using RawDeal.Plays;
using RawDeal.Initialize;
using RawDealView.Options;


public class Player
{
    private bool _hasWon = false;

    public bool HasWon { 
        get { return _hasWon; }
        set { _hasWon = value; }
    }

    public int Fortitude { get { return RingArea.CalculatePlayerFortitude(); } }

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

    private bool _playedAManeuverLast = false;
    public bool PlayedAManeuverLast {
        get { return _playedAManeuverLast; }
        set { _playedAManeuverLast = value; }
    }

    int _lastDamageInflicted = 0;
    public int LastDamageInflicted {
        get { return _lastDamageInflicted; }
        set { _lastDamageInflicted = value; }
    }

    private int _nextSubtypeIsPlusD = 0;
    public int NextSubtypeIsPlusD {
        get { return _nextSubtypeIsPlusD; }
        set { _nextSubtypeIsPlusD = value; }
    }

    private int _nextSubtypeReversalIsPlusF = 0;
    public int NextSubtypeReversalIsPlusF {
        get { return _nextSubtypeReversalIsPlusF; }
        set { _nextSubtypeReversalIsPlusF = value; }
    }

    int _nextManeuverIsPlusD = 0;
    public int NextManeuverIsPlusD {
        get { return _nextManeuverIsPlusD; }
        set { _nextManeuverIsPlusD = value; }
    }

    int _nextManeuverIsPlusDCounter = 0;
    public int NextManeuverIsPlusDCounter {
        get { return _nextManeuverIsPlusDCounter; }
        set { _nextManeuverIsPlusDCounter = value; }
    }

    Subtype _nextManeuverIsPlusDSubtype = Subtype.None;
    public Subtype NextManeuverIsPlusDSubtype {
        get { return _nextManeuverIsPlusDSubtype; }
        set { _nextManeuverIsPlusDSubtype = value; }
    }

    private int _damageBonusForRestOfTurn = 0;
    public int DamageBonusForRestOfTurn {
        get { return _damageBonusForRestOfTurn; }
        set { _damageBonusForRestOfTurn = value; }
    }

    private int _damageBonusForPlayedAfterSomeDamage = 0;
    public int DamageBonusForPlayedAfterSomeDamage {
        get { return _damageBonusForPlayedAfterSomeDamage; }
        set { _damageBonusForPlayedAfterSomeDamage = value; }
    }

    private Subtype _damageBonusForRestOfTurnSubtype = Subtype.None;
    public Subtype DamageBonusForRestOfTurnSubtype {
        get { return _damageBonusForRestOfTurnSubtype; }
        set { _damageBonusForRestOfTurnSubtype = value; }
    }

    private Subtype _nextSubtypeDoesSomeEffect = Subtype.None;
    public Subtype NextSubtypeDoesSomeEffect {
        get { return _nextSubtypeDoesSomeEffect; }
        set { _nextSubtypeDoesSomeEffect = value; }
    }

    private string _lastCardPlayedTitle = "";
    public string LastCardPlayedTitle {
        get { return _lastCardPlayedTitle; }
        set { _lastCardPlayedTitle = value; }
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

    public Hand Hand { get { return Deck.Hand; } }

    public Arsenal Arsenal { get { return Deck.Arsenal; } }

    public RingArea RingArea { get { return Deck.RingArea; } }

    public DeckHandler.CardSet Ringside { get { return Deck.Ringside; } }

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

    public void HasWonByPinVictory()
    {
        _hasWon = true;
    }

    public void HasWonByCountOutVictory()
    {
        _hasWon = true;
    }

    public int SelectCardToPlay()
    {
        PossibleCardsToPlay possibleCards = Deck.GetPossibleCardsToPlay();
        List<string> formattedPossibleCards = Formatter.GetFormattedCardList(
            possibleCards.Cards,
            NextPlay.PlayCard
        );
        int idCardSelected = Formatter.View.AskUserToSelectAPlay(formattedPossibleCards);
        return idCardSelected;
    }

    public void PlayCard(int idCardSelected)
    {
        Play = PlayFactory.GetPlay(idCardSelected, this);
        CheckNextSubtypeIs(Play);
        Play.Start();
        if (Play.Card.Title != _lastCardPlayedTitle)
        {
            ResetNextSubtypeIs();
        }
        _lastCardPlayedTitle = Play.Card.Title;
    }

    public int SelectReversal(Card oponentCard)
    {
        List<string> formattedReversals = Formatter.GetFormattedCardList(
            Deck.GetPossibleReversals(oponentCard, Fortitude).Cards,
            NextPlay.PlayCard
        );
        int reversalSelected = Formatter.View.AskUserToSelectAReversal(
            Superstar.Name,
            formattedReversals
        );
        if (reversalSelected != -1)
        {
            _wantsToReverseACard = true;
        }
        return reversalSelected;
    }

    public int HandleDamage(int damage, bool reversing=false)
    {
        if (Oponent.Superstar.CanUseAbilityBeforeTakingDamage)
        {
            damage = Oponent.Superstar.TakeLessDamage(damage);
        }
        else if (reversing && Superstar.CanUseAbilityBeforeTakingDamage)
        {
            damage = Superstar.TakeLessDamage(damage);
        }
        return damage;
    }

    public Card RecieveDamage()
    {
        return Deck.DrawCardFromArsenalToRingside();
    }

    // public void ReturnACard()
    // {
    //     List<string> formattedHand = Formatter.GetFormattedCardList(Hand.Cards, NextPlay.ShowCards);
    //     int cardId = Formatter.View.AskPlayerToReturnOneCardFromHisHandToHisArsenal(
    //         Superstar.Name,
    //         formattedHand
    //     );
    //     Deck.DrawCardFromHandToArsenalById(cardId);
    // }

    // public void DiscardCards(int quantity)
    // {
    //     for (int i = quantity; i > 0; i--)
    //     {
    //         DiscardACard(i);
    //     }
    // }

    // public void DiscardCardsFromOponentHand(int quantity)
    // {
    //     for (int i = quantity; i > 0; i--)
    //     {
    //         DiscardACardFromOponentHand(i);
    //     }
    // }

    // public void DiscardACard(int iter = 1)
    // {
    //     if (Hand.Any())
    //     {
    //         int discardCardId = SelectCardToDiscard(iter);
    //         Deck.DrawCardFromHandToRingsideById(discardCardId);
    //     }
    // }

    // public void DiscardACardFromOponentHand(int iter = 1)
    // {
    //     if (Oponent.Hand.Any())
    //     {
    //         int discardCardId = SelectCardFromOponentHandToDiscard(iter);
    //         Oponent.Deck.DrawCardFromHandToRingsideById(discardCardId);
    //     }
    // }

    // public void DiscardPossibleCardById(int cardId)
    // {
    //     string cardTitle = Deck.GetPossibleCardsToPlay().GetCard(cardId).Title;
    //     Formatter.View.SayThatPlayerMustDiscardThisCard(Superstar.Name, cardTitle);
    //     Deck.DrawCardFromPossibleCardsToRingsideById(cardId);
    // }

    // private int SelectCardToDiscard(int iter)
    // {
    //     List<string> formattedHand = Formatter.GetFormattedCardList(Hand.Cards, NextPlay.ShowCards);
    //     int discardCardId = Formatter.View.AskPlayerToSelectACardToDiscard(
    //         formattedHand,
    //         Superstar.Name,
    //         Superstar.Name, 
    //         iter
    //     );
    //     return discardCardId;
    // }

    // private int SelectCardFromOponentHandToDiscard(int iter)
    // {
    //     List<string> formattedHand = Formatter.GetFormattedCardList(
    //         Oponent.Hand.Cards,
    //         NextPlay.ShowCards
    //     );
    //     int discardCardId = Formatter.View.AskPlayerToSelectACardToDiscard(
    //         formattedHand,
    //         Oponent.Superstar.Name,
    //         Superstar.Name,
    //         iter
    //     );
    //     return discardCardId;
    // }

    // public void DrawCards(int quantity)
    // {
    //     if (quantity > 0)
    //     {
    //         Formatter.View.SayThatPlayerDrawCards(Superstar.Name, quantity);
    //         for (int i = quantity; i > 0; i--)
    //         {
    //             Deck.DrawCardFromArsenalToHand();
    //         }
    //     }
    // }

    // public void DrawACard()
    // {
    //     Deck.DrawCardFromArsenalToHand();
    //     Formatter.View.SayThatPlayerDrawCards(Superstar.Name, 1);
    // }

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
        Deck.DrawCardFromRingsideToArsenalById(cardId);
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
        Deck.DrawCardFromRingsideToHandById(cardId);
    }

    // public void DrawCardsBecauseOfStunValue(int stunValue, int gapDamage)
    // {
    //     int howManyWillDraw = 0;
    //     if (stunValue > 0 && gapDamage > 0)
    //     {
    //         howManyWillDraw = Formatter.View.AskHowManyCardsToDrawBecauseOfStunValue(
    //             Superstar.Name,
    //             stunValue
    //         );
    //         DrawCards(howManyWillDraw);
    //     }
    // }

    public void ResetPlayProgress()
    {
        _playedAManeuverLast = false;
        _lastDamageInflicted = 0;
        ResetNextSubtypeIs();
        ResetDamageBonusForRestOfTurn();
        _damageBonusForPlayedAfterSomeDamage = 0;
        ResetNextManeuverIsPlusD();
        _lastCardPlayedTitle = "";
    }

    public void CheckNextSubtypeIs(Play play)
    {
        if (!play.Card.Subtypes.Contains(_nextSubtypeDoesSomeEffect.ToString()) || 
            (play.Player.NextSubtypeDoesSomeEffect == Subtype.All))
        {
            ResetNextSubtypeIs();
        }
    }

    private void ResetNextSubtypeIs()
    {
        _nextSubtypeIsPlusD = 0;
        _nextSubtypeReversalIsPlusF = 0;
        _nextSubtypeDoesSomeEffect = Subtype.None;
    }

    private void ResetDamageBonusForRestOfTurn()
    {
        _damageBonusForRestOfTurn = 0;
        _damageBonusForRestOfTurnSubtype = Subtype.None;
    }

    private void ResetNextManeuverIsPlusD()
    {
        _nextManeuverIsPlusD = 0;
        _nextManeuverIsPlusDCounter = 0;
        _nextManeuverIsPlusDSubtype = Subtype.None;
    }
}