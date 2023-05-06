using RawDealView.Options;
using RawDealView.Utils;
using RawDealView.Views;

namespace RawDealView;

public class View
{
    private readonly AbstractView _view;
    private readonly PlayFormatter _playFormatter = new();
    private readonly CardFormatter _cardFormatter = new();

    public static View BuildConsoleView()
        => new (new ConsoleView());
    
    public static View BuildTestingView(string pathTestScript)
        => new (new TestingView(pathTestScript));

    public static View BuildManualTestingView(string pathTestScript)
        => new (new ManualTestingView(pathTestScript));

    private View(AbstractView newView)
        => _view = newView;
    
    public void ExportScript(string path)
        => _view.ExportScript(path);

    public string[] GetScript()
        => _view.GetScript();

    public void SayThatPlayerIsGoingToUseHisAbility(string superstarName, string superstarAbility)
    {
        ShowDivision();
        _view.WriteLine($"{superstarName} usa su habilidad: {superstarAbility}"); 
    }

    private void ShowDivision()
        => _view.WriteLine("--------------------------------------------");

    public int AskPlayerToReturnOneCardFromHisHandToHisArsenal(string superstarName, List<string> cardsInHand)
    {
        _view.WriteLine($"{superstarName} elige una carta para devolver a tu arsenal.");
        return AskUserToSelectACard(cardsInHand);
    }

    private int AskUserToSelectACard(List<string> cards)
    {
        ShowCards(cards);
        return AskUserToSelectANumber(0, cards.Count - 1);
    }

    private int AskUserToSelectANumber(int minValue, int maxValue)
    {
        _view.WriteLine($"(Ingresa un número entre {minValue} y {maxValue})");
        int value;
        bool wasParsePossible;
        do
        {
            string? userInput = _view.ReadLine();
            wasParsePossible = int.TryParse(userInput, out value);
        } while (!wasParsePossible || IsValueOutsideTheValidRange(minValue, value, maxValue));

        return value;
    }

    private bool IsValueOutsideTheValidRange(int minValue, int value, int maxValue)
        => value < minValue || value > maxValue;

    public void ShowCards(List<string> cards)
        => ShowItems(_cardFormatter, cards);

    private void ShowItems(ItemFormatter itemFormatter, List<string> items)
    {
        string message = itemFormatter.ConvertToString(items);
        _view.WriteLine(message);
    }

    public int AskPlayerToSelectCardsToRecover(string superstarName, int numCards, List<string> cards)
    {
        ShowEmptyLineAndDivision();
        _view.WriteLine($"{superstarName} debe elegir {numCards} carta(s) para devolver de su ringside a su arsenal. ¿Qué carta quieres recuperar?");
        return AskUserToSelectACard(cards);
    }

    private void ShowEmptyLineAndDivision()
    {
        _view.WriteLine("");
        ShowDivision();
    }

    public int AskPlayerToSelectCardsToPutInHisHand(string superstarName, int numCards, List<string> cards)
    {
        ShowEmptyLineAndDivision();
        _view.WriteLine($"{superstarName} debe elegir {numCards} carta(s) para poner en su mano. ¿Qué carta quieres obtener?");
        return AskUserToSelectACard(cards);
    }

    public int AskPlayerToSelectACardToDiscard(List<string> cardsThatMightBeDiscarded, string superstarWhoMustDiscard,
        string superstarWhoDecidesWhatToDiscard, int totalCardsToDiscard)
    {
        _view.WriteLine($"{superstarWhoMustDiscard} debe descartar {totalCardsToDiscard} carta(s).");
        _view.WriteLine($"{superstarWhoDecidesWhatToDiscard} elige una carta para descartar.");
        return AskUserToSelectACard(cardsThatMightBeDiscarded);
    }

    public bool DoesPlayerWantToUseHisAbility(string superstarName)
    {
        _view.WriteLine($"{superstarName}: ¿Quieres usar tu habilidad? (Y/N).");
        string answer = "";
        while (answer != "Y" && answer != "N")
            answer = _view.ReadLine();
        return answer == "Y";
    }

    public void SayThatPlayerMustDiscardThisCard(string superstarName, string cardToDiscard)
        => _view.WriteLine($"{superstarName} descarta {cardToDiscard}.");

    public void SayThatPlayerDrawCards(string superstarName, int numOfCardsToDraw)
        => _view.WriteLine($"{superstarName} roba {numOfCardsToDraw} carta(s).");
    
    public void SayThatPlayerIsTryingToPlayThisCard(string superstarName, string playInfo)
    {
        ShowDivision();
        _view.WriteLine($"{superstarName} intenta jugar la siguiente carta:\n{playInfo}");
    }
    
    public void SayThatPlayerReversedTheCard(string superstarName, string reversalInfo)
    {
        ShowDivision();
        _view.WriteLine($"{superstarName} revierte la carta usando:\n{reversalInfo}");
    }

