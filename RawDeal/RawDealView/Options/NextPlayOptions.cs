namespace RawDealView.Options;

static class NextPlayOptions
{
    private const string UseAbility = "Usar habilidad";
    private const string ShowCards = "Ver cartas";
    private const string PlayCard = "Jugar carta"; 
    private const string EndTurn = "Terminar turno";
    private const string GiveUp = "Rendirse";

    public static string[] GetOptionsWithoutSuperstarAbility()
        => new[] {ShowCards, PlayCard, EndTurn, GiveUp};

    public static string[] GetAllPossibleOptions()
        => new[] {ShowCards, PlayCard, UseAbility, EndTurn, GiveUp};

    public static NextPlay GetNextPlayFromText(string nextPlay)
    {
        if (nextPlay == UseAbility)
            return NextPlay.UseAbility;
        if (nextPlay == ShowCards)
            return NextPlay.ShowCards;
        if (nextPlay == PlayCard)
            return NextPlay.PlayCard;
        if (nextPlay == EndTurn)
            return NextPlay.EndTurn;
        if (nextPlay == GiveUp)
            return NextPlay.GiveUp;
        throw new ArgumentException("One of the options is invalid.");
    }

}