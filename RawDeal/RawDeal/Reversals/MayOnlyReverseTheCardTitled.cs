namespace RawDeal.Reversals;


using RawDeal.Plays;


public class MayOnlyReverseTheCardTitled : Reversal
{
    private PlayAs _playAs;
    private string _cardToReverseTitle;

    public MayOnlyReverseTheCardTitled(
        string cardToReverseTitle, PlayAs playAs, string title, List<string> types, 
        List<string> subtypes, string fortitude, string damage, string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
        _playAs = playAs;
        _cardToReverseTitle = cardToReverseTitle;
    }

    public override bool CanReverse(Card card, int fortitude, Player oponent)
    {
        bool fortitudeRestriction = CalculateFortitudeRestriction(
            fortitude,
            oponent.NextSubtypeReversalIsPlusF
        );
        bool reversalRestriction = card.PlayAs == _playAs && 
                                   card.Title == _cardToReverseTitle;
        return fortitudeRestriction && reversalRestriction;
    }
}