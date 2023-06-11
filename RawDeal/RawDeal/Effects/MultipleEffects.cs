namespace RawDeal.Effects;


public class MultipleEffects : Effect
{
    public override bool CantBeReversed { 
        get { 
            foreach (Effect effect in _effects)
            {
                if (effect.CantBeReversed)
                {
                    return true;
                }
            }
            return false;
        } }

    private Effect[] _effects;

    public MultipleEffects(Effect[] effects, int cardId, Player player) 
                           : base(cardId, player) 
    { 
        _effects = effects;
    }

    public override void Resolve()
    {
        foreach (Effect effect in _effects)
        {
            effect.Resolve();
        }
    }
}