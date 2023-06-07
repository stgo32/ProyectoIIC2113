namespace RawDeal;


public class TurnHandler
{
    private int _idPlayerAtTurn { get; set;} = 0;

    private bool _turnHasStarted { get; set;} = false;

    private Game _game;
    public Game Game
    {
        get { return _game; }
        set
        {
            _game = value;
            _game.Players = players;
        }
    }

    private Player[] players { get; set; } = { new Player(), new Player() };

    public Player playerAtTurn { get { return players[_idPlayerAtTurn]; } }

    public Player oponent { get { return playerAtTurn.Oponent; } }
    

    public void SetOrderOfTurns()
    {
        Player playerThatStarts;
        Player playerThatGoesSecond;
        if (players[0].Superstar.SuperstarValue >= players[1].Superstar.SuperstarValue)
        {
            playerThatStarts = players[0];
            playerThatGoesSecond = players[1];
        }
        else
        {
            playerThatStarts = players[1];
            playerThatGoesSecond = players[0];
        }
        Player[] orderedPlayers = { playerThatStarts, playerThatGoesSecond };
        players = orderedPlayers;
    }

    private void BeginTurnSettings()
    {
        playerAtTurn.Superstar.UsedAbilityThisTurn = false;
        playerAtTurn.HasReversedACard = false;
        playerAtTurn.Deck.DrawCardFromArsenalToHand();
        Formatter.View.SayThatATurnBegins(playerAtTurn.Superstar.Name);
        _turnHasStarted = true;
    }

    public void BeginTurn()
    {
        BeginTurnSettings();
        Game.SuperstarCanUseAbilityAtBeginOfTurn();
    } 

    public void EndTurn()
    {
        _turnHasStarted = false;
        playerAtTurn.ResetJockeyingForPosition();
        CheckCountOutVictory();
        if (_idPlayerAtTurn == 0)
        {
            _idPlayerAtTurn = 1;
        }
        else
        {
            _idPlayerAtTurn = 0;
        }
    }

    private void CheckCountOutVictory()
    {
        if (oponent.Arsenal.IsEmpty())
        {
            playerAtTurn.HasWonByCountOutVictory();
        }
        else if (playerAtTurn.Arsenal.IsEmpty())
        {
            oponent.HasWonByCountOutVictory();
        }
    }

    public void SetTurn()
    {   if (APlayerHasReversedACard())
        {
            EndTurn();
        }
        if (!_turnHasStarted)
        {
            BeginTurn();
        }
        Formatter.PrintPlayersInfo(playerAtTurn, oponent);
    }

    public bool APlayerHasReversedACard()
    {
        return oponent.HasReversedACard || playerAtTurn.HasReversedACard;
    }
}