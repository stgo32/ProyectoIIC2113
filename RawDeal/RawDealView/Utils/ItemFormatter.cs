namespace RawDealView.Utils;

abstract class ItemFormatter
{
    private List<string> _message;
    private List<string> _items;
    
    public string ConvertToString(List<string> items)
    {
        ResetMessageAndItems(items);
        AddDivisionToMessage();
        AddItemsToMessage();
        AddTextIndicatingThatTheListOfItemsIsEmptyIfNeeded();
        AddDivisionToMessage();
        return string.Join("\n", _message);
    }

    private void ResetMessageAndItems(List<string> items)
    {
        _message = new List<string>();
        _items = items;
    }

    private void AddDivisionToMessage()
        => _message.Add("--------------------------------------------");

    private void AddItemsToMessage()
    {
        for(int i = 0; i < _items.Count; i++)
            _message.Add($"{i}- {_items[i]}");
    }

    private void AddTextIndicatingThatTheListOfItemsIsEmptyIfNeeded()
    {
        if (!_items.Any())
            _message.Add(GetTextIndicatingThatTheListOfItemsIsEmpty());
    }

    protected abstract string GetTextIndicatingThatTheListOfItemsIsEmpty();

}