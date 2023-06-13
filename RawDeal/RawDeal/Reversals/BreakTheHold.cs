// namespace RawDeal.Reversals;


// using RawDeal.Plays;


// public class BreakTheHole : Reversal
// {
//     public BreakTheHole(
//         string title, List<string> types, List<string> subtypes, 
//         string fortitude, string damage, string stunValue, string cardEffect
//     ) : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
//     {}

//     public override bool CanReverse(Card card, int fortitude, Player oponent)
//     {
//         bool fortitudeRestriction = CalculateFortitudeRestriction(
//             fortitude,
//             oponent.NextSubtypeReversalIsPlusF
//         );
//         bool reversalRestriction = card.ContainsSubtype("Submission") && 
//                                    card.PlayAs == PlayAs.Maneuver;
//         return fortitudeRestriction && reversalRestriction;
//     }

//     protected override void ApplyDamage(Play play) { return; }
// }