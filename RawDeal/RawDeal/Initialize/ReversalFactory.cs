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
        else
        {
            throw new Exception("Reversal not found");
        }
        reversal.PlayAs = "Reversal";
        return reversal;
    }
}