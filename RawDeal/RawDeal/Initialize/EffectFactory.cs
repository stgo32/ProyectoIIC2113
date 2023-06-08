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
        else if (card.Title == "Bear Hug")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, cardId, player);
        }
        else if (card.Title == "Choke Hold")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, cardId, player);
        }
        else if (card.Title == "Anckle Lock")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, cardId, player);
        }
        else if (card.Title == "Pump Handle Slam")
        {
            effect = new DiscardCards(2, PlayerTarget.Oponent, cardId, player);
        }
        else if (card.Title == "Samoan Drop")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, cardId, player);
        }
        else if (card.Title == "Power Slam")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, cardId, player);
        }
        else if (card.Title == "Boston Crab")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, cardId, player);
        }
        else if (card.Title == "Torture Rack")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, cardId, player);
        }
        else if (card.Title == "Figure Four Leg Lock")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, cardId, player);
        }
        else if (card.Title == "Bulldog")
        {
            effect = new DiscardCards(1, PlayerTarget.Both, cardId, player);
        }
        else if (card.Title == "Kick")
        {
            effect = new ColateralDamage(cardId, player);
        }
        else if (card.Title == "Running Elbow Smash")
        {
            effect = new ColateralDamage(cardId, player);
        }
        else if (card.Title == "Double Leg Takedown")
        {
            effect = new DrawCards(1, PlayerTarget.Self, cardId, player);
        }
        else if (card.Title == "Reverse DDT")
        {
            effect = new DrawCards(1, PlayerTarget.Self, cardId, player);
        }
        else if (card.Title == "Headlock Takedown")
        {
            effect = new DrawCards(1, PlayerTarget.Oponent, cardId, player);
        }
        else if (card.Title == "Standing Side Headlock")
        {
            effect = new DrawCards(1, PlayerTarget.Oponent, cardId, player);
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