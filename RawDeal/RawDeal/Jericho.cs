namespace RawDeal;

using RawDealView.Options;

public class Jericho:Superstar
{
    public override Player Player { get; set; }
    public override bool UsedAbilityThisTurn { get; set; } = false;
    public override bool CanChooseToUseAbility { 
        get {
            if (UsedAbilityThisTurn) 
            {
                return false;
            }
            return Player.Hand.Count >= 1;
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
        DiscardACard(Player);
        DiscardACard(Player.Oponent);
        UsedAbilityThisTurn = true;
    }
    public override int TakeLessDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    private int SelectCardToDiscard(Player player)
    {
        List<string> formattedHand = Formatter.GetFormattedCardList(player.Hand, NextPlay.ShowCards);
        string superstarName = player.Superstar.Name;
        int discardCardId = Formatter.View.AskPlayerToSelectACardToDiscard(formattedHand, superstarName, superstarName, 1);
        return discardCardId;
    }

    private void DiscardACard(Player player)
    {
        int discardCardId = SelectCardToDiscard(player);
        player.DrawCardFromHandToRingsideById(discardCardId);
    }

}