namespace RawDealView.Views;

class TestingView: AbstractView
{
    private const string InputKeyword = "INPUT: ";
    private readonly string[] _expectedScript;
    private readonly Queue<string> _inputsFromUser = new();

    public TestingView(string pathTestScript)
    {
        _expectedScript = File.ReadAllLines(pathTestScript);
        AddInputsFromUser();
    }

    private void AddInputsFromUser()
    {
        foreach (string line in _expectedScript)
            if(IsInputFromUser(line))
                _inputsFromUser.Enqueue(line.Replace(InputKeyword, ""));
    }

    private bool IsInputFromUser(string line)
        => line.StartsWith(InputKeyword);

    protected override string GetNextInput()
    {
        if(_inputsFromUser.Any()) 
            return _inputsFromUser.Dequeue();
        throw new AggregateException("Tu programa pidió un input pero no hay más inputs del usuario en este test case!");
    }
}