namespace RawDeal.Effects;


public class ColateralDamage : Effect
{
    private int _quantity;
    
    private PlayerTarget _target;

    public override bool CantBeReversed { get { return false; } }

    public ColateralDamage(int cardId, Player player) : base(cardId, player) { }

    public override void Resolve()
    {
        Formatter.View.SayThatPlayerDamagedHimself(_player.Superstar.Name, 1);
        Formatter.View.SayThatSuperstarWillTakeSomeDamage(_player.Superstar.Name, 1);
        if (_player.Arsenal.Any())
        {
            Card cardOverturend = _player.RecieveDamage();
            Formatter.PrintCardOverturned(cardOverturend, 1, 1);
        }
        if (_player.Arsenal.IsEmpty())
        {
            Formatter.View.SayThatPlayerLostDueToSelfDamage(_player.Superstar.Name);
            _player.Oponent.HasWonByPinVictory();
        }
    }
}