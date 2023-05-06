namespace RawDealView.Formatters;

public static class Formatter
{
    public static string PlayToString(IViewablePlayInfo playInfo)
        => $"[{playInfo.PlayedAs}] {CardToString(playInfo.CardInfo)}";

    public static string CardToString(IViewableCardInfo cardInfo)
    {
        string cardStr = GetFormattedTitle(cardInfo);
        cardStr += GetFormattedCardStats(cardInfo);
        cardStr += GetFormattedCardTypesAndSubtypes(cardInfo);
        cardStr += GetFormattedCardEffect(cardInfo);
        return cardStr;
    }

    private static string GetFormattedTitle(IViewableCardInfo cardInfo)
        => "*" + cardInfo.Title + "*";
    
    private static string GetFormattedCardStats(IViewableCardInfo cardInfo)
    {
        string info = ". Info: ";
        info += $"{cardInfo.Fortitude}F/{cardInfo.Damage}D";
        if (cardInfo.StunValue != "0")
            info += $"/{cardInfo.StunValue}SV";
        return info;
    }

    private static string GetFormattedCardTypesAndSubtypes(IViewableCardInfo cardInfo)
    {
        string types = String.Join('/', cardInfo.Types);
        if(cardInfo.Subtypes.Any())
            types += ", " + String.Join('/', cardInfo.Subtypes);
        return ", " + types + ".";
    }

    private static string GetFormattedCardEffect(IViewableCardInfo cardInfo)
    {
        if(cardInfo.CardEffect.Any())
            return $" Effect: {cardInfo.CardEffect}";
        return "";
    }
    
}