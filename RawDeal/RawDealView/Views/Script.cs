namespace RawDealView.Views;

class Script
{
    private const string InputKeyword = "INPUT: ";
    private string _script = "";

    public void AddInput(string inputFromUser)
        => AddToScript($"{InputKeyword}{inputFromUser}\n");

    public void AddToScript(string message)
        => _script += message;

    public string GetScript()
        => _script;

    public void ExportScript(string outputPath)
        => File.WriteAllText(outputPath, _script);
    
}