namespace RawDeal;


using RawDeal.Superstars;
using RawDeal.Reversals;
using RawDeal.Plays;
using RawDeal.Maneuvers;


public static class Initializer
{
    // public static Reversal InitReversalByTitle(Card card)
    // {
    //     string reversalTitle = card.Title;
    //     Reversal reversal;
    //     if (reversalTitle == "Break the Hold")
    //     {
    //         reversal = new BreakTheHole(card.Title, card.Types, card.Subtypes, card.Fortitude,
    //                                     card.Damage, card.StunValue, card.CardEffect);
    //     }
    //     else if (reversalTitle == "Escape Move")
    //     {
    //         reversal = new EscapeMove(card.Title, card.Types, card.Subtypes, card.Fortitude,
    //                                   card.Damage, card.StunValue, card.CardEffect);
    //     }
    //     else if (reversalTitle == "No Chance in Hell")
    //     {
    //         reversal = new NoChanceInHell(card.Title, card.Types, card.Subtypes, card.Fortitude,
    //                                       card.Damage, card.StunValue, card.CardEffect);
    //     }
    //     else if (reversalTitle == "Step Aside")
    //     {
    //         reversal = new StepAside(card.Title, card.Types, card.Subtypes, card.Fortitude,
    //                                  card.Damage, card.StunValue, card.CardEffect);
    //     }
    //     else if (reversalTitle == "Rolling Takedown")
    //     {
    //         reversal = new RollingTakedown(card.Title, card.Types, card.Subtypes, card.Fortitude,
    //                                        card.Damage, card.StunValue, card.CardEffect);
    //     }
    //     else if (reversalTitle == "Knee to the Gut")
    //     {
    //         reversal = new KneeToTheGut(card.Title, card.Types, card.Subtypes, card.Fortitude,
    //                                     card.Damage, card.StunValue, card.CardEffect);
    //     }
    //     else if (reversalTitle == "Elbow to the Face")
    //     {
    //         reversal = new ElbowToTheFace(card.Title, card.Types, card.Subtypes, card.Fortitude,
    //                                       card.Damage, card.StunValue, card.CardEffect);
    //     }
    //     else if (reversalTitle == "Manager Interferes")
    //     {
    //         reversal = new ManagerInterferes(card.Title, card.Types, card.Subtypes, card.Fortitude,
    //                                          card.Damage, card.StunValue, card.CardEffect);
    //     }
    //     else if (reversalTitle == "Chyna Interferes")
    //     {
    //         reversal = new ChynaInterferes(card.Title, card.Types, card.Subtypes, card.Fortitude,
    //                                        card.Damage, card.StunValue, card.CardEffect);
    //     }
    //     else if (reversalTitle == "Clean Break")
    //     {
    //         reversal = new CleanBreak(card.Title, card.Types, card.Subtypes, card.Fortitude,
    //                                   card.Damage, card.StunValue, card.CardEffect);
    //     }
    //     else if (reversalTitle == "Jockeying for Position")
    //     {
    //         reversal = new JockeyingForPosition(card.Title, card.Types, card.Subtypes, card.Fortitude,
    //                                             card.Damage, card.StunValue, card.CardEffect);
    //     }
    //     else
    //     {
    //         throw new Exception("Reversal not found");
    //     }
    //     reversal.PlayAs = "Reversal";
    //     return reversal;
    // }

    // public static Superstar InitSuperstarByName(Superstar superstarInfo)
    // {
    //     string superstarName = superstarInfo.Name;
    //     Superstar superstar;
    //     if (superstarName == "STONE COLD STEVE AUSTIN")
    //     {
    //         superstar = new StoneCold();
    //     }
    //     else if (superstarName == "THE UNDERTAKER")
    //     {
    //         superstar = new Undertaker();
    //     }
    //     else if (superstarName == "MANKIND")
    //     {
    //         superstar = new ManKind();
    //     }
    //     else if (superstarName == "HHH")
    //     {
    //         superstar = new HHH();
    //     }
    //     else if (superstarName == "THE ROCK")
    //     {
    //         superstar = new TheRock();
    //     }
    //     else if (superstarName == "KANE")
    //     {
    //         superstar = new Kane();
    //     }
    //     else if (superstarName == "CHRIS JERICHO")
    //     {
    //         superstar = new Jericho();
    //     }
    //     else
    //     {
    //         throw new Exception("Superstar not found");
    //     }
    //     return superstar;
    // }

