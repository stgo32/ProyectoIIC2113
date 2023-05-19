namespace RawDeal.Plays;


using RawDealView.Options;


public class Action : Play
{
    public Action(int cardId, Player player) : base(cardId, player) { }

    public override void Start()
    {
        Formatter.PlayCard(Card, Player);
        if (!IsBeingReversedByHand())
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        base.Attack();
        if (Card.Title == "Jockeying for Position")
        {
            JockeyingForPositionEffect();
            Player.Deck.DrawCardFromPossibleCardsToRingAreaById(_cardId);
        }
        else
        {
            Player.DiscardPossibleCardById(_cardId);
            Player.DrawACard();
        }
    }

    private void JockeyingForPositionEffect()
    {
        SelectedEffect selectedEffect = Formatter.View.AskUserToSelectAnEffectForJockeyForPosition(
            Player.Superstar.Name
        );
        if (selectedEffect == SelectedEffect.NextGrappleIsPlus4D)
        {
            NextGrappleIsPlus4D();
        }
        else if (selectedEffect == SelectedEffect.NextGrapplesReversalIsPlus8F)
        {
            NextGrapplesReversalIsPlus8F();
        }
    }

    private void NextGrappleIsPlus4D()
    {
    }

    private void NextGrapplesReversalIsPlus8F()
    {
    }
}