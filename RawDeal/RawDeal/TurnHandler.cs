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
            _game.players = players;
        }
    }
    private Player[] players { get; set; } = { new Player(), new Player() };
    public Player playerAtTurn { get { return players[_idPlayerAtTurn]; } }
    public Player oponent { get { return playerAtTurn.Oponent; } }

    public void SetOrderOfTurns()
    {
        Player playerThatStarts;
        Player playerThatGoesSecond;
        if (players[0].GetSuperstarValue() >= players[1].GetSuperstarValue())
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
        playerAtTurn.DrawCardFromArsenalToHand();
        Formatter.View.SayThatATurnBegins(playerAtTurn.GetSuperstarName());
    }

    public void BeginTurn()
    {
        BeginTurnSettings();
        Game.SuperstarCanUseAbilityAtBeginOfTurn();
    } 

    public void EndTurn()
    {
        _turnHasStarted = false;
        if (_idPlayerAtTurn == 0)
        {
            _idPlayerAtTurn = 1;
        }
        else
        {
            _idPlayerAtTurn = 0;
        }
    }

    public void SetTurn()
    {
        if (!_turnHasStarted)
        {
            BeginTurn();
            _turnHasStarted = true;
        }
        Formatter.PrintPlayersInfo(playerAtTurn, oponent);
    }
}