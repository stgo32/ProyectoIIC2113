namespace RawDeal.Preconditions;



public class SomeCardHasToBePlayedBefore : Precondition
{
    private string _lastCardNeededTitle;

    public SomeCardHasToBePlayedBefore(string lastCardNeededTitle, Card card) : base(card) 
    {
        _lastCardNeededTitle = lastCardNeededTitle;
    }

    public override bool IsPossibleToPlay(Player player)
    { 
        bool isPossible = fortitudePrecodition(player.Fortitude) &&
                          player.LastCardPlayed == _lastCardNeededTitle;
        return isPossible;
    }
}