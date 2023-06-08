namespace RawDeal.Initialize;


using RawDeal.Effects;
using RawDeal.Plays;


public static class EffectFactory
{
    public static Effect GetEffect(int cardId, Player player)
    {
        Card card = player.Deck.GetPossibleCardsToPlay().GetCard(cardId);
        Effect effect;
        if (card.Title == "Jockeying for Position")
        {
            effect = new JockeyingForPosition(cardId, player);
        }
        else if (card.Title == "Head Butt")
        {
            effect = new DiscardCards(1, PlayerTarget.Self, cardId, player);
        }
        else if (card.Title == "Arm Drag")
        {
            effect = new DiscardCards(1, PlayerTarget.Self, cardId, player);
        }
        else if (card.Title == "Arm Bar")
        {
            effect = new DiscardCards(1, PlayerTarget.Self, cardId, player);
        }
        else
        {
            if (card.PlayAs == PlayAs.Action)
            {
                effect = new StandardActionEffect(cardId, player);
            }
            else
            {
                effect = new NoEffect(cardId, player);
            }
        }
        Console.WriteLine($"Card: {card.Title}");
        Console.WriteLine($"Card.PlayAs: {card.PlayAs}");
        Console.WriteLine($"EffectFactory.GetEffect: {effect.GetType().Name}");
        return effect;
    }
}