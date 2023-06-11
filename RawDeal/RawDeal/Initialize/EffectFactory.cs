namespace RawDeal.Initialize;


using RawDeal.Effects;
using RawDeal.Plays;


public static class EffectFactory
{
    public static Effect GetEffect(Card card, int cardId, Player player)
    {
        Effect effect;
        if (card.Title == "Jockeying for Position")
        {
            effect = new JockeyingForPosition(player);
        }
        else if (card.Title == "Head Butt")
        {
            effect = new DiscardCards(1, PlayerTarget.Self, player);
        }
        else if (card.Title == "Arm Drag")
        {
            effect = new DiscardCards(1, PlayerTarget.Self, player);
        }
        else if (card.Title == "Arm Bar")
        {
            effect = new DiscardCards(1, PlayerTarget.Self, player);
        }
        else if (card.Title == "Bear Hug")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, player);
        }
        else if (card.Title == "Choke Hold")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, player);
        }
        else if (card.Title == "Ankle Lock")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, player);
        }
        else if (card.Title == "Spinning Heel Kick")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, player);
        }
        else if (card.Title == "Pump Handle Slam")
        {
            effect = new DiscardCards(2, PlayerTarget.Oponent, player);
        }
        else if (card.Title == "Samoan Drop")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, player);
        }
        else if (card.Title == "Power Slam")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, player);
        }
        else if (card.Title == "Boston Crab")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, player);
        }
        else if (card.Title == "Torture Rack")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, player);
        }
        else if (card.Title == "Figure Four Leg Lock")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, player);
        }
        else if (card.Title == "Bulldog")
        {
            effect = new DiscardCards(1, PlayerTarget.Both, player);
        }
        else if (card.Title == "Kick")
        {
            effect = new ColateralDamage(player);
        }
        else if (card.Title == "Running Elbow Smash")
        {
            effect = new ColateralDamage(player);
        }
        else if (card.Title == "Double Leg Takedown")
        {
            effect = new DrawCards(1, PlayerTarget.Self, true, player);
        }
        else if (card.Title == "Reverse DDT")
        {
            effect = new DrawCards(1, PlayerTarget.Self, true, player);
        }
        else if (card.Title == "Headlock Takedown")
        {
            effect = new DrawCards(1, PlayerTarget.Oponent, false, player);
        }
        else if (card.Title == "Standing Side Headlock")
        {
            effect = new DrawCards(1, PlayerTarget.Oponent, false, player);
        }
        else if (card.Title == "Offer Handshake")
        {
            Effect[] effects = {
                new DrawCards(3, PlayerTarget.Self, true, player),
                new DiscardCards(1, PlayerTarget.Self, player)
            };
            effect = new MultipleEffects(effects, player);
        }
        else if (card.Title == "Press Slam")
        {
            Effect[] effects = {
                new ColateralDamage(player),
                new DiscardCards(2, PlayerTarget.Oponent, player)
            };
            effect = new MultipleEffects(effects, player);
        }
        else if (card.Title == "Fisherman's Suplex")
        {
            Effect[] effects = {
                new ColateralDamage(player),
                new DrawCards(1, PlayerTarget.Self, true, player)
            };
            effect = new MultipleEffects(effects, player);
        }
        else if (card.Title == "DDT")
        {
            Effect[] effects = {
                new ColateralDamage(player),
                new DiscardCards(2, PlayerTarget.Oponent, player)
            };
            effect = new MultipleEffects(effects, player);
        }
        else if (card.Title == "Guillotine Stretch")
        {
            Effect[] effects = {
                new DiscardCards(1, PlayerTarget.Oponent, player),
                new DrawCards(1, PlayerTarget.Self, true, player)
            };
            effect = new MultipleEffects(effects, player);
        }
        else if (card.Title == "Spit At Opponent")
        {
            Effect[] effects = {
                new DiscardCards(1, PlayerTarget.Self, player),
                new DiscardCards(4, PlayerTarget.Oponent, player),
            };
            effect = new MultipleEffects(effects, player);
        }
        else if (card.Title == "Chicken Wing")
        {
            effect = new RecoverCards(2, player);
        }
        else if (card.Title == "Puppies! Puppies!")
        {
            Effect[] effects = {
                new RecoverCards(5, player),
                new DrawCards(2, PlayerTarget.Self, false, player)
            };
            effect = new MultipleEffects(effects, player);
        }
        else if (card.Title == "Recovery")
        {
            Effect[] effects = {
                new RecoverCards(2, player),
                new DrawCards(1, PlayerTarget.Self, false, player)
            };
            effect = new MultipleEffects(effects, player);
        }
        else if (card.Title == "Lionsault")
        {
            effect = new DiscardCards(1, PlayerTarget.Oponent, player);
        }
        else if (card.Title == "Tree of Woe")
        {
            Effect[] effects = {
                new MayNotBeReversed(player),
                new DiscardCards(2, PlayerTarget.Oponent, player)
            };
            effect = new MultipleEffects(effects, player);
        }
        else if (card.Title == "Austin Elbow Smash")
        {
            effect = new MayNotBeReversed(player);
        }
        else if (card.Title == "Irish Whip")
        {
            effect = new NextSubtypePlayedIsPlusDamage(card.Title, Subtype.Strike, 5, player);
        }
        else if (card.Title == "Y2J")
        {
            effect = new ChooseBetweenDrawOrForcingOpponentToDiscard(5, true, player);
        }
        else
        {
            if (card.PlayAs == PlayAs.Action)
            {
                effect = new StandardActionEffect(cardId, player);
            }
            else
            {
                effect = new NoEffect(player);
            }
        }
        Console.WriteLine($"Card: {card.Title}");
        Console.WriteLine($"Card.PlayAs: {card.PlayAs}");
        Console.WriteLine($"EffectFactory.GetEffect: {effect.GetType().Name}");
        return effect;
    }
}