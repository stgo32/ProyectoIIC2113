namespace RawDeal.Preconditions;


using RawDeal.Plays;


public abstract class Precondition
{
    protected Card _card;

    public Precondition(Card card)
    {
        _card = card;
    }

    public abstract bool IsPossibleToPlay(Player player);

    protected virtual bool fortitudePrecodition(int fortitude)
    {
        return _card.GetFortitude() <= fortitude;
    }

    protected virtual bool PlayAsPrecondition()
    {
        return _card.PlayAs == PlayAs.Action || _card.PlayAs == PlayAs.Maneuver;
    }
}