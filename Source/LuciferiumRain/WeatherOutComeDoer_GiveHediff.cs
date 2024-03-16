using RimWorld;
using Verse;

namespace LuciferiumRain;

public class WeatherOutComeDoer_GiveHediff : WeatherOutcomeDoer
{
    public readonly float severity = -1f;

    public HediffDef hediffDef;

    public ChemicalDef toleranceChemical;

    protected override void DoWeatherOutcomeSpecial(Pawn pawn, GameCondition gameCondition)
    {
        var hediff = HediffMaker.MakeHediff(hediffDef, pawn);
        var num = severity > 0f ? severity : hediffDef.initialSeverity;

        num /= pawn.BodySize;

        AddictionUtility.ModifyChemicalEffectForToleranceAndBodySize(pawn, toleranceChemical, ref num, true);
        hediff.Severity = num;
        Log.Message($"Setting severity to {num} for {pawn}");
        pawn.health.AddHediff(hediff);
    }
}