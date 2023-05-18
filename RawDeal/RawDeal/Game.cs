namespace RawDeal;


using RawDeal.Deserialize;
using RawDeal.DeckHandler;
using RawDealView;
using RawDealView.Options;


public class Game
{
    private string _deckFolder;
    public List<Superstar> superstars { get; set; }
    public List<Card> cards { get; set; }
    public Player[] players { get; set; } = { new Player(), new Player() };
    private TurnHandler TurnHandler = new TurnHandler();
    private Player playerAtTurn { get { return TurnHandler.playerAtTurn; } }
    private Player oponent { get { return TurnHandler.oponent; } }
    private Player _winner;
    
    public Game(View view, string deckFolder)
    {
        _deckFolder = deckFolder;
        Formatter.View = view;
        TurnHandler.Game = this;
    }

    public void Play()
    {
        bool successInitializingGame = SetGameInitialConfig();
        if (!successInitializingGame)
        {
            return;
        }
        SetPlayersInitialConfig();
        GameLoop();
    }

    private bool SetGameInitialConfig()
    {
        bool correctDecks = ReadDecks();
        return correctDecks;
    }

    private void DeserializeData()
    {
        superstars = Deserializer.DeserializeInfoSuperstars();
        cards = Deserializer.DeserializeInfoCards();
    }

    private bool ReadDecks()
    {
        bool correctDeck = true;
        DeserializeData();
        foreach (Player player in players)
        {
            ReadDeck(player);
            correctDeck = player.Deck.Check(superstars);
            if (!correctDeck) 
            {
                Formatter.View.SayThatDeckIsInvalid();
                break;
            }
        }
        return correctDeck;
    }

    private void ReadDeck(Player player)
    {
        string pathDeck = Formatter.View.AskUserToSelectDeck(_deckFolder);
        player.Deck = new Deck();
        player.Deck.Read(pathDeck, superstars, cards);
    }
    
    private void SetPlayersInitialConfig(){
        TurnHandler.SetOrderOfTurns();
        SetOponentsToPlayers();
    }

    private void SetOponentsToPlayers()
    {
        players[0].Oponent = players[1];
        players[1].Oponent = players[0];
    }

    private void GameLoop()
    {
        while (!APlayerHasWon())
        {
            TurnHandler.SetTurn();
            NextPlay nextPlay = ChooseNextPlay();
            if (nextPlay == NextPlay.ShowCards)
            {
                ShowCards();
            }
            else if (nextPlay == NextPlay.PlayCard)
            {
                PlayCard();
            }
            else if (nextPlay == NextPlay.UseAbility)
            {
                UseSuperstarAbility();
            }
            else if (nextPlay == NextPlay.EndTurn)
            {
                TurnHandler.EndTurn();
            }
            else if (nextPlay == NextPlay.GiveUp)
            {
                GiveUp();
            }
        }
        CongratulateWinner();
        return;
    }

    private void ShowCards()
    {
        Formatter.ShowCards(playerAtTurn, oponent);
    }

    private NextPlay ChooseNextPlay()
    {   
        NextPlay nextPlay;
        if (playerAtTurn.Superstar.CanChooseToUseAbility)
        {
            nextPlay = Formatter.View.AskUserWhatToDoWhenUsingHisAbilityIsPossible();
        }
        else 
        {
            nextPlay = Formatter.View.AskUserWhatToDoWhenHeCannotUseHisAbility();
        }
        return nextPlay;
    }

    private void GiveUp()
    {
        HasWon(oponent);
        TurnHandler.EndTurn();
    }

    private bool APlayerHasWon()
    {
        CheckIfPlayerHasWon();
        return playerAtTurn.HasWon || oponent.HasWon;
    }

    private void CheckIfPlayerHasWon()
    {
        if (oponent.Arsenal.Count == 0)
        {
            HasWon(playerAtTurn);
        }
        else if (playerAtTurn.Arsenal.Count == 0 && oponent.WantsToReverseACard)
        {
            HasWon(oponent);
        }
    }

    private void HasWon(Player winner)
    {
        winner.HasWon = true;
        _winner = winner;
    }

    public void SuperstarCanUseAbilityAtBeginOfTurn()
    {
        if (playerAtTurn.Superstar.CanUseAbilityAtBeginOfTurn)
        {
            if (playerAtTurn.NeedToAskToUseAbility)
            {
                AskPlayerToUseAbility(playerAtTurn);
            }
            if (playerAtTurn.WantsToUseAbility)
            {
                UseSuperstarAbility();
            }
        }
    }
  
    private int SelectCardIdToPlay()
    {
        List<Card> possibleCards = playerAtTurn.Deck.GetPossibleCardsToPlay();
        NextPlay nextPlay = NextPlay.PlayCard;
        List<string> formattedPossibleCards = Formatter.GetFormattedCardList(possibleCards, nextPlay);
        int idCardSelected = Formatter.View.AskUserToSelectAPlay(formattedPossibleCards);
        return idCardSelected;
    }


    private void CongratulateWinner()
    {
        Formatter.View.CongratulateWinner(_winner.Superstar.Name);
    }


    private void PlayCard()
    {
        int idCardSelected = playerAtTurn.SelectCardToPlay();
        if (idCardSelected != -1)
        {
            playerAtTurn.PlayCard(idCardSelected);
        }
    }

    private void UseSuperstarAbility()
    {
        Superstar superstar = playerAtTurn.Superstar;
        superstar.UseAbility();
    }

    private void AskPlayerToUseAbility(Player playerAtTurn)
    {
        string superstarName = playerAtTurn.Superstar.Name;
        bool wantsToUseAbility = Formatter.View.DoesPlayerWantToUseHisAbility(superstarName);
        playerAtTurn.WantsToUseAbility = wantsToUseAbility;
    }
}