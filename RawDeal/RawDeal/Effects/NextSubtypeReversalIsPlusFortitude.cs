namespace RawDeal.Effects;


// using RawDeal;
using RawDealView.Options;


public class NextSubtypeReversalIsPlusFortitude : Effect
{
    private Card _card;
    private Subtype _subtype;
    private int _fortitude;

    public override bool CantBeReversed { get { return false; } }
    
    public NextSubtypeReversalIsPlusFortitude(Card card, Subtype subtype, int fortitude, Player player)
                                               : base(player) 
    {
        _card = card;
        _subtype = subtype;
        _fortitude = fortitude;
    }

    public override void Resolve()
    {
        _player.NextSubtypeReversalIsPlusF = _fortitude;
        _player.NextSubtypeDoesSomeEffect = _subtype;
        _player.LastCardPlayed = _card.Title;
    }
}