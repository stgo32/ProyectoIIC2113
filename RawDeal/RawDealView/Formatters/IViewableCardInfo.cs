namespace RawDealView.Formatters;

public interface IViewableCardInfo
{
    string Title { get; }
    string Fortitude { get; }
    string Damage { get; }
    string StunValue { get; }
    List<string> Types { get; }
    List<string> Subtypes { get; }
    string CardEffect { get; }
}