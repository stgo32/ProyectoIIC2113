namespace RawDeal;

using RawDealView.Options;

public class Undertaker:Superstar
{
    public override Player Player { get; set; }
    public override bool UsedAbilityThisTurn { get; set; } = false;
    public override bool CanChooseToUseAbility { 
        get {
            if (UsedAbilityThisTurn) 
            {
                return false;
            }
            return Player.Hand.Count >= 2;
        }
        set {
            CanChooseToUseAbility = value;
        }
    }
    public override bool CanUseAbilityAtBeginOfTurn { get { return false; } }
    public override bool CanUseAbilityBeforeTakingDamage { get { return false; } }

    public override void UseAbility()
    {
        Formatter.View.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        // DiscardTwoCards();
        Player.DiscardCards(2);
        // DrawACard();
        Player.RetrieveACard();
        UsedAbilityThisTurn = true;
    }

    public override int TakeLessDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    private void DrawACard()
    {
        List<string> formattedRingside = Formatter.GetFormattedCardList(Player.Ringside, NextPlay.ShowCards);
        int cardId = Formatter.View.AskPlayerToSelectCardsToPutInHisHand(Name, 1, formattedRingside);
        Player.Deck.DrawCardFromRingsideToHandById(cardId);
    }
}