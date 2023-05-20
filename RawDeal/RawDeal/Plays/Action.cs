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
            Console.WriteLine("Jockeying for Position: " + _cardId);
            Player.Deck.DrawCardFromPossibleCardsToRingAreaById(_cardId);
            JockeyingForPositionEffect();
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
            Console.WriteLine("NextGrappleIsPlus4D");
            NextGrappleIsPlus4D();
        }
        else if (selectedEffect == SelectedEffect.NextGrapplesReversalIsPlus8F)
        {
            Console.WriteLine("NextGrapplesReversalIsPlus8F");
            NextGrapplesReversalIsPlus8F();
        }
        Player.PlayedJockeyingForPositionLast = true;
    }

    private void NextGrappleIsPlus4D()
    {
        Player.NextGrappleIsPlus4D = true;
    }

    private void NextGrapplesReversalIsPlus8F()
    {
        Player.NextGrapplesReversalIsPlus8F = true;
    }
}