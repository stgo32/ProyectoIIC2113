namespace RawDealView.Options;

public static class ShowCardOptions
{
    private const string Hand = "Mi mano";
    private const string RingArea = "Mi ring area";
    private const string RingsidePile = "Mi ringside pile"; 
    private const string OpponentsRingArea = "El ring area de mi oponente";
    private const string OpponentsRingsidePile = "El ringside pile de mi oponente";

    public static string[] GetOptions()
        => new[] {Hand, RingArea, RingsidePile, OpponentsRingArea, OpponentsRingsidePile};
    
    public static CardSet GetShowCardFromText(string showCard)
    {
        if (showCard == Hand)
            return CardSet.Hand;
        if (showCard == RingArea)
            return CardSet.RingArea;
        if (showCard == RingsidePile)
            return CardSet.RingsidePile;
        if (showCard == OpponentsRingArea)
            return CardSet.OpponentsRingArea;
        if (showCard == OpponentsRingsidePile)
            return CardSet.OpponentsRingsidePile;
        throw new ArgumentException("One of the options is invalid.");
    }

}