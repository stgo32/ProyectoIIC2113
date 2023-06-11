namespace RawDeal.Effects;


public class NextSubtypeReversalIsPlusFortitude : Effect
{
    private string _cardTitle;
    private Subtype _subtype;
    private int _fortitude;

    public override bool CantBeReversed { get { return false; } }
    
    public NextSubtypeReversalIsPlusFortitude(string cardTitle, Subtype subtype, int fortitude, Player player)
                                              : base(player) 
    {
        _cardTitle = cardTitle;
        _subtype = subtype;
        _fortitude = fortitude;
    }

    public override void Resolve()
    {
        _player.NextSubtypeReversalIsPlusF = _fortitude;
        _player.NextSubtypeDoesSomeEffect = _subtype;
        _player.LastCardPlayed = _cardTitle;
    }
}