namespace RawDeal;


using RawDealView;
using RawDealView.Options;


public static class Formatter
{
    private static View _view;
    public static View View { 
        get { return _view; } 
        set { _view = value; }
    }

    public static void PrintPlayersInfo(Player atTurn, Player oponent)
    {
        List<Player> players = new List<Player> {atTurn, oponent};
        List<PlayerInfo> playersInfo = new List<PlayerInfo>();
        foreach (Player player in players)
        {
            PlayerInfo info = new PlayerInfo(
                player.Superstar.Name,
                player.Fortitude,
                player.Hand.Count,
                player.Arsenal.Count
            );
            playersInfo.Add(info);
        }
        View.ShowGameInfo(playersInfo[0], playersInfo[1]);
    }

    public static void ShowCards(Player playerAtTurn, Player oponent)
    {
        CardSet cardsToSee = View.AskUserWhatSetOfCardsHeWantsToSee();
        if (cardsToSee == CardSet.Hand)
        {
            View.ShowCards(GetFormattedCardList(playerAtTurn.Hand, NextPlay.ShowCards));
        }
        else if (cardsToSee == CardSet.RingArea)
        {
            View.ShowCards(GetFormattedCardList(playerAtTurn.RingArea, NextPlay.ShowCards));
        }
        else if (cardsToSee == CardSet.RingsidePile)
        {
            View.ShowCards(GetFormattedCardList(playerAtTurn.Ringside, NextPlay.ShowCards));
        }
        else if (cardsToSee == CardSet.OpponentsRingArea)
        {
            View.ShowCards(GetFormattedCardList(oponent.RingArea, NextPlay.ShowCards));
        }
        else if (cardsToSee == CardSet.OpponentsRingsidePile)
        {
            View.ShowCards(GetFormattedCardList(oponent.Ringside, NextPlay.ShowCards));
        }
    }

    public static List<string> GetFormattedCardList(List<Card> cardList, NextPlay nextPlay)
    {
        List<string> formattedCardList = new List<string>();
        foreach (Card card in cardList)
        {
            string formattedCard = FormatCard(card, nextPlay);
            formattedCardList.Add(formattedCard);
        }
        return formattedCardList;
    }

    public static string FormatCard(Card card, NextPlay nextPlay)
    {
        string formattedCard = "";
        if (nextPlay == NextPlay.PlayCard)
        {
            formattedCard = PlayCardFormat(card);
        }
        else if (nextPlay == NextPlay.ShowCards)
        {
            formattedCard = ShowCardFormat(card);
        }
        return formattedCard;
    }

    private static string PlayCardFormat(Card card)
    {
        ViewablePlayInfo play = new ViewablePlayInfo(card, card.PlayAs.ToUpper());
        string formattedCard = RawDealView.Formatters.Formatter.PlayToString(play);
        return formattedCard;
    }

    private static string ShowCardFormat(Card card)
    {
        string formattedCard = RawDealView.Formatters.Formatter.CardToString(card);
        return formattedCard;
    }

    public static void PrintCardOverturned(Card cardOverturned, int numCard, int damage)
    {
        string infoCardOvertuned = RawDealView.Formatters.Formatter.CardToString(cardOverturned);
        View.ShowCardOverturnByTakingDamage(infoCardOvertuned, numCard, damage);
    }

    public static void PlayCard(Card card, Player player)
    {
        Formatter.PrintCardInfo(card, player);
    }

    public static void PlayCardAsAction(string discardedCardTitle, string superstarName)
    {
        View.SayThatPlayerMustDiscardThisCard(superstarName, discardedCardTitle);
    }

    private static void PrintCardInfo(Card card, Player playerAtTurn)
    {
        string infoCardSelected = FormatCard(card, NextPlay.PlayCard);
        View.SayThatPlayerIsTryingToPlayThisCard(playerAtTurn.Superstar.Name, infoCardSelected);
    }
}