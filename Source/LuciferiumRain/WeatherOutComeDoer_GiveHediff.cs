using RimWorld;
using Verse;

namespace LuciferiumRain;

public class WeatherOutComeDoer_GiveHediff : WeatherOutcomeDoer
{
    private bool divideByBodySize;

    public HediffDef hediffDef;

    public float severity = -1f;

    public ChemicalDef toleranceChemical;

    protected override void DoWeatherOutcomeSpecial(Pawn pawn, GameCondition gameCondition)
    {
        var hediff = HediffMaker.MakeHediff(hediffDef, pawn);
        var num = severity > 0f ? severity : hediffDef.initialSeverity;

        if (divideByBodySize)
        {
            num /= pawn.BodySize;
        }

        AddictionUtility.ModifyChemicalEffectForToleranceAndBodySize(pawn, toleranceChemical, ref num);
        hediff.Severity = num;
        pawn.health.AddHediff(hediff);
    }
}