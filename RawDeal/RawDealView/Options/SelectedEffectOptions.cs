namespace RawDealView.Options;

public static class SelectedEffectOptions
{
    private const string NextGrappleIsPlus4D = "Tu siguiente carta, si es un Grapple, tiene +4D.";
    private const string NextGrapplesReversalIsPlus8F =
        "Tu siguiente carta, si es un Grapple, tu oponente requiere +8F para revertirla.";
    private const string DrawCards = "Robar cartas.";
    private const string ForceOpponentToDiscard = "Forzar al oponente a descartar cartas.";
    private const string TakeCardFromArsenal = "Obtener carta desde el arsenal.";
    private const string TakeCardFromRingside = "Obtener carta desde el ringside.";

    private static readonly Dictionary<SelectedEffect, string> Effect2String = new ();
    private static readonly Dictionary<string, SelectedEffect> String2Effect = new ();
    
    static SelectedEffectOptions()
    {
        InitializeString2Effect();
        InitializeString2EffectUsingTheInformationFromEffect2String();
    }

    private static void InitializeString2Effect()
    {
        Effect2String[SelectedEffect.NextGrappleIsPlus4D] = NextGrappleIsPlus4D;
        Effect2String[SelectedEffect.NextGrapplesReversalIsPlus8F] = NextGrapplesReversalIsPlus8F;
        Effect2String[SelectedEffect.DrawCards] = DrawCards;
        Effect2String[SelectedEffect.ForceOpponentToDiscard] = ForceOpponentToDiscard;
        Effect2String[SelectedEffect.TakeCardFromArsenal] = TakeCardFromArsenal;
        Effect2String[SelectedEffect.TakeCardFromRingside] = TakeCardFromRingside;
    }

    private static void InitializeString2EffectUsingTheInformationFromEffect2String()
    {
        foreach (var item in Effect2String)
            String2Effect[item.Value] = item.Key;
    }

    public static string[] GetOptions(SelectedEffect[] possibleEffects)
    {
        string[] options = new string[possibleEffects.Length];
        for (int i = 0; i < possibleEffects.Length; i++)
            options[i] = Effect2String[possibleEffects[i]];
        return options;
    }

    public static SelectedEffect GetSelectedEffectFromText(string selectedEffect)
        => String2Effect[selectedEffect];

}