namespace RawDeal.Initialize;


using RawDeal.Reversals;


public static class ReversalFactory
{
    public static Reversal GetReversal(Card card)
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
        else if (reversalTitle == "Irish Whip")
        {
            reversal = new IrishWhip(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                     card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "Shoulder Block")
        {
            reversal = new MayOnlyReverseAManeuverAfterIrishWhip(
                true, card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, 
                card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Spear")
        {
            reversal = new MayOnlyReverseAManeuverAfterIrishWhip(
                true, card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, 
                card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Facebuster")
        {
            reversal = new MayOnlyReverseAManeuverAfterIrishWhip(
                false, card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, 
                card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Lou Thesz Press")
        {
            reversal = new MayOnlyReverseAManeuverAfterIrishWhip(
                false, card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, 
                card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Cross Body Block")
            reversal = new MayOnlyReverseAManeuverAfterIrishWhip(
                true, card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, 
                card.StunValue, card.CardEffect
            );
        else if (reversalTitle == "Belly to Belly Suplex")
        {
            reversal = new BellyToBellySuplex(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                              card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "Vertical Suplex")
        {
            reversal = new VerticalSuplex(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                          card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "Belly to Back Suplex")
        {
            reversal = new BellyToBackSuplex(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                             card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "Ensugiri")
        {
            reversal = new Ensugiri(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                    card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "Drop Kick")
        {
            reversal = new DropKick(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                    card.Damage, card.StunValue, card.CardEffect);
        }
        else if (reversalTitle == "Double Arm DDT")
        {
            reversal = new DoubleArmDDT(card.Title, card.Types, card.Subtypes, card.Fortitude,
                                        card.Damage, card.StunValue, card.CardEffect);
        }
        else
        {
            throw new Exception("Reversal not found");
        }
        reversal.PlayAs = Plays.PlayAs.Reversal;
        return reversal;
    }
}