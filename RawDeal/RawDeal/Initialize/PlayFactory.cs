namespace RawDeal.Initialize;


using RawDeal.Plays;


public static class PlayFactory
{
    public static Play GetPlay(int cardId, Player player)
    {
        Card card = player.Deck.GetPossibleCardsToPlay().GetCard(cardId);
        Play play;
        if (card.PlayAs == PlayAs.Maneuver)
        {
            play = new Maneuver(cardId, player);
        }
        else if (card.PlayAs == PlayAs.Action)
        {
            play = new Action(cardId, player);
        }
        else
        {
            throw new Exception("Play not found");
        }
        return play;
    }
}