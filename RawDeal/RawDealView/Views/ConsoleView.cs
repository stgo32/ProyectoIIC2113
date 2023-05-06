namespace RawDealView.Views;

class ConsoleView : AbstractView
{
    protected override void Write(object text)
    {
        base.Write(text);
        Console.Write(text);
    }

    protected override string GetNextInput()
        => Console.ReadLine();
}