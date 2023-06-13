namespace RawDeal.Reversals;


using RawDeal.Plays;


public class NoEffectReversal : Reversal
{
    private PlayAs _playAs;
    private Subtype _subtype;

    public NoEffectReversal(
        PlayAs playAs, Subtype subtype, string title, List<string> types, List<string> subtypes, 
        string fortitude, string damage, string stunValue, string cardEffect
    ) : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
        _playAs = playAs;
        _subtype = subtype;
    }

    public override bool CanReverse(Card card, int fortitude, Player oponent)
    {
        bool fortitudeRestriction = CalculateFortitudeRestriction(
            fortitude,
            oponent.NextSubtypeReversalIsPlusF
        );
        bool reversalRestriction = card.ContainsSubtype(_subtype) && 
                                   card.PlayAs == _playAs;
        return fortitudeRestriction && reversalRestriction;
    }
}