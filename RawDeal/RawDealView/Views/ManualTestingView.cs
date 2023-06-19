namespace RawDealView.Views;

class ManualTestingView:TestingView
{
    private const string EndOfFileString = "[EndOfFile]";
    
    private string[] _expectedScript;
    private int _currentLine;
    private bool _isOutputCorrectSoFar = true;
    
    public ManualTestingView(string pathTestScript) : base(pathTestScript)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        _expectedScript = File.ReadAllLines(pathTestScript);
        _currentLine = 0;
    }
    
    protected override void Write(object text)
    {
        if (_isOutputCorrectSoFar)
            CheckIfCurrentOutputIsAsExpected(text);
        base.Write(text);
        Console.Write(text);
    }

    private void CheckIfCurrentOutputIsAsExpected(object text)
    {
        string normalizedText = GetNormalizedTest(text.ToString());
        string[] lines = normalizedText.Split("\n");
        CheckThatLinesMatchTheExpectedOutput(lines);
    }

    private void CheckThatLinesMatchTheExpectedOutput(string[] lines)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            if (IsThisLineDifferentFromTheExpectedValue(lines[i]))
            {
                IndicateThatThereIsAnErrorInThisLineAndChangeTheColorOfTheConsole();
                break;
            }
            _currentLine++;
        }
    }

    private bool IsThisLineDifferentFromTheExpectedValue(string line)
        => GetExpectedLine() != line;

    private string GetExpectedLine()
    {
        if (IsTheEndOfTheExpectedScript())
            return EndOfFileString;
        return _expectedScript[_currentLine];
    }

    private bool IsTheEndOfTheExpectedScript()
        => _currentLine == _expectedScript.Length;
    
    private void IndicateThatThereIsAnErrorInThisLineAndChangeTheColorOfTheConsole()
    {
        _isOutputCorrectSoFar = false;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ERROR] el valor esperado acá era: \"{GetExpectedLine()}\"");
    }

    private string GetNormalizedTest(string text)
        => text.Remove(text.Length-1);

    protected override string GetNextInput()
    {
        if (_isOutputCorrectSoFar)
            return GetNextInputFromTestFile();
        return GetNextInputFromUser();
    }

    private string GetNextInputFromTestFile()
    {
        string nextInput = base.GetNextInput();
        Console.Write($"[INPUT TEST]: {nextInput}");
        Console.ReadLine();
        _currentLine++;
        return nextInput;
    }

    private string GetNextInputFromUser()
    {
        Console.Write($"[INPUT MANUAL]: ");
        return Console.ReadLine();
    }
}