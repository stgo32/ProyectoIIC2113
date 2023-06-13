namespace RawDeal.Initialize;


using RawDeal.Reversals;
using RawDeal.Plays;

public static class ReversalFactory
{
    public static Reversal GetReversal(Card card)
    {
        string reversalTitle = card.Title;
        Reversal reversal;
        if (reversalTitle == "Step Aside")
        {
            reversal = new MayOnlyReverseACardThatContainsSubtype(
                PlayAs.Maneuver, Subtype.Strike, card.Title, card.Types, card.Subtypes, 
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Escape Move")
        {
            reversal = new MayOnlyReverseACardThatContainsSubtype(
                PlayAs.Maneuver, Subtype.Grapple, card.Title, card.Types, card.Subtypes, 
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Break the Hold")
        {
            reversal = new MayOnlyReverseACardThatContainsSubtype(
                PlayAs.Maneuver, Subtype.Submission, card.Title, card.Types, card.Subtypes, 
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "No Chance in Hell")
        {
            reversal = new MayOnlyReverseACardThatContainsSubtype(
                PlayAs.Action, Subtype.All, card.Title, card.Types, card.Subtypes, 
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Rolling Takedown")
        {
            reversal = new TakesTheDamageOfTheCardBeingReversed(
                PlayAs.Maneuver, Subtype.Grapple, 7, card.Title, card.Types, card.Subtypes,
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Knee to the Gut")
        {
            reversal = new TakesTheDamageOfTheCardBeingReversed(
                PlayAs.Maneuver, Subtype.Strike, 7, card.Title, card.Types, card.Subtypes,
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Elbow to the Face")
        {
            reversal = new MayReverseSomeDamageOrLess(
                PlayAs.Maneuver, Subtype.All, 7, card.Title, card.Types, card.Subtypes,
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Manager Interferes")
        {
            reversal = new MayOnlyReverseACardThatContainsSubtype(
                PlayAs.Maneuver, Subtype.All, card.Title, card.Types, card.Subtypes, 
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Chyna Interferes")
        {
            reversal = new MayOnlyReverseACardThatContainsSubtype(
                PlayAs.Maneuver, Subtype.All, card.Title, card.Types, card.Subtypes, 
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Clean Break")
        {
            reversal = new MayOnlyReverseTheCardTitled(
                "Jockeying for Position", PlayAs.Action, card.Title, card.Types, card.Subtypes,
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Jockeying for Position")
        {
            reversal = new MayOnlyReverseTheCardTitled(
                "Jockeying for Position", PlayAs.Action, card.Title, card.Types, card.Subtypes,
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Irish Whip")
        {
            reversal = new MayOnlyReverseTheCardTitled(
                "Irish Whip", PlayAs.Action, card.Title, card.Types, card.Subtypes,
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
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
            reversal = new MayOnlyReverseTheCardTitled(
                "Belly to Belly Suplex", PlayAs.Maneuver, card.Title, card.Types, card.Subtypes,
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Vertical Suplex")
        {
            reversal = new MayOnlyReverseTheCardTitled(
                "Vertical Suplex", PlayAs.Maneuver, card.Title, card.Types, card.Subtypes,
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Belly to Back Suplex")
        {
            reversal = new MayOnlyReverseTheCardTitled(
                "Belly to Back Suplex", PlayAs.Maneuver, card.Title, card.Types, card.Subtypes,
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Ensugiri")
        {
            reversal = new MayOnlyReverseTheCardTitled(
                "Kick", PlayAs.Maneuver, card.Title, card.Types, card.Subtypes,
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Drop Kick")
        {
            reversal = new MayOnlyReverseTheCardTitled(
                "Drop Kick", PlayAs.Maneuver, card.Title, card.Types, card.Subtypes,
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
        }
        else if (reversalTitle == "Double Arm DDT")
        {
            reversal = new MayOnlyReverseTheCardTitled(
                "Back Body Drop", PlayAs.Maneuver, card.Title, card.Types, card.Subtypes,
                card.Fortitude, card.Damage, card.StunValue, card.CardEffect
            );
        }
        else
        {
            throw new Exception("Reversal not found");
        }
        reversal.PlayAs = Plays.PlayAs.Reversal;
        return reversal;
    }
}