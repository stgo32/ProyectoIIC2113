namespace RawDeal.Preconditions;


using RawDeal.DeckHandler;

public class NeedsSomeNumberOfCardsInHand : Precondition
{
    private int _numberOfCards;

    public NeedsSomeNumberOfCardsInHand(int numberOfCards, Card card) : base(card)
    {
        _numberOfCards = numberOfCards;
    }

    public override bool IsPossibleToPlay(Player player)
    { 
        bool isPossible = fortitudePrecodition(player.Fortitude) &&
                          PlayAsPrecondition() &&
                          NeedsSomeNumberOfCardsInHandPrecondition(player.Hand);
        return isPossible;
    }

    private bool NeedsSomeNumberOfCardsInHandPrecondition(Hand hand)
    {
        return _numberOfCards <= hand.Count();
    }
}