    public void SayThatCardWasReversedByDeck(string superstarName)
    {
        ShowDivision();
        _view.WriteLine($"{superstarName} revierte la carta desde el mazo.");
    }

    public int AskHowManyCardsToDrawBecauseOfStunValue(string superstarName, int stunValue)
    {
        ShowDivision();
        _view.WriteLine(
            $"{superstarName} puede robar hasta {stunValue} cartas debido al stun value. ¿Cuántas cartas quieres robar?");
        return AskUserToSelectANumber(0, stunValue);
    }

    public void SayThatPlayerSuccessfullyPlayedACard()
    {
        ShowDivision();
        _view.WriteLine("La carta fue exitosamente jugada.");
    }

    public void SayThatOpponentWillTakeSomeDamage(string opponentsSuperstarName, int damageToBeReceived)
        => _view.WriteLine($"{opponentsSuperstarName} recibe {damageToBeReceived} de daño.");

    public void ShowCardOverturnByTakingDamage(string overturnedCardInfo, int currentDamage, int totalDamage)
        => _view.WriteLine($"-------------------------------- {currentDamage}/{totalDamage}\n{overturnedCardInfo}");
    
    public void SayThatDeckIsInvalid()
        => _view.WriteLine($"El mazo ingresado es inválido.");
    
    public void SayThatATurnBegins(string superstarName)
    {
        ShowDivision();
        _view.WriteLine($"Comienza el turno de {superstarName}.");
    }

    public void ShowGameInfo(PlayerInfo player1, PlayerInfo player2)
    {
        ShowDivision();
        _view.WriteLine(player1);
        _view.WriteLine(player2);
        ShowDivision();
    }

    public int AskUserToSelectAReversal(string superstarName, List<string> applicableReversals)
    {
        if (applicableReversals.Any())
        {
            ShowDivision();
            _view.WriteLine($"{superstarName} tiene la opción de revertir la carta:");
            return AskUserToSelectAPlay(applicableReversals);
        }

        return -1;
    }

    public int AskUserToSelectAPlay(List<string> plays)
    {
        ShowItems(_playFormatter, plays);
        if (plays.Any())
            return AskUserToSelectANumber(-1, plays.Count - 1);
        return -1;
    }
    
    public void CongratulateWinner(string winnersName)
    {
        _view.WriteLine("\n--------------------------------------------");
        _view.WriteLine($"Felicidades, gana {winnersName}.");
    }

    public string AskUserToSelectDeck(string folder)
    {
        string[] decks = Directory.GetFiles(folder, "*.txt");
        _view.WriteLine("-------------------------");
        _view.WriteLine("Elige un mazo");
        Array.Sort(decks);
        return AskUserToSelectOption(decks);
    }

    public NextPlay AskUserWhatToDoWhenUsingHisAbilityIsPossible()
    {
        string[] options = NextPlayOptions.GetAllPossibleOptions();
        return AskUserWhatToDo(options);
    }

    private NextPlay AskUserWhatToDo(string[] options)
    {
        string selectedOption = AskUserToSelectOption(options);
        return NextPlayOptions.GetNextPlayFromText(selectedOption);
    }

    public NextPlay AskUserWhatToDoWhenHeCannotUseHisAbility()
    {
        string[] options = NextPlayOptions.GetOptionsWithoutSuperstarAbility();
        return AskUserWhatToDo(options);
    }

    public SelectedEffect AskUserToSelectAnEffectForJockeyForPosition(string superstarName)
        => AskUserToSelectAnEffect(superstarName,
            new[] { SelectedEffect.NextGrappleIsPlus4D, SelectedEffect.NextGrapplesReversalIsPlus8F });

    private SelectedEffect AskUserToSelectAnEffect(string superstarName, SelectedEffect[] possibleEffects)
    {
        ShowDivision();
        _view.WriteLine($"Como resultado, {superstarName} debe elegir uno de los siguientes efectos:");
        string[] options = SelectedEffectOptions.GetOptions(possibleEffects);
        string selectedOption = AskUserToSelectOption(options);
        return SelectedEffectOptions.GetSelectedEffectFromText(selectedOption);
    }

    public CardSet AskUserWhatSetOfCardsHeWantsToSee()
    {
        string[] options = ShowCardOptions.GetOptions();
        string selectedOption = AskUserToSelectOption(options);
        return ShowCardOptions.GetShowCardFromText(selectedOption);
    }

    private string AskUserToSelectOption(string[] options)
    {
        ShowOptions(options);
        int idOption = AskUserToSelectANumber(1, options.Length) - 1;
        return options[idOption];
    }

    private void ShowOptions(string[] options)
    {
        for (int i = 0; i < options.Length; i++)
        {
            string normalizedOption = ReplaceBackslashesWithSlashesToFixPathIncompatibilitiesWithWindows(options[i]);
            _view.WriteLine($"{i+1}- {normalizedOption}");
        }
    }

    private string ReplaceBackslashesWithSlashesToFixPathIncompatibilitiesWithWindows(string path)
        => path.Replace("\\", "/");
    
}