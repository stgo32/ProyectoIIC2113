namespace RawDeal.Initialize;


using RawDeal.Plays;
// using RawDeal.Maneuvers;


public static class PlayFactory
{
    public static Play GetPlay(int cardId, Player player)
    {
        Card card = player.Deck.GetPossibleCardsToPlay().GetCard(cardId);
        Play play;
        if (card.PlayAs == PlayAs.Maneuver)
        {
            play = new Maneuver(cardId, player);
            // play = InitManeuverByTitle(card.Title, cardId, player);
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

    // private static Maneuver InitManeuverByTitle(string cardTitle, int cardId, Player player)
    // {
    //     Maneuver maneuver;
    //     if (cardTitle == "Chop")
    //     {
    //         maneuver = new Chop(cardId, player);
    //     }
    //     else if (cardTitle == "Punch")
    //     {
    //         maneuver = new Punch(cardId, player);
    //     }
    //     else if (cardTitle == "Head Butt")
    //     {
    //         maneuver = new HeadButt(cardId, player);
    //     }
    //     else if (cardTitle == "Roundhouse Punch")
    //     {
    //         maneuver = new RoundhousePunch(cardId, player);
    //     }
    //     else if (cardTitle == "Big Boot")
    //     {
    //         maneuver = new BigBoot(cardId, player);
    //     }
    //     else if (cardTitle == "Shoulder Block")
    //     {
    //         maneuver = new ShoulderBlock(cardId, player);
    //     }
    //     else if (cardTitle == "Ensugiri")
    //     {
    //         maneuver = new Ensugiri(cardId, player);
    //     }
    //     else if (cardTitle == "Drop Kick")
    //     {
    //         maneuver = new DropKick(cardId, player);
    //     }
    //     else if (cardTitle == "Spear")
    //     {
    //         maneuver = new Spear(cardId, player);
    //     }
    //     else if (cardTitle == "Chair Shot")
    //     {
    //         maneuver = new ChairShot(cardId, player);
    //     }
    //     else if (cardTitle == "Hurricanrana")
    //     {
    //         maneuver = new Hurricanrana(cardId, player);
    //     }
    //     else if (cardTitle == "Arm Bar Takedown")
    //     {
    //         maneuver = new ArmBarTakedown(cardId, player);
    //     }
    //     else if (cardTitle == "Hip Toss")
    //     {
    //         maneuver = new HipToss(cardId, player);
    //     }
    //     else if (cardTitle == "Arm Drag")
    //     {
    //         maneuver = new ArmDrag(cardId, player);
    //     }
    //     else if (cardTitle == "Russian Leg Sweep")
    //     {
    //         maneuver = new RussianLegSweep(cardId, player);
    //     }
    //     else if (cardTitle == "Gut Buster")
    //     {
    //         maneuver = new GutBuster(cardId, player);
    //     }
    //     else if (cardTitle == "Body Slam")
    //     {
    //         maneuver = new BodySlam(cardId, player);
    //     }
    //     else if (cardTitle == "Back Breaker")
    //     {
    //         maneuver = new BackBreaker(cardId, player);
    //     }
    //     else if (cardTitle == "Atomic Facebuster")
    //     {
    //         maneuver = new AtomicFacebuster(cardId, player);
    //     }
    //     else if (cardTitle == "Inverse Atomic Drop")
    //     {
    //         maneuver = new InverseAtomicDrop(cardId, player);
    //     }
    //     else if (cardTitle == "Wrist Lock")
    //     {
    //         maneuver = new WristLock(cardId, player);
    //     }
    //     else if (cardTitle == "Sit Out Powerbomb")
    //     {
    //         maneuver = new SitOutPowerbomb(cardId, player);
    //     }
    //     else if (cardTitle == "Chin Lock")
    //     {
    //         maneuver = new ChinLock(cardId, player);
    //     }
    //     else if (cardTitle == "Step Over Toe Hold")
    //     {
    //         maneuver = new StepOverToeHold(cardId, player);
    //     }
    //     else if (cardTitle == "Bow & Arrow")
    //     {
    //         maneuver = new BowAndArrow(cardId, player);
    //     }
    //     else if (cardTitle == "Collar & Elbow Lockup")
    //     {
    //         maneuver = new CollarAndElbowLockup(cardId, player);
    //     }
    //     else if (cardTitle == "Undertaker's Chokeslam")
    //     {
    //         maneuver = new UndertakersChokeslam(cardId, player);
    //     }
    //     else if (cardTitle == "Kane's Chokeslam")
    //     {
    //         maneuver = new KanesChokeslam(cardId, player);
    //     }
    //     else
    //     {
    //         throw new Exception("Maneuver not found");
    //     }
    //     return maneuver;
    // }
}