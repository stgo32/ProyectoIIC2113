namespace RawDealView;

public class PlayerInfo
{
    private readonly string _superstarName;
    private readonly int _fortitudeRating;
    private readonly int _numberOfCardsInHand;
    private readonly int _numberOfCardsInArsenal;

    public PlayerInfo(string superstarName, int fortitudeRating, int numberOfCardsInHand, int numberOfCardsInArsenal)
    {
        _superstarName = superstarName;
        _fortitudeRating = fortitudeRating;
        _numberOfCardsInHand = numberOfCardsInHand;
        _numberOfCardsInArsenal = numberOfCardsInArsenal;
    }

    public override string ToString()
        => $"{_superstarName}: {_fortitudeRating}F, tiene {_numberOfCardsInHand} cartas en la mano y {_numberOfCardsInArsenal} en el arsenal.";
}