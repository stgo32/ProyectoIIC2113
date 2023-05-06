namespace RawDeal;

using RawDealView.Options;

public class StoneCold:Superstar
{
    public override Player Player { get; set; }
    public override bool CanChooseToUseAbility { 
        get {
            if (UsedAbilityThisTurn) 
            {
                return false;
            }
            return Player.Arsenal.Count >= 1;
        }
        set {
            CanChooseToUseAbility = value;
        }
    }
    public override bool UsedAbilityThisTurn { get; set; } = false;
    public override bool CanUseAbilityAtBeginOfTurn { get { return false; } }
    public override bool CanUseAbilityBeforeTakingDamage { get { return false; } }


    public override void UseAbility()
    {
        Formatter.View.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        DrawACard();
        ReturnACard();
        UsedAbilityThisTurn = true;
    }

    public override int TakeLessDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
    private void DrawACard()
    {
        Player.DrawCardFromArsenalToHand();
        Formatter.View.SayThatPlayerDrawCards(Name, 1);
    }

    private void ReturnACard()
    {
        List<string> formattedHand = Formatter.GetFormattedCardList(Player.Hand, NextPlay.ShowCards);
        int cardId = Formatter.View.AskPlayerToReturnOneCardFromHisHandToHisArsenal(Name, formattedHand);
        Player.DrawCardFromHandToArsenalById(cardId);
    }

}