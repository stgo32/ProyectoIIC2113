namespace RawDeal;


using RawDeal.Deserialize;
using RawDeal.DeckHandler;
using RawDealView;
using RawDealView.Options;


public class Game
{
    private string _deckFolder;

    private List<Superstar> _superstars { get; set; }
    
    private List<Card> _cards { get; set; }
    
    public Player[] Players { get; set; } = { new Player(), new Player() };
    
    private TurnHandler _turnHandler = new TurnHandler();

    private Player _playerAtTurn { get { return _turnHandler.playerAtTurn; } }

    private Player _oponent { get { return _turnHandler.oponent; } }

    private Player _winner;
    
    public Game(View view, string deckFolder)
    {
        _deckFolder = deckFolder;
        Formatter.View = view;
        _turnHandler.Game = this;
    }

    public void Play()
    {
        bool successInitializingGame = SetGameInitialConfig();
        if (successInitializingGame)
        {
            SetPlayersInitialConfig();
            PlayGame();
        }
    }

    private bool SetGameInitialConfig()
    {
        bool correctDecks = ReadDecks();
        return correctDecks;
    }

    private void DeserializeData()
    {
        _superstars = Deserializer.DeserializeInfoSuperstars();
        _cards = Deserializer.DeserializeInfoCards();
    }

    private bool ReadDecks()
    {
        bool correctDeck = true;
        DeserializeData();
        foreach (Player player in Players)
        {
            ReadDeck(player);
            correctDeck = player.Deck.CheckCorrectness(_superstars);
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
        player.Deck.ReadCardsFromFile(pathDeck, _superstars, _cards);
    }
    
    private void SetPlayersInitialConfig(){
        _turnHandler.SetOrderOfTurns();
        SetOponentsToPlayers();
    }

    private void SetOponentsToPlayers()
    {
        Players[0].Oponent = Players[1];
        Players[1].Oponent = Players[0];
    }

    private void PlayGame()
    {
        while (!APlayerHasWon())
        {
            _turnHandler.SetTurn();
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
                _turnHandler.EndTurn();
            }
            else if (nextPlay == NextPlay.GiveUp)
            {
                GiveUp();
            }
        }
        CongratulateWinner();
    }

    private void ShowCards()
    {
        Formatter.ShowCards(_playerAtTurn, _oponent);
    }

    private NextPlay ChooseNextPlay()
    {   
        NextPlay nextPlay;
        if (_playerAtTurn.Superstar.CanChooseToUseAbility)
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
        HasWon(_oponent);
        _turnHandler.EndTurn();
    }

    private bool APlayerHasWon()
    {
        CheckIfPlayerHasWon();
        return _playerAtTurn.HasWon || _oponent.HasWon;
    }

    private void CheckIfPlayerHasWon()
    {
        if (_playerAtTurn.HasWon)
        {
            HasWon(_playerAtTurn);
        }
        else if (_oponent.HasWon)
        {
            HasWon(_oponent);
        }
        else if (_playerAtTurn.Arsenal.Count == 0 && _oponent.HasReversedACard)
        {
            HasWon(_oponent);
        }
        else if (_oponent.Arsenal.Count == 0 && _oponent.HasReversedACard)
        {
            HasWon(_playerAtTurn);
        }
    }

    private void HasWon(Player winner)
    {
        winner.HasWon = true;
        _winner = winner;
    }

    public void SuperstarCanUseAbilityAtBeginOfTurn()
    {
        if (_playerAtTurn.Superstar.CanUseAbilityAtBeginOfTurn)
        {
            if (_playerAtTurn.NeedToAskToUseAbility)
            {
                AskPlayerToUseAbility(_playerAtTurn);
            }
            if (_playerAtTurn.WantsToUseAbility)
            {
                UseSuperstarAbility();
            }
        }
    }


    private void CongratulateWinner()
    {
        Formatter.View.CongratulateWinner(_winner.Superstar.Name);
    }


    private void PlayCard()
    {
        int idCardSelected = _playerAtTurn.SelectCardToPlay();
        if (idCardSelected != -1)
        {
            _playerAtTurn.PlayCard(idCardSelected);
        }
    }

    private void UseSuperstarAbility()
    {
        Superstar superstar = _playerAtTurn.Superstar;
        superstar.UseAbility();
    }

    private void AskPlayerToUseAbility(Player playerAtTurn)
    {
        string superstarName = playerAtTurn.Superstar.Name;
        bool wantsToUseAbility = Formatter.View.DoesPlayerWantToUseHisAbility(superstarName);
        playerAtTurn.WantsToUseAbility = wantsToUseAbility;
    }
}