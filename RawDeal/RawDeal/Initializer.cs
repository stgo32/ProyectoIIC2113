namespace RawDeal;


using RawDeal.Superstars;
using RawDeal.Reversals;
using RawDeal.Plays;


public static class Initializer
{
    public static Reversal InitReversalByTitle(Card card)
    {
        string reversalTitle = card.Title;
        Reversal reversal;
        if (reversalTitle == "Break the Hold")
        {
            reversal = new BreakTheHole(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                        card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "Escape Move")
        {
            reversal = new EscapeMove(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                      card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "No Chance in Hell")
        {
            reversal = new NoChanceInHell(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                          card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "Step Aside")
        {
            reversal = new StepAside(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                     card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "Rolling Takedown")
        {
            reversal = new RollingTakedown(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                           card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "Knee to the Gut")
        {
            reversal = new KneeToTheGut(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                        card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "Elbow to the Face")
        {
            reversal = new ElbowToTheFace(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                          card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "Manager Interferes")
        {
            reversal = new ManagerInterferes(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                             card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "Chyna Interferes")
        {
            reversal = new ChynaInterferes(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                           card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "Clean Break")
        {
            reversal = new CleanBreak(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                      card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "Jockeying for Position")
        {
            reversal = new JockeyingForPosition(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                                card.Damage, card.StunValue, card.CardEffect);
        }
        else
        {
            throw new Exception("Reversal not found");
        }
        reversal.PlayAs = "Reversal";
        return reversal;
    }

    public static Superstar InitSuperstarByName(Superstar superstarInfo)
    {
        string superstarName = superstarInfo.Name;
        Superstar superstar;
        if (superstarName == "STONE COLD STEVE AUSTIN")
        {
            superstar = new StoneCold();
        }
        else if (superstarName == "THE UNDERTAKER")
        {
            superstar = new Undertaker();
        }
        else if (superstarName == "MANKIND")
        {
            superstar = new ManKind();
        }
        else if (superstarName == "HHH")
        {
            superstar = new HHH();
        }
        else if (superstarName == "THE ROCK")
        {
            superstar = new TheRock();
        }
        else if (superstarName == "KANE")
        {
            superstar = new Kane();
        }
        else if (superstarName == "CHRIS JERICHO")
        {
            superstar = new Jericho();
        }
        else
        {
            throw new Exception("Superstar not found");
        }
        return superstar;
    }

    public static Play InitPlayByType(int cardId, Player player)
    {
        Card card = player.Deck.GetPossibleCardsToPlay()[cardId];
        Play play;
        if (card.PlayAs == "Maneuver")
        {
            play = new Maneuver(cardId, player);
        }
        else if (card.PlayAs == "Action")
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