    public static Play InitPlayByType(int cardId, Player player)
    {
        // Card card = player.Deck.GetPossibleCardsToPlay()[cardId];
        Card card = player.Deck.GetPossibleCardsToPlay().GetCard(cardId);
        Play play;
        if (card.PlayAs == "Maneuver")
        {
            play = InitManeuverByTitle(card.Title, cardId, player);
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

    private static Maneuver InitManeuverByTitle(string cardTitle, int cardId, Player player)
    {
        Maneuver maneuver;
        if (cardTitle == "Chop")
        {
            maneuver = new Chop(cardId, player);
        }
        else if (cardTitle == "Punch")
        {
            maneuver = new Punch(cardId, player);
        }
        else if (cardTitle == "Head Butt")
        {
            maneuver = new HeadButt(cardId, player);
        }
        else if (cardTitle == "Roundhouse Punch")
        {
            maneuver = new RoundhousePunch(cardId, player);
        }
        else if (cardTitle == "Big Boot")
        {
            maneuver = new BigBoot(cardId, player);
        }
        else if (cardTitle == "Shoulder Block")
        {
            maneuver = new ShoulderBlock(cardId, player);
        }
        else if (cardTitle == "Ensugiri")
        {
            maneuver = new Ensugiri(cardId, player);
        }
        else if (cardTitle == "Drop Kick")
        {
            maneuver = new DropKick(cardId, player);
        }
        else if (cardTitle == "Spear")
        {
            maneuver = new Spear(cardId, player);
        }
        else if (cardTitle == "Chair Shot")
        {
            maneuver = new ChairShot(cardId, player);
        }
        else if (cardTitle == "Hurricanrana")
        {
            maneuver = new Hurricanrana(cardId, player);
        }
        else if (cardTitle == "Arm Bar Takedown")
        {
            maneuver = new ArmBarTakedown(cardId, player);
        }
        else if (cardTitle == "Hip Toss")
        {
            maneuver = new HipToss(cardId, player);
        }
        else if (cardTitle == "Arm Drag")
        {
            maneuver = new ArmDrag(cardId, player);
        }
        else if (cardTitle == "Russian Leg Sweep")
        {
            maneuver = new RussianLegSweep(cardId, player);
        }
        else if (cardTitle == "Gut Buster")
        {
            maneuver = new GutBuster(cardId, player);
        }
        else if (cardTitle == "Body Slam")
        {
            maneuver = new BodySlam(cardId, player);
        }
        else if (cardTitle == "Back Breaker")
        {
            maneuver = new BackBreaker(cardId, player);
        }
        else if (cardTitle == "Atomic Facebuster")
        {
            maneuver = new AtomicFacebuster(cardId, player);
        }
        else if (cardTitle == "Inverse Atomic Drop")
        {
            maneuver = new InverseAtomicDrop(cardId, player);
        }
        else if (cardTitle == "Wrist Lock")
        {
            maneuver = new WristLock(cardId, player);
        }
        else if (cardTitle == "Sit Out Powerbomb")
        {
            maneuver = new SitOutPowerbomb(cardId, player);
        }
        else if (cardTitle == "Chin Lock")
        {
            maneuver = new ChinLock(cardId, player);
        }
        else if (cardTitle == "Step Over Toe Hold")
        {
            maneuver = new StepOverToeHold(cardId, player);
        }
        else if (cardTitle == "Bow & Arrow")
        {
            maneuver = new BowAndArrow(cardId, player);
        }
        else if (cardTitle == "Collar & Elbow Lockup")
        {
            maneuver = new CollarAndElbowLockup(cardId, player);
        }
        else if (cardTitle == "Undertaker's Chokeslam")
        {
            maneuver = new UndertakersChokeslam(cardId, player);
        }
        else if (cardTitle == "Kane's Chokeslam")
        {
            maneuver = new KanesChokeslam(cardId, player);
        }
        else
        {
            throw new Exception("Maneuver not found");
        }
        return maneuver;
    }